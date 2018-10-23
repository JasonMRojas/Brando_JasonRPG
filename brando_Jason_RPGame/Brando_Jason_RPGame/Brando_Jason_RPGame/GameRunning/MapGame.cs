using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGMapping.Entities;
using Brando_Jason_RPGMapping.Mapping;
using BrandoJason_RPGEncounterLogic;

namespace Brando_Jason_RPGMapping.GameRunning
{
    public class MapGame
    {
        /// <summary>
        /// Runs the logic around the game loop
        /// </summary>
        /// <param name="difficulty">Difficulty of the game</param>
        /// <param name="newPlayer">The current player</param>
        /// <returns>Whether or not the game will continue</returns>
        public bool RunMapGame()
        {
            EncounterProg encounter = new EncounterProg();
            encounter.RunCharacterCreation();



            PlayerToken player = new PlayerToken();
            bool newMap = false;

            int height = 8, width = 20;
            do
            {
                player.Position[0] = 1;
                player.Position[1] = 1;
                Console.WriteLine("Loading New Map...");
                Console.WriteLine("This may take up to 60 seconds...");

                List<ICharacter> entityPile = new List<ICharacter>();
                List<Tile> specialTilePile = new List<Tile>();
                TownMapTile currentTownMapTile = new TownMapTile();
                ExitTile endTile = new ExitTile();
                endTile.Position = new int[2];


                Map map = new Map(width, height);
                TownMap townMap = new TownMap(width, height);





                entityPile.Add(player);
                Dictionary<int, CaveMap> caveMapStorage = new Dictionary<int, CaveMap>();

                Random randomNumberSeed = new Random();
                int randomSeed = randomNumberSeed.Next(1, 5);
                for (int i = 0; i < randomSeed; i++)
                {
                    double randomWidthSeed = randomNumberSeed.Next(2, 4);
                    double randomHeightSeed = randomNumberSeed.Next(1, 1);
                    CaveMap caveMap = new CaveMap((int)(width / (randomWidthSeed)), (int)(height / (randomHeightSeed)));
                    CaveTile caveTile = new CaveTile();
                    caveTile.Position = FindTileOrEntitySpawn.StartingPostion(map, caveTile);
                    caveTile.AssociationNum = i + 1;
                    specialTilePile.Add(caveTile);
                    caveMapStorage.Add(i + 1, caveMap);
                }
                randomSeed = randomNumberSeed.Next(1, width / 10);
                for (int i = 0; i < randomSeed; i++)
                {
                    NormalMonster slime = new NormalMonster();
                    slime.Position = FindTileOrEntitySpawn.StartingPostion(map, slime);
                    entityPile.Add(slime);
                }


                currentTownMapTile.Position = FindTileOrEntitySpawn.StartingPostion(map, currentTownMapTile);

                specialTilePile.Add(currentTownMapTile);
                specialTilePile.Add(endTile);




                bool isTownMap = false;
                bool isCaveMap = false;
                do
                {

                    isTownMap = false;
                    isCaveMap = false;

                    map.MapArrayOfArrays[currentTownMapTile.Position[0]][currentTownMapTile.Position[1]] = TownMapTile.Value;
                    map.MapArrayOfArrays[player.Position[0]][player.Position[1]] = -2;
                    foreach (Tile tile in specialTilePile)
                    {
                        if (tile.GetType() == typeof(CaveTile))
                        {
                            map.MapArrayOfArrays[tile.Position[0]][tile.Position[1]] = CaveTile.Value;
                        }
                    }
                    endTile.Position[0] = map.MapArrayOfArrays.Length - 2;
                    endTile.Position[1] = map.MapArrayOfArrays[0].Length - 2;
                    map.MapArrayOfArrays[map.MapArrayOfArrays.Length - 2][map.MapArrayOfArrays[0].Length - 2] = ExitTile.Value;

                    map.BuildMapDisplay();

                    newMap = RunMapGameLoop(map, entityPile, specialTilePile, encounter);

                    foreach (Tile tile in specialTilePile)
                    {
                        if (tile.GetType() == typeof(CaveTile) && IsColliding.IsCurrentlyColliding(tile, player))
                        {
                            endTile.Position[0] = 1;
                            endTile.Position[1] = 1;
                            player.Position[0] = 1;
                            player.Position[1] = 2;

                            caveMapStorage[tile.AssociationNum].SetEntityPosition(player);
                            int[] exitTilePos = new int[2] { 1, 1 };
                            caveMapStorage[tile.AssociationNum].SetTilePosition(exitTilePos, ExitTile.Value);

                            List<ICharacter> entityCavePile = new List<ICharacter>();
                            entityCavePile.Add(player);
                            randomSeed = randomNumberSeed.Next(1, (caveMapStorage[tile.AssociationNum].MapArrayOfArrays[0].Length - 2)/2);
                            for (int i = 0; i < randomSeed; i++)
                            {
                                NormalMonster caveSlime = new NormalMonster();
                                caveSlime.Position = FindTileOrEntitySpawn.StartingPostion(caveMapStorage[tile.AssociationNum], caveSlime);
                                entityCavePile.Add(caveSlime);
                            }

                            List<Tile> caveSpecialTilePile = new List<Tile>()
                            {
                                endTile
                            };

                            caveMapStorage[tile.AssociationNum].BuildMapDisplay();

                            newMap = RunMapGameLoop(caveMapStorage[tile.AssociationNum], entityCavePile, caveSpecialTilePile, encounter);
                            isCaveMap = true;

                            foreach (ICharacter entity in entityCavePile)
                            {
                                caveMapStorage[tile.AssociationNum].SetTilePosition(entity.Position, Tile.Value);
                                caveMapStorage[tile.AssociationNum].BuildMapDisplay();
                            }

                            entityCavePile.Clear();
                            caveSpecialTilePile.Clear();

                            player.Position[0] = tile.Position[0];
                            player.Position[1] = tile.Position[1] + 1;
                        }
                        else if (tile.GetType() == typeof(TownMapTile) && IsColliding.IsCurrentlyColliding(currentTownMapTile, player))
                        {
                            endTile.Position[0] = 1;
                            endTile.Position[1] = 1;

                            player.Position[0] = 1;
                            player.Position[1] = 2;

                            townMap.SetEntityPosition(player);
                            int[] exitTilePos = new int[2] { 1, 1 };
                            townMap.SetTilePosition(exitTilePos, ExitTile.Value);

                            townMap.BuildMapDisplay();

                            List<ICharacter> townEntityPile = new List<ICharacter>();
                            townEntityPile.Add(player);

                            List<Tile> townSpecialTilePile = new List<Tile>()
                            {
                                endTile
                            };

                            newMap = RunMapGameLoop(townMap, townEntityPile, townSpecialTilePile, encounter);
                            isTownMap = true;
                            player.Position[0] = currentTownMapTile.Position[0];
                            player.Position[1] = currentTownMapTile.Position[1] + 1;

                            townEntityPile.Clear();
                        }
                    }
                } while (isTownMap || isCaveMap);



                width += 10;
                height += 2;
                if (width == 100)
                {
                    newMap = false;
                }
            } while (newMap);

            return newMap;

        }

        /// <summary>
        /// Runs the game loop. Moving tokens and otherwise
        /// </summary>
        /// <param name="map">The Current Map</param>
        /// <returns></returns>
        public bool RunMapGameLoop(Map map, List<ICharacter> entityPile, List<Tile> specialTilePile, EncounterProg encounter)
        {
            Console.Clear();
            Display_Map.DisplayMap(map);
            List<ICharacter> npcs = new List<ICharacter>();
            ICharacter player = new PlayerToken();
            while (true)
            {
                foreach (ICharacter entity in entityPile)
                {
                    if (entity.GetType() != typeof(PlayerToken))
                    {
                        npcs.Add(entity);
                    }
                    else
                    {
                        player = entity;
                    }
                }
                Console.CursorVisible = false;
                bool isOver = false;
                Drone toRemove = new Drone();
                foreach (ICharacter entity in entityPile) //Moves each entity which was added to the list of them. 
                {
                    if (toRemove != entity)
                    {
                        entity.Move(map);
                    }
                    foreach (Tile special in specialTilePile)
                    {
                        map.SetTilePosition(special.Position, special.InstanValue);
                        if (IsColliding.IsCurrentlyColliding(special, entity) && entity.GetType() == typeof(PlayerToken))
                        {
                            map.SetEntityPosition(entity);
                            map.BuildMapDisplay();
                            Display_Map.DisplayMap(map);
                            isOver = true;
                            break;
                        }
                    }
                    foreach (Drone npc in npcs)
                    {
                        if (IsColliding.IsCurrentlyColliding(npc, player) && npc != toRemove)
                        {
                            map.BuildMapDisplay();
                            Display_Map.DisplayMap(map);

                            isOver = encounter.RunEncounterProg(npc.EncounterLevel);

                            toRemove = npc;

                            Console.Clear();
                        }
                    }
                    map.BuildMapDisplay();
                }
                entityPile.Remove(toRemove);


                if (isOver)
                {
                    break;
                }

                npcs.Clear();
                map.SetEntityPosition(player);
                map.BuildMapDisplay();
                Display_Map.DisplayMap(map);
            }

            return true;
        }
    }
}

