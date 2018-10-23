using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGMapping.Entities;

namespace Brando_Jason_RPGMapping.Mapping
{
    public class Map
    {
        /// <summary>
        /// Holds the display in the form of a string
        /// </summary>
        public string Display { get; private set; }

        /// <summary>
        /// Holds the values in the map in an Array of Arrays. Its is basically a grid
        /// </summary>
        public int[][] MapArrayOfArrays { get; protected set; }

        /// <summary>
        /// Holds the height of the grid
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Holds the width of the grid
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Builds the map based off of the character and the height and width
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="character"></param>
        public Map(int width, int height)
        {
            this.Height = height;
            this.Width = width;
            this.MapArrayOfArrays = BuildMapArrayEntirely();
            this.MapArrayOfArrays[1][1] = -2;
        }

        public void SetEntityPosition(ICharacter entity)
        {
            MapArrayOfArrays[entity.Position[0]][entity.Position[1]] = entity.Value;
        }

        public void SetTilePosition(int[] position, int value)
        {
            MapArrayOfArrays[position[0]][position[1]] = value;
        }

        /// <summary>
        /// Updates the positions of the various drones on the map values
        /// </summary>
        /// <param name="pDronePos"></param>
        /// <param name="newDronePosition"></param>
        public void UpdateDronePostion(int[] pDronePos, Drone newDronePosition)
        {
            this.MapArrayOfArrays[newDronePosition.Position[0]][newDronePosition.Position[1]] = newDronePosition.Value;
            this.MapArrayOfArrays[pDronePos[0]][pDronePos[1]] = Tile.Value;
        }

        /// <summary>
        /// Updates the characters position.
        /// </summary>
        /// <param name="pCharacterPos"></param>
        /// <param name="newCharacterPosition"></param>
        public void UpdateCharacterPostion(int[] pCharacterPos, PlayerToken newCharacterPosition)
        {
            this.MapArrayOfArrays[newCharacterPosition.Position[0]][newCharacterPosition.Position[1]] = newCharacterPosition.Value;
            this.MapArrayOfArrays[pCharacterPos[0]][pCharacterPos[1]] = Tile.Value;
        }

        /// <summary>
        /// Runs the various methods to build the map array
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        private int[][] BuildMapArrayEntirely()
        {
            do
            {
                BuildMapGrid();
                BuildMazeInGrid();
            } while (!IsValidMaze());

            return this.MapArrayOfArrays;
        }

        /// <summary>
        /// Carves the maze into the grid. Uses a robot to randomly do it
        /// </summary>
        protected virtual void BuildMazeInGrid()
        {
            ConstructorDrone constructor = new ConstructorDrone();
            Random randomSeed = new Random();
            for (int i = 0; i < this.MapArrayOfArrays.Length; i++)
            {
                for (int j = 0; j < this.MapArrayOfArrays[i].Length * 4; j++)
                {
                    int randomSeedNumber = randomSeed.Next(0, 3);
                    constructor.Move(this);
                    if (this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] != ExitTile.Value)
                    {
                        if (!IsSurrounded(this.MapArrayOfArrays, constructor, Tile.Value))
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = Tile.Value;
                        }
                        else
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = Wall.Value;
                        }
                        if (this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] == Wall.Value && IsSurrounded(this.MapArrayOfArrays, constructor, Tile.Value) && randomSeedNumber < 2)
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = Tile.Value;
                        }
                        if (this.IsNextTo(this.MapArrayOfArrays, constructor, ExitTile.Value))
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = Tile.Value;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a drone is surrounded by a tile value
        /// </summary>
        /// <param name="mapMazeArray"></param>
        /// <param name="constructor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static bool IsSurrounded(int[][] mapMazeArray, ConstructorDrone constructor, int value)
        {
            return mapMazeArray[constructor.Position[0] + 1][constructor.Position[1]] == value
                                && mapMazeArray[constructor.Position[0]][constructor.Position[1] + 1] == value
                                && mapMazeArray[constructor.Position[0]][constructor.Position[1] - 1] == value
                                && mapMazeArray[constructor.Position[0] - 1][constructor.Position[1]] == value;
        }

        /// <summary>
        /// Checks if a drone is next to a tile value
        /// </summary>
        /// <param name="mapMazeArray"></param>
        /// <param name="constructor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsNextTo(int[][] mapMazeArray, ConstructorDrone constructor, int value)
        {
            return mapMazeArray[constructor.Position[0] + 1][constructor.Position[1]] == value
                                || mapMazeArray[constructor.Position[0]][constructor.Position[1] + 1] == value
                                || mapMazeArray[constructor.Position[0]][constructor.Position[1] - 1] == value
                                || mapMazeArray[constructor.Position[0] - 1][constructor.Position[1]] == value;
        }

        public bool IsNextTo(int[][] mapMazeArray, int[] position, int value)
        {
            return mapMazeArray[position[0] + 1][position[1]] == value
                                || mapMazeArray[position[0]][position[1] + 1] == value
                                || mapMazeArray[position[0]][position[1] - 1] == value
                                || mapMazeArray[position[0] - 1][position[1]] == value;
        }


        /// <summary>
        /// Checks if a generic position is surrounded by a value
        /// </summary>
        /// <param name="mapMazeArray"></param>
        /// <param name="position"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsSurrounded(int[][] mapMazeArray, int[] position, int value)
        {
            return mapMazeArray[position[0] + 1][position[1]] == value
                                && mapMazeArray[position[0]][position[1] + 1] == value
                                && mapMazeArray[position[0]][position[1] - 1] == value
                                && mapMazeArray[position[0] - 1][position[1]] == value;
        }

        /// <summary>
        /// Checks to see if the current maze combination is valid, IE the start can get to the end.
        /// </summary>
        /// <returns></returns>
        private bool IsValidMaze()
        {
            // Loop over rows and then columns.
            bool isValid = false;
            TestDrone tester = new TestDrone();
            int moveCounter = 0;
            double xCounter = 0;
            for (int i = 0; i < this.MapArrayOfArrays.Length - 1; i++)
            {
                for (int j = 0; j < this.MapArrayOfArrays[i].Length - 1; j++)
                {
                    if (this.MapArrayOfArrays[i][j] == Wall.Value && i != 0 && j != 0)
                    {
                        xCounter++;
                    }
                }
            }
            while (this.MapArrayOfArrays[tester.Position[0]][tester.Position[1]] != ExitTile.Value && moveCounter < (this.MapArrayOfArrays.Length * 5 * this.MapArrayOfArrays[0].Length * 5))
            {
                tester.Move(this);
                if (this.MapArrayOfArrays[tester.Position[0]][tester.Position[1]] == ExitTile.Value)
                {
                    isValid = true;
                }
                moveCounter++;
            }
            return isValid;
        }

        /// <summary>
        /// Constructs a grid of walls at the size designated by the height and width
        /// </summary>
        private void BuildMapGrid()
        {
            int[][] mapArray = new int[this.Height][];
            for (int i = 0; i < mapArray.Length; i++)
            {
                //int[] row = new int[mapArray.Length]; Not yet
                int[] row = new int[this.Width];
                for (int j = 0; j < row.Length; j++)
                {
                    if (i == 1 && j == 1)
                    {
                        row[j] = -2;
                    }
                    else if (j == row.Length - 2 && i == mapArray.Length - 2)
                    {
                        row[j] = ExitTile.Value;
                    }
                    else
                    {
                        row[j] = Wall.Value;
                    }
                }
                mapArray[i] = row;
            }
            this.MapArrayOfArrays = mapArray;
        }

        /// <summary>
        /// Logic for turning the values within the map into the string for display
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public void BuildMapDisplay()
        {
            string mapDisplay = "";
            for (int i = 0; i < this.MapArrayOfArrays.Length; i++)
            {
                int[] row = this.MapArrayOfArrays[i];
                for (int j = 0; j < row.Length; j++)
                {
                    if (row[j] == Wall.Value)
                    {
                        mapDisplay += Wall.Display;
                    }

                    else if (row[j] == -2)
                    {
                        mapDisplay += "P";
                    }
                    else if (row[j] == -3)
                    {
                        mapDisplay += "D";
                    }
                    else if (row[j] == -4)
                    {
                        mapDisplay += "G";
                    }
                    else if (row[j] == -5)
                    {
                        mapDisplay += "H";
                    }
                    else if (row[j] == CaveTile.Value)
                    {
                        mapDisplay += CaveTile.Display;
                    }
                    else if (row[j] == TownMapTile.Value)
                    {
                        mapDisplay += TownMapTile.Display;
                    }
                    else if (row[j] == Tile.Value)
                    {
                        mapDisplay += Tile.Display;
                    }
                    else if (row[j] == ExitTile.Value)
                    {
                        mapDisplay += ExitTile.Display;
                    }

                }
                mapDisplay += "\n\r";
            }
            this.Display = mapDisplay;
        }
    }
}
