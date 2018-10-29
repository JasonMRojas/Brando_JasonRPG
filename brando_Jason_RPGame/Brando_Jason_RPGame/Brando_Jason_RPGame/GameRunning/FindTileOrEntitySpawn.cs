using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGMapping.Entities;
using Brando_Jason_RPGMapping.Mapping;

namespace Brando_Jason_RPGMapping.GameRunning
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
                        int[] leftOfCurrentPosition = new int[2];
                        leftOfCurrentPosition[0] = i;
                        leftOfCurrentPosition[1] = j - 1;
                        int[] northOfCurrentPosition = new int[2];
                        northOfCurrentPosition[0] = i + 1;
                        northOfCurrentPosition[1] = j;
                        int[] southOfCurrentPosition = new int[2];
                        southOfCurrentPosition[0] = i - 1;
                        southOfCurrentPosition[1] = j;
                        
                        if (map.MapArrayOfArrays[i][j] == 0 && !map.IsSurrounded(map.MapArrayOfArrays, currentPosition, Wall.Value)
                            && !map.IsNextTo(map.MapArrayOfArrays, rightOfCurrentPosition, Wall.Value))
                        {
                            returnedInt[0] = i;
                            returnedInt[1] = j;
                            if (!map.IsNextTo(map.MapArrayOfArrays, currentPosition, TownMapTile.Value) 
                                && !map.IsNextTo(map.MapArrayOfArrays, currentPosition, CaveTile.Value)
                                && !map.IsNextTo(map.MapArrayOfArrays, rightOfCurrentPosition, TownMapTile.Value)
                                && !map.IsNextTo(map.MapArrayOfArrays, rightOfCurrentPosition, CaveTile.Value)
                                && !map.IsNextTo(map.MapArrayOfArrays, leftOfCurrentPosition, TownMapTile.Value)
                                && !map.IsNextTo(map.MapArrayOfArrays, leftOfCurrentPosition, CaveTile.Value)
                                && !map.IsNextTo(map.MapArrayOfArrays, northOfCurrentPosition, TownMapTile.Value)
                                && !map.IsNextTo(map.MapArrayOfArrays, northOfCurrentPosition, CaveTile.Value)
                                && !map.IsNextTo(map.MapArrayOfArrays, southOfCurrentPosition, TownMapTile.Value)
                                && !map.IsNextTo(map.MapArrayOfArrays, southOfCurrentPosition, CaveTile.Value)
                                && map.MapArrayOfArrays[i][j] != CaveTile.Value
                                && map.MapArrayOfArrays[i][j] != TownMapTile.Value)
                            {
                                tilePlaced = true;
                                break;
                            }
                        }
                    }
                    if (i == returnedInt[0])
                    {
                        break;
                    }
                }
            } while (!tilePlaced);

            return returnedInt;
        }

        public static int[] StartingPostion(Map map, ICharacter droneToPlace)
        {
            int[] returnedInt = new int[2];
            bool dronePlaced = false;
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
                            map.MapArrayOfArrays[i][j] = droneToPlace.Value;
                            returnedInt[0] = i;
                            returnedInt[1] = j;
                            dronePlaced = true;
                            break;
                        }
                    }
                    if (i == returnedInt[0])
                    {
                        break;
                    }
                }
            } while (!dronePlaced);

            return returnedInt;
        }
    }
}
