using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGMapping.Mapping;

namespace Brando_Jason_RPGMapping.Entities
{
    public class NormalMonster : Drone
    {
        public override int Value { get; }

        public string Display { get; }

        public override int EncounterLevel { get; }

        public int StuckCount { get; private set; }

        public NormalMonster()
        {
            this.Value = -3;
            this.Display = "D";
            this.EncounterLevel = 1;
            this.StuckCount = 0;
        }

        public override void Move(Map map)
        {
            int[] playerPosition = new int[2];
            int[] pDronePos = new int[2];
            pDronePos[0] = this.Position[0];
            pDronePos[1] = this.Position[1];

            for (int i = 1; i < map.MapArrayOfArrays.Length - 1; i++)
            {
                for (int j = 1; j < map.MapArrayOfArrays[i].Length - 1; j++)
                {
                    if (map.MapArrayOfArrays[i][j] == -2)
                    {
                        playerPosition[0] = i;
                        playerPosition[1] = j;
                    }
                }
            }

            if ((Math.Abs(playerPosition[0] - this.Position[0]) < map.MapArrayOfArrays.Length / 6 || Math.Abs(playerPosition[1] - this.Position[1]) < map.MapArrayOfArrays[0].Length / 6)&& StuckCount <= 5)
            {
                DoPlayerTrackingMovement(map, playerPosition);
            }
            else
            {

                Random random = new Random();
                int nextDirection = random.Next(1, 5);
                switch (nextDirection)
                {
                    case 1:
                        if (map.MapArrayOfArrays[Position[0] + 1][Position[1]] != Wall.Value
                            && map.MapArrayOfArrays[Position[0] + 1][Position[1]] != ExitTile.Value)
                        {
                            Position[0]++;
                        }
                        break;
                    case 2:
                        if (map.MapArrayOfArrays[Position[0] - 1][Position[1]] != Wall.Value
                            && map.MapArrayOfArrays[Position[0] - 1][Position[1]] != ExitTile.Value)
                        {
                            Position[0]--;
                        }
                        break;
                    case 3:
                        if (map.MapArrayOfArrays[Position[0]][Position[1] - 1] != Wall.Value
                            && map.MapArrayOfArrays[Position[0]][Position[1] - 1] != ExitTile.Value)
                        {
                            Position[1]--;
                        }
                        break;
                    case 4:
                        if (map.MapArrayOfArrays[Position[0]][Position[1] + 1] != Wall.Value
                            && map.MapArrayOfArrays[Position[0]][Position[1] + 1] != ExitTile.Value)
                        {
                            Position[1]++;
                        }
                        break;
                }
            }
            if (this.Position[0] != pDronePos[0] || this.Position[1] != pDronePos[1])
            {
                map.UpdateDronePostion(pDronePos, this);
                StuckCount = 0;
            }
            else
            {
                if (this.StuckCount >= 5)
                {
                    this.StuckCount -= 2;
                }
                else
                {
                    this.StuckCount = 0;
                }
            }
        }

        private void DoPlayerTrackingMovement(Map map, int[] playerPosition)
        {
            if (Math.Abs(playerPosition[0] - this.Position[0]) > Math.Abs(playerPosition[1] - this.Position[1])
                                && ((map.MapArrayOfArrays[Position[0] - 1][Position[1]] != Wall.Value
                                && map.MapArrayOfArrays[Position[0] - 1][Position[1]] != ExitTile.Value)
                                || (map.MapArrayOfArrays[Position[0] + 1][Position[1]] != Wall.Value
                                && map.MapArrayOfArrays[Position[0] + 1][Position[1]] != ExitTile.Value)))
            {
                if (playerPosition[0] - this.Position[0] < 1 && StuckCount <= 2)
                {
                    if (map.MapArrayOfArrays[Position[0] - 1][Position[1]] != Wall.Value
                    && map.MapArrayOfArrays[Position[0] - 1][Position[1]] != ExitTile.Value)
                    {
                        this.Position[0]--;
                    }
                    else if (playerPosition[1] - this.Position[1] < 1)
                    {
                        if (map.MapArrayOfArrays[Position[0]][Position[1] - 1] != Wall.Value
                    && map.MapArrayOfArrays[Position[0]][Position[1] - 1] != ExitTile.Value)
                        {
                            this.Position[1]--;
                        }
                    }
                    else if (map.MapArrayOfArrays[Position[0]][Position[1] + 1] != Wall.Value
                         && map.MapArrayOfArrays[Position[0]][Position[1] + 1] != ExitTile.Value)
                    {
                        this.Position[1]++;
                    }
                }
                else if (map.MapArrayOfArrays[Position[0] + 1][Position[1]] != Wall.Value
                         && map.MapArrayOfArrays[Position[0] + 1][Position[1]] != ExitTile.Value && StuckCount <= 2)
                {
                    this.Position[0]++;
                }
                else
                {
                    if (playerPosition[1] - this.Position[1] < 1)
                    {
                        if (map.MapArrayOfArrays[Position[0]][Position[1] - 1] != Wall.Value
                    && map.MapArrayOfArrays[Position[0]][Position[1] - 1] != ExitTile.Value)
                        {
                            this.Position[1]--;
                        }
                    }
                    else if (map.MapArrayOfArrays[Position[0]][Position[1] + 1] != Wall.Value
                         && map.MapArrayOfArrays[Position[0]][Position[1] + 1] != ExitTile.Value)
                    {
                        this.Position[1]++;
                    }
                }
            }
            else if ((map.MapArrayOfArrays[Position[0]][Position[1] - 1] != Wall.Value
                   && map.MapArrayOfArrays[Position[0]][Position[1] - 1] != ExitTile.Value)
                   || (map.MapArrayOfArrays[Position[0]][Position[1] + 1] != Wall.Value
                         && map.MapArrayOfArrays[Position[0]][Position[1] + 1] != ExitTile.Value))
            {
                if (playerPosition[1] - this.Position[1] < 1 && StuckCount <= 2)
                {
                    if (map.MapArrayOfArrays[Position[0]][Position[1] - 1] != Wall.Value
                    && map.MapArrayOfArrays[Position[0]][Position[1] - 1] != ExitTile.Value)
                    {
                        this.Position[1]--;
                    }
                    else if (playerPosition[0] - this.Position[0] < 1)
                    {
                        if (map.MapArrayOfArrays[Position[0] - 1][Position[1]] != Wall.Value
                    && map.MapArrayOfArrays[Position[0] - 1][Position[1]] != ExitTile.Value)
                        {
                            this.Position[0]--;
                        }
                    }
                    else if (map.MapArrayOfArrays[Position[0] + 1][Position[1]] != Wall.Value
                         && map.MapArrayOfArrays[Position[0] + 1][Position[1]] != ExitTile.Value)
                    {
                        this.Position[0]++;
                    }
                }
                else if (map.MapArrayOfArrays[Position[0]][Position[1] + 1] != Wall.Value
                         && map.MapArrayOfArrays[Position[0]][Position[1] + 1] != ExitTile.Value && StuckCount <= 2)
                {
                    this.Position[1]++;
                }
                else
                {
                    if (playerPosition[0] - this.Position[0] < 1)
                    {
                        if (map.MapArrayOfArrays[Position[0] - 1][Position[1]] != Wall.Value
                    && map.MapArrayOfArrays[Position[0] - 1][Position[1]] != ExitTile.Value)
                        {
                            this.Position[0]--;
                        }
                    }
                    else if (map.MapArrayOfArrays[Position[0] + 1][Position[1]] != Wall.Value
                         && map.MapArrayOfArrays[Position[0] + 1][Position[1]] != ExitTile.Value)
                    {
                        this.Position[0]++;
                    }
                }
            }
        }
    }
}