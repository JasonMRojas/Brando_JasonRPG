using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGMapping.Entities;
using BrandoJason_RPGMapping.Entities;
using BrandoJason_RPGMapping.Mapping;

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
            while (true)
            {
                // I is the Rows
                // J is the Cols

                // Pick some random spaces within the play bounds area (ignoring the outer wall)
                Random random = new Random();
                int randomSeedI = random.Next(2, map.MapArrayOfArrays.Length - 2);
                int randomSeedJ = random.Next(2, map.MapArrayOfArrays[map.MapArrayOfArrays.Length - 2].Length - 2);
                int[] currentPosition = new int[2];
                currentPosition[0] = randomSeedI;
                currentPosition[1] = randomSeedJ;
                int[] rightOfCurrentPosition = new int[2];
                rightOfCurrentPosition[0] = randomSeedI;
                rightOfCurrentPosition[1] = randomSeedJ + 1;
                int[] leftOfCurrentPosition = new int[2];
                leftOfCurrentPosition[0] = randomSeedI;
                leftOfCurrentPosition[1] = randomSeedJ - 1;
                int[] northOfCurrentPosition = new int[2];
                northOfCurrentPosition[0] = randomSeedI + 1;
                northOfCurrentPosition[1] = randomSeedJ;
                int[] southOfCurrentPosition = new int[2];
                southOfCurrentPosition[0] = randomSeedI - 1;
                southOfCurrentPosition[1] = randomSeedJ;

                if (
                    // Make sure this spot is empty (so we can even place a tile here)
                    map.MapArrayOfArrays[randomSeedI][randomSeedJ] == 0
                    // Make sure that this potential "door" tile has at least one entry point free of walls 
                    && !map.IsSurrounded(map.MapArrayOfArrays, currentPosition, Wall.Value)
                    // Make sure that the exit (space to the right) of this potential "door" tile has open spaces to move after player exits the door
                    && !map.IsNextTo(map.MapArrayOfArrays, rightOfCurrentPosition, Wall.Value)
                )
                {
                    // Ensure that all towns and caves are at least two tiles apart
                    if (!map.IsNextTo(map.MapArrayOfArrays, currentPosition, TownMapTile.Value)
                        && !map.IsNextTo(map.MapArrayOfArrays, currentPosition, CaveTile.Value)
                        && !map.IsNextTo(map.MapArrayOfArrays, rightOfCurrentPosition, TownMapTile.Value)
                        && !map.IsNextTo(map.MapArrayOfArrays, rightOfCurrentPosition, CaveTile.Value)
                        && !map.IsNextTo(map.MapArrayOfArrays, leftOfCurrentPosition, TownMapTile.Value)
                        && !map.IsNextTo(map.MapArrayOfArrays, leftOfCurrentPosition, CaveTile.Value)
                        && !map.IsNextTo(map.MapArrayOfArrays, northOfCurrentPosition, TownMapTile.Value)
                        && !map.IsNextTo(map.MapArrayOfArrays, northOfCurrentPosition, CaveTile.Value)
                        && !map.IsNextTo(map.MapArrayOfArrays, southOfCurrentPosition, TownMapTile.Value)
                        && !map.IsNextTo(map.MapArrayOfArrays, southOfCurrentPosition, CaveTile.Value))
                    {
                        return new int[] { randomSeedI, randomSeedJ };
                    }
                }
            }
        }

        public static int[] StartingPostion(Map map, ICharacter droneToPlace)
        {
            int[] returnedInt = new int[2];
            bool dronePlaced = false;
            if (droneToPlace.GetType() == typeof(BossToken))
            {
                returnedInt[0] = map.MapArrayOfArrays.Length - 2;
                returnedInt[1] = map.MapArrayOfArrays[0].Length - 2;
                return returnedInt;
            }
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

