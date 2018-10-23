using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGMapping.Entities;

namespace Brando_Jason_RPGMapping.Mapping
{
    public class CaveMap : Map
    {
        public CaveMap(int width, int height) : base(width, height)
        {
            this.MapArrayOfArrays[MapArrayOfArrays.Length - 2][MapArrayOfArrays[0].Length - 2] = Tile.Value;
        }

        protected override void BuildMazeInGrid()
        {
            ConstructorDrone constructor = new ConstructorDrone();
            Random randomSeed = new Random();
            for (int i = 0; i < this.MapArrayOfArrays.Length; i++)
            {
                for (int j = 0; j < this.MapArrayOfArrays[i].Length * 4; j++)
                {
                    int randomSeedNumber = randomSeed.Next(0, 5);
                    constructor.Move(this);
                    int[] start = new int[2] { 1, 2 };
                    if (IsNextTo(this.MapArrayOfArrays, start, Wall.Value))
                    {
                        this.MapArrayOfArrays[2][2] = Tile.Value;
                        this.MapArrayOfArrays[2][3] = Tile.Value;
                        this.MapArrayOfArrays[1][3] = Tile.Value;
                    }
                    if (this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] != ExitTile.Value)
                    {
                        if (!IsSurrounded(this.MapArrayOfArrays, constructor, 0))
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = Tile.Value;
                        }
                        else
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = Wall.Value;
                        }
                        if (this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] == Wall.Value && IsSurrounded(this.MapArrayOfArrays, constructor, Tile.Value) && randomSeedNumber < 1)
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = Tile.Value;
                        }
                        if (this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] == Tile.Value
                            && IsSurrounded(this.MapArrayOfArrays, constructor, Wall.Value))
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = Wall.Value;
                        }
                        if (this.IsNextTo(this.MapArrayOfArrays, constructor, ExitTile.Value))
                        {
                            this.MapArrayOfArrays[constructor.Position[0]][constructor.Position[1]] = Tile.Value;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            for (int i = 1; i < MapArrayOfArrays.Length- 1; i++)
            {
                for (int j = 1; j < MapArrayOfArrays[i].Length - 1; j++)
                {
                    int[] pos = new int[2] { i, j };
                    if(IsSurrounded(this.MapArrayOfArrays, pos, Wall.Value))
                    {
                        this.MapArrayOfArrays[i][j] = Wall.Value;
                    }
                }
            }
        }
    }
}
