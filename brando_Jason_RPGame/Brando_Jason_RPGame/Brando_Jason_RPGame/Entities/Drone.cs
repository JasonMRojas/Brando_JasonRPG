using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGMapping.Mapping;

namespace Brando_Jason_RPGMapping.Entities
{
    public class Drone : ICharacter
    {
        public int[] Position { get; set; }

        public virtual int Value { get; }

        public virtual int EncounterLevel { get; }

        public bool DidMove { get; set; }

        public Drone()
        {
            Position = new int[] { 1, 1 };
        }

        public virtual void Move(Map map)
        {
            Random random = new Random();
            int nextDirection = random.Next(1, 11);

            switch (nextDirection)
            {
                case 1:

                    if (Position[0] + 1 < map.MapArrayOfArrays.Length - 1)
                    {
                        Position[0]++;
                    }
                    break;
                case 2:

                    if (Position[0] - 1 > 0)
                    {
                        Position[0]--;
                    }
                    break;
                case 9:
                case 7:
                case 5:
                case 3:
                    if (Position[1] - 1 > 0)
                    {
                        Position[1]--;
                    }
                    break;
                case 10:
                case 8:
                case 6:
                case 4:
                    if (Position[1] + 1 < map.MapArrayOfArrays[0].Length - 1)
                    {
                        Position[1]++;
                    }
                    break;

            }
        }
    }
}
