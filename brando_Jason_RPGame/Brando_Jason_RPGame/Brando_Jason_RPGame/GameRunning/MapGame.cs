using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGame.Entities;
using Brando_Jason_RPGame.Mapping;

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
            int height = 12, width = 50;
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

                Dictionary<int, CaveMap> caveMapStorage = new Dictionary<int, CaveMap>();

                Random randomCaveNumberSeed = new Random();
                int randomSeed = randomCaveNumberSeed.Next(2, width / 20);
                for (int i = 0; i < randomSeed; i++)
                {
                    double randomWidthSeed = randomCaveNumberSeed.NextDouble();
                    double randomHeightSeed = randomCaveNumberSeed.NextDouble();
                    CaveMap caveMap = new CaveMap((int)(width * (randomWidthSeed + .5)), (int)(height * (randomHeightSeed + .25)));
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
                    if (IsColliding.IsCurrentlyColliding(currentTownMapTile, player))
                    {
                        endTile.Position[0] = 1;
                        endTile.Position[1] = 1;
                        player.Position[0] = 1;
                        player.Position[1] = 2;
                        townMap.MapArrayOfArrays[1][2] = player.Value;
                        townMap.MapArrayOfArrays[1][1] = ExitTile.Value;
                        townMap.Display = townMap.BuildMapDisplay();
                        newMap = RunMapGameLoop(townMap, entityPile, specialTilePile);
                        isTownMap = true;
                        player.Position[0] = currentTownMapTile.Position[0];
                        player.Position[1] = currentTownMapTile.Position[1] + 1;
                    }

                    foreach (Tile caveTile in specialTilePile)
                    {
                        if (caveTile.GetType() == typeof(CaveTile) && IsColliding.IsCurrentlyColliding(caveTile, player))
                        {
                            endTile.Position[0] = 1;
                            endTile.Position[1] = 1;
                            player.Position[0] = 1;
                            player.Position[1] = 2;
                            caveMapStorage[caveTile.AssociationNum].MapArrayOfArrays[1][2] = player.Value;
                            caveMapStorage[caveTile.AssociationNum].MapArrayOfArrays[1][1] = ExitTile.Value;
                            caveMapStorage[caveTile.AssociationNum].Display = caveMapStorage[caveTile.AssociationNum].BuildMapDisplay();
                            newMap = RunMapGameLoop(caveMapStorage[caveTile.AssociationNum], entityPile, specialTilePile);
                            isCaveMap = true;
                            player.Position[0] = caveTile.Position[0];
                            player.Position[1] = caveTile.Position[1] + 1;
                        }
                    }
                } while (isTownMap || isCaveMap);
                width += 10;
                height += 3;
                if (width == 90)
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
                foreach (ICharacter entity in entityPile) //Moves each entity which was added to the list of them. 
                {
                    entity.Move(map);
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
                map.Display = map.BuildMapDisplay();
                Display_Map.DisplayMap(map);

                if (isOver)
                {
                    break;
                }
            }

            return true;
        }
    }
}

