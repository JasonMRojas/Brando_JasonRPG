using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGame.Entities;
using Brando_Jason_RPGame.Mapping;

namespace Brando_Jason_RPGame.GameRunning
{
    public static class FindTileOrEntitySpawn
    {
        //public static int[] StartingDronePostion(Map map, Tile randomTilePlace)
        //{
        //    Random random = new Random();
        //    int randomSeedI = random.Next(2, map.MapArrayOfArrays.Length - 2);
        //    int randomSeedJ = random.Next(2, map.MapArrayOfArrays[map.MapArrayOfArrays.Length - 2].Length - 2);
        //    int[] returnedInt = new int[2];
        //    for (int i = randomSeedI; i < map.MapArrayOfArrays.Length; i++)
        //    {
        //        for (int j = randomSeedJ; j < map.MapArrayOfArrays[i].Length; j++)
        //        {
        //            int[] currentPosition = new int[2];
        //            currentPosition[0] = i;
        //            currentPosition[1] = j;
        //            if (map.MapArrayOfArrays[i][j] == 0 && !map.IsSurrounded(map.MapArrayOfArrays, currentPosition, -1))
        //            {
        //                map.MapArrayOfArrays[i][j] = -3;
        //                returnedInt[0] = i;
        //                returnedInt[1] = j;
        //                break;
        //            }
        //        }
        //        if (i == returnedInt[0])
        //        {
        //            break;
        //        }
        //    }
        //    return returnedInt;
        //}

        public static int[] StartingPostion(Map map, Tile mapTile)
        {
            int[] returnedInt = new int[2];
            bool tilePlaced = false;
            do
            {
                Random random = new Random();
                int randomSeedI = random.Next(2, map.MapArrayOfArrays.Length - 2);
                int randomSeedJ = random.Next(2, map.MapArrayOfArrays[map.MapArrayOfArrays.Length - 2].Length - 2);
                for (int i = randomSeedI; i < map.MapArrayOfArrays.Length; i++)
                {
                    for (int j = randomSeedJ; j < map.MapArrayOfArrays[i].Length; j++)
                    {
                        int[] currentPosition = new int[2];
                        currentPosition[0] = i;
                        currentPosition[1] = j;
                        int[] rightOfCurrentPosition = new int[2];
                        rightOfCurrentPosition[0] = i;
                        rightOfCurrentPosition[1] = j + 1;
                        if (map.MapArrayOfArrays[i][j] == 0 && !map.IsSurrounded(map.MapArrayOfArrays, currentPosition, Wall.Value)
                            && !map.IsNextTo(map.MapArrayOfArrays, rightOfCurrentPosition, Wall.Value))
                        {
                            if (mapTile.GetType() == typeof(TownMapTile))
                            {
                                map.MapArrayOfArrays[i][j] = TownMapTile.Value;
                            }
                            else
                            {
                                map.MapArrayOfArrays[i][j] = CaveTile.Value;
                            }
                            returnedInt[0] = i;
                            returnedInt[1] = j;
                            tilePlaced = true;
                            break;
                        }
                    }
                    if (i == returnedInt[0])
                    {
                        break;
                    }
                }
                Console.WriteLine("TRIED PLACE TILE");
            } while (!tilePlaced);
            
            return returnedInt;
        }

        public static int[] StartingPostion(Map map, ICharacter droneToPlace)
        {
            int[] returnedInt = new int[2];
            bool tilePlaced = false;
            do
            {
                Random random = new Random();
                int randomSeedI = random.Next(2, map.MapArrayOfArrays.Length - 2);
                int randomSeedJ = random.Next(2, map.MapArrayOfArrays[map.MapArrayOfArrays.Length - 2].Length - 2);
                for (int i = randomSeedI; i < map.MapArrayOfArrays.Length; i++)
                {
                    for (int j = randomSeedJ; j < map.MapArrayOfArrays[i].Length; j++)
                    {
                        int[] currentPosition = new int[2];
                        currentPosition[0] = i;
                        currentPosition[1] = j;
                        int[] rightOfCurrentPosition = new int[2];
                        rightOfCurrentPosition[0] = i;
                        rightOfCurrentPosition[1] = j + 1;
                        if (map.MapArrayOfArrays[i][j] == 0 && !map.IsSurrounded(map.MapArrayOfArrays, currentPosition, Wall.Value)
                            && !map.IsNextTo(map.MapArrayOfArrays, rightOfCurrentPosition, Wall.Value))
                        {
                            if (droneToPlace.GetType() == typeof(TownMapTile))
                            {
                                map.MapArrayOfArrays[i][j] = -2;
                            }
                            else
                            {
                                map.MapArrayOfArrays[i][j] = -3;
                            }
                            returnedInt[0] = i;
                            returnedInt[1] = j;
                            tilePlaced = true;
                            break;
                        }
                    }
                    if (i == returnedInt[0])
                    {
                        break;
                    }
                }
                Console.WriteLine("TRIED PLACE TILE");
            } while (!tilePlaced);

            return returnedInt;
        }
    }
}
