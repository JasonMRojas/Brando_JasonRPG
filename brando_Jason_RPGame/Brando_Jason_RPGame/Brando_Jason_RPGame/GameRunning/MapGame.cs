using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGame.Entities;
using Brando_Jason_RPGame.Mapping;
using BrandoJason_RPGEncounterLogic;

namespace Brando_Jason_RPGame.GameRunning
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
            PlayerCharacter player = new PlayerCharacter("IRRELEVANT");
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

                NormalMonster slime = new NormalMonster(map);

                slime.Position = FindTileOrEntitySpawn.StartingPostion(map, slime);

                entityPile.Add(slime);
                Dictionary<int, CaveMap> caveMapStorage = new Dictionary<int, CaveMap>();

                Random randomCaveNumberSeed = new Random();
                int randomSeed = randomCaveNumberSeed.Next(0, width / 10);
                for (int i = 0; i < randomSeed; i++)
                {
                    double randomWidthSeed = randomCaveNumberSeed.Next(2, 4);
                    double randomHeightSeed = randomCaveNumberSeed.Next(1, 1);
                    CaveMap caveMap = new CaveMap((int)(width / (randomWidthSeed)), (int)(height / (randomHeightSeed)));
                    CaveTile caveTile = new CaveTile();
                    caveTile.Position = FindTileOrEntitySpawn.StartingPostion(map, caveTile);
                    caveTile.AssociationNum = i + 1;
                    specialTilePile.Add(caveTile);
                    caveMapStorage.Add(i + 1, caveMap);
                }


                currentTownMapTile.Position = FindTileOrEntitySpawn.StartingPostion(map, currentTownMapTile);

                specialTilePile.Add(currentTownMapTile);
                specialTilePile.Add(endTile);


                entityPile.Add(player);


                bool isTownMap = false;
                bool isCaveMap = false;
                do
                {
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
                    map.Display = map.BuildMapDisplay();

                    newMap = RunMapGameLoop(map, entityPile, specialTilePile);
                    isTownMap = false;
                    isCaveMap = false;

                    foreach (Tile tile in specialTilePile)
                    {
                        if (tile.GetType() == typeof(CaveTile) && IsColliding.IsCurrentlyColliding(tile, player))
                        {
                            foreach(ICharacter entity in entityPile)
                            {
                                if (entity.GetType() != typeof(PlayerCharacter))
                                {
                                    map.MapArrayOfArrays[entity.Position[0]][entity.Position[1]] = 0;
                                    entity.Position = FindTileOrEntitySpawn.StartingPostion(caveMapStorage[tile.AssociationNum], entity);
                                }
                            }
                            endTile.Position[0] = 1;
                            endTile.Position[1] = 1;
                            player.Position[0] = 1;
                            player.Position[1] = 2;
                            caveMapStorage[tile.AssociationNum].MapArrayOfArrays[1][2] = player.Value;
                            caveMapStorage[tile.AssociationNum].MapArrayOfArrays[1][1] = ExitTile.Value;
                            caveMapStorage[tile.AssociationNum].Display = caveMapStorage[tile.AssociationNum].BuildMapDisplay();
                            newMap = RunMapGameLoop(caveMapStorage[tile.AssociationNum], entityPile, specialTilePile);
                            isCaveMap = true;
                            player.Position[0] = tile.Position[0];
                            player.Position[1] = tile.Position[1] + 1;
                            foreach (ICharacter entity in entityPile)
                            {
                                if (entity.GetType() != typeof(PlayerCharacter))
                                {
                                    map.MapArrayOfArrays[entity.Position[0]][entity.Position[1]] = 0;
                                    entity.Position = FindTileOrEntitySpawn.StartingPostion(caveMapStorage[tile.AssociationNum], entity);
                                    caveMapStorage[tile.AssociationNum].MapArrayOfArrays[entity.Position[0]][entity.Position[1]] = 0;
                                }
                            }

                        }
                        else if (tile.GetType() == typeof(TownMapTile) && IsColliding.IsCurrentlyColliding(currentTownMapTile, player))
                        {
                            endTile.Position[0] = 1;
                            endTile.Position[1] = 1;
                            player.Position[0] = 1;
                            player.Position[1] = 2;
                            townMap.MapArrayOfArrays[1][2] = player.Value;
                            townMap.MapArrayOfArrays[1][1] = ExitTile.Value;
                            townMap.Display = townMap.BuildMapDisplay();
                            for (int i = 0; i < entityPile.Count; i++)
                            {
                                if (entityPile[i].GetType() != typeof(PlayerCharacter))
                                {
                                    entityPile.Remove(entityPile[i]);
                                    i = 0;
                                }
                            }
                            newMap = RunMapGameLoop(townMap, entityPile, specialTilePile);
                            isTownMap = true;
                            player.Position[0] = currentTownMapTile.Position[0];
                            player.Position[1] = currentTownMapTile.Position[1] + 1;
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
        public bool RunMapGameLoop(Map map, List<ICharacter> entityPile, List<Tile> specialTilePile)
        {

            Console.Clear();
            Display_Map.DisplayMap(map);
            while (true)
            {
                Console.CursorVisible = false;
                bool isOver = false;
                bool hitEnemy = false;
                List<ICharacter> npcs = new List<ICharacter>();
                ICharacter player = new PlayerCharacter("...");
                foreach (ICharacter entity in entityPile)
                {
                    if (entity.GetType() != typeof(PlayerCharacter))
                    {
                        npcs.Add(entity);
                    }
                    else
                    {
                        player = entity;
                    }
                }
                foreach (ICharacter entity in entityPile) //Moves each entity which was added to the list of them. 
                {
                    entity.Move(map);
                    map.Display = map.BuildMapDisplay();
                    Display_Map.DisplayMap(map);
                    foreach (Tile special in specialTilePile)
                    {
                        if (IsColliding.IsCurrentlyColliding(special, entity) && entity.GetType() == typeof(PlayerCharacter))
                        {
                            isOver = true;
                            break;
                        }
                    }
                    if (isOver)
                    {
                        break;
                    }

                }
                foreach (ICharacter npc in npcs)
                {
                    if (IsColliding.IsCurrentlyColliding(npc, player))
                    {
                        isOver = EncounterProg.RunEncounterProg(1);
                        hitEnemy = true;
                    }
                }
                if (hitEnemy)
                {
                    foreach (ICharacter npc in npcs)
                    {
                        if (npc.Position[0] == player.Position[0] && npc.Position[1] == player.Position[1])
                        {
                            entityPile.Remove(npc);
                        }
                    }
                }


                if (isOver)
                {
                    break;
                }
            }

            return true;
        }
    }
}

