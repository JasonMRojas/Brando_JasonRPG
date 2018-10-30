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
        int _currentLevel = 1;

        /// <summary>
        /// Runs the logic around the game loop
        /// </summary>
        /// <param name="difficulty">Difficulty of the game</param>
        /// <param name="newPlayer">The current player</param>
        /// <returns>Whether or not the game will continue</returns>
        public void RunMapGame()
        {
            EncounterProg encounter = new EncounterProg();
            encounter.RunCharacterCreation();

            Random randomNumberSeed = new Random();

            bool newMap = true;

            int height = 8, width = 20;
            while (newMap)
            {
                PlayerToken player = new PlayerToken(encounter);

                Console.WriteLine("Loading New Map...");
                Console.WriteLine("This may take up to 60 seconds...");

                Map map = new Map(width, height);
                TownMap townMap = new TownMap(width, height);
                Dictionary<int, CaveMap> caveMapStorage = new Dictionary<int, CaveMap>();
                List<Tile> specialTilePile = new List<Tile>();

                int randomSeed = randomNumberSeed.Next(_currentLevel - 1, _currentLevel + 2);
                if (_currentLevel == 1)
                {
                    randomSeed = randomNumberSeed.Next(0, 3);
                }
                else if (_currentLevel == 2)
                {
                    randomSeed = randomNumberSeed.Next(1, 3);
                }
                for (int i = 0; i < randomSeed; i++)
                {
                    double randomWidthSeed = randomNumberSeed.Next(2, 4);
                    double randomHeightSeed = randomNumberSeed.Next(1, 1);
                    CaveMap caveMap = new CaveMap((int)(width / (randomWidthSeed)), (int)(height / (randomHeightSeed)));
                    CaveTile caveTile = new CaveTile();
                    caveTile.AssociationNum = i + 1;
                    specialTilePile.Add(caveTile);
                    caveMapStorage.Add(i + 1, caveMap);
                }
                TownMapTile currentTownMapTile = new TownMapTile();
                ExitTile endTile = new ExitTile();
                endTile.Position = new int[2];
                currentTownMapTile.Position = FindTileOrEntitySpawn.StartingPostion(map, currentTownMapTile);
                specialTilePile.Add(currentTownMapTile);
                specialTilePile.Add(endTile);


                List<ICharacter> entityPile = new List<ICharacter>();
                randomSeed = randomNumberSeed.Next(_currentLevel, _currentLevel * 3);

                bool isTownMap = true;
                bool isCaveMap = true;
                while (isTownMap || isCaveMap)
                {
                    entityPile.Add(player);
                    endTile.Position[0] = map.MapArrayOfArrays.Length - 2;
                    endTile.Position[1] = map.MapArrayOfArrays[0].Length - 2;
                    isTownMap = false;
                    isCaveMap = false;

                    AddEntitiesAndTilesToMap(map, specialTilePile, randomSeed, entityPile);

                    map.BuildMapDisplay();

                    newMap = RunMapGameLoop(map, entityPile, specialTilePile, encounter);
                    ClearMap(map, entityPile);

                    foreach (Tile tile in specialTilePile)
                    {
                        if (tile.GetType() == typeof(CaveTile) && IsColliding.IsCurrentlyColliding(tile, player))
                        {
                            newMap = RunCaveMap(encounter, player, caveMapStorage, endTile, tile);
                            isCaveMap = true;
                        }
                        else if (tile.GetType() == typeof(TownMapTile) && IsColliding.IsCurrentlyColliding(currentTownMapTile, player))
                        {
                            encounter.TownReplenish();
                            newMap = RunTownMap(encounter, player, townMap, currentTownMapTile, endTile);
                            isTownMap = true;
                        }
                    }
                }

                width += 10;
                height += 2;
                if (width == 100)
                {
                    newMap = false;
                }

                _currentLevel++;
            }

            Console.WriteLine("Game Over");
        }

        private static void AddEntitiesAndTilesToMap(Map map, List<Tile> specialTilePile, int randomSeed, List<ICharacter> entityPile)
        {
            foreach (Tile tile in specialTilePile)
            {
                if (tile.GetType() != typeof(ExitTile) && tile.Position == null)
                {
                    tile.Position = FindTileOrEntitySpawn.StartingPostion(map, tile);
                }
                map.SetTilePosition(tile.Position, tile.InstanValue);
            }
            for (int i = 0; i < randomSeed; i++)
            {
                NormalMonster monster = new NormalMonster();
                monster.Position = FindTileOrEntitySpawn.StartingPostion(map, monster);
                entityPile.Add(monster);
            }
            foreach (ICharacter entity in entityPile)
            {
                map.SetEntityPosition(entity);
            }
        }

        private static void AddEntitiesAndTilesToMap(Map map, List<Tile> specialTilePile, List<ICharacter> entityPile)
        {
            foreach (ICharacter entity in entityPile)
            {
                map.SetTilePosition(entity.Position, entity.Value);
            }
            foreach (Tile tile in specialTilePile)
            {
                if (tile.GetType() != typeof(ExitTile) && tile.Position == null)
                {
                    tile.Position = FindTileOrEntitySpawn.StartingPostion(map, tile);
                }
                map.MapArrayOfArrays[tile.Position[0]][tile.Position[1]] = tile.InstanValue;
            }
        }

        private bool RunCaveMap(EncounterProg encounter, PlayerToken player, Dictionary<int, CaveMap> caveMapStorage, ExitTile endTile, Tile tile)
        {
            Random rand = new Random();
            int randomEnemySeed = rand.Next(_currentLevel, (int)(_currentLevel * 1.5));
            List<ICharacter> entityCavePile = new List<ICharacter>();
            entityCavePile.Add(player);
            endTile.Position[0] = 1;
            endTile.Position[1] = 1;
            player.Position[0] = 1;
            player.Position[1] = 2;

            caveMapStorage[tile.AssociationNum].SetEntityPosition(player);
            int[] exitTilePos = new int[2] { 1, 1 };
            caveMapStorage[tile.AssociationNum].SetTilePosition(exitTilePos, ExitTile.Value);

            List<Tile> caveSpecialTilePile = new List<Tile>()
            {
                endTile
            };


            caveMapStorage[tile.AssociationNum].BuildMapDisplay();
            AddEntitiesAndTilesToMap(caveMapStorage[tile.AssociationNum], caveSpecialTilePile, randomEnemySeed, entityCavePile);
            bool nextMap = RunMapGameLoop(caveMapStorage[tile.AssociationNum], entityCavePile, caveSpecialTilePile, encounter);
            ClearMap(caveMapStorage[tile.AssociationNum], entityCavePile);


            player.Position[0] = tile.Position[0];
            player.Position[1] = tile.Position[1] + 1;

            return nextMap;
        }

        private bool RunTownMap(EncounterProg encounter, PlayerToken player, TownMap townMap, TownMapTile currentTownMapTile, ExitTile endTile)
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

            AddEntitiesAndTilesToMap(townMap, townSpecialTilePile, townEntityPile);

            bool nextMap = RunMapGameLoop(townMap, townEntityPile, townSpecialTilePile, encounter);
            ClearMap(townMap, townEntityPile);

            player.Position[0] = currentTownMapTile.Position[0];
            player.Position[1] = currentTownMapTile.Position[1] + 1;



            return nextMap;
        }

        private static void ClearMap(Map map, List<ICharacter> entityPile)
        {
            foreach (ICharacter entity in entityPile)
            {
                map.SetTilePosition(entity.Position, Tile.Value);
            }

            entityPile.Clear();
        }

        /// <summary>
        /// Runs the game loop. Moving tokens and otherwise
        /// </summary>
        /// <param name="map">The Current Map</param>
        /// <returns></returns>
        private bool RunMapGameLoop(Map map, List<ICharacter> entityPile, List<Tile> specialTilePile, EncounterProg encounter)
        {
            Console.Clear();
            Display_Map.DisplayMap(map);
            List<ICharacter> npcs = new List<ICharacter>();
            ICharacter player = new PlayerToken(encounter);
            int tickCounter = 1;
            Random randomMovement = new Random();
            bool updateDisplay = false;
            bool gameOver = false;

            int randomTick = randomMovement.Next(100, 200);
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
                    if (entity.GetType() == typeof(PlayerToken))
                    {
                        entity.Move(map);
                    }
                    else if (tickCounter == randomTick && toRemove != entity)
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

                            gameOver = encounter.RunEncounterProg(npc.EncounterLevel);

                            isOver = gameOver;

                            toRemove = npc;

                            Console.Clear();
                        }
                    }
                    map.BuildMapDisplay();
                    npcs.Remove(toRemove);
                    if (entity.DidMove)
                    {
                        updateDisplay = true;
                    }
                }
                entityPile.Remove(toRemove);


                if (isOver)
                {
                    break;
                }

                if (tickCounter >= randomTick + 1)
                {
                    tickCounter = 1;
                    randomTick = randomMovement.Next(100, 200);
                }

                npcs.Clear();
                map.SetEntityPosition(player);
                if (updateDisplay)
                {
                    map.BuildMapDisplay();
                    Display_Map.DisplayMap(map);
                }

                updateDisplay = false;
                tickCounter++;
            }

            return !gameOver;
        }
    }
}

