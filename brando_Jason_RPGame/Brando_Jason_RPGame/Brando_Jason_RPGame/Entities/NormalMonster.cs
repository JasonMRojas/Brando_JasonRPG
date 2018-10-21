using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGame.Mapping;

namespace Brando_Jason_RPGame.Entities
{
    public class NormalMonster : Drone
    {
        public override int Value { get; }

        public string Display { get; }

        public NormalMonster(Map map)
        {
            this.Value = -3;
            this.Display = "D";
        }

        public override void Move(Map map)
        {
            Random random = new Random();
            int[] pDronePos = new int[2];
            pDronePos[0] = this.Position[0];
            pDronePos[1] = this.Position[1];
            bool notStuck = false;
            int howLongStuck = 0;
            do
            {
                int nextDirection = random.Next(1, 5);
                switch (nextDirection)
                {
                    case 1:
                        if (map.MapArrayOfArrays[Position[0] + 1][Position[1]] != Wall.Value
                            && map.MapArrayOfArrays[Position[0] + 1][Position[1]] != 2)
                        {
                            Position[0]++;
                            notStuck = true;
                        }
                        else
                        {
                            notStuck = false;
                        }
                        break;
                    case 2:
                        if (map.MapArrayOfArrays[Position[0] - 1][Position[1]] != Wall.Value
                            && map.MapArrayOfArrays[Position[0] - 1][Position[1]] != 2)
                        {
                            Position[0]--;
                            notStuck = true;
                        }
                        else
                        {
                            notStuck = false;
                        }
                        break;
                    case 3:
                        if (map.MapArrayOfArrays[Position[0]][Position[1] - 1] != Wall.Value
                            && map.MapArrayOfArrays[Position[0]][Position[1] - 1] != 2)
                        {
                            Position[1]--;
                            notStuck = true;
                        }
                        else
                        {
                            notStuck = false;
                        }
                        break;
                    case 4:
                        if (map.MapArrayOfArrays[Position[0]][Position[1] + 1] != Wall.Value
                            && map.MapArrayOfArrays[Position[0]][Position[1] + 1] != 2)
                        {
                            Position[1]++;
                            notStuck = true;
                        }
                        else
                        {
                            notStuck = false;
                        }
                        break;
                }

                howLongStuck++;

            } while (!notStuck && howLongStuck < 20);
            if (this.Position[0] != pDronePos[0] || this.Position[1] != pDronePos[1])
            {
                map.UpdateDronePostion(pDronePos, this);
            }
        }
    }
}
