using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGMapping.Mapping;
using BrandoJason_RPGEncounterLogic;

namespace Brando_Jason_RPGMapping.Entities
{
    public class PlayerToken : ICharacter
    {
        public bool DidMove { get; set; }

        public EncounterProg CurrentEncounter { get; }

        public string Name { get; }

        public int[] Position { get; set; }


        public string Display { get { return "P"; } }

        public int Value { get; }

        public PlayerToken(EncounterProg encounter)
        {
            this.Position = new int[2];
            this.Position[0] = 1; //Position of i
            this.Position[1] = 1; //Position of j
            this.Value = -2;
            this.CurrentEncounter = encounter;
        }

        public void Move(Map currentMap)
        {
            ConsoleKeyInfo input = new ConsoleKeyInfo();
            if (Console.KeyAvailable)
            {
                input = Console.ReadKey();
            }
            int pX = this.Position[1];
            int pY = this.Position[0];
            int[] pCharacterPos = new int[2];
            pCharacterPos[0] = pY;
            pCharacterPos[1] = pX;

            switch (input.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (currentMap.MapArrayOfArrays[Position[0] - 1][Position[1]] != Wall.Value)
                    {
                        this.Position[0]--;
                    }
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (currentMap.MapArrayOfArrays[Position[0]][Position[1] + 1] != Wall.Value)
                    {
                        this.Position[1]++;
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (currentMap.MapArrayOfArrays[Position[0] + 1][Position[1]] != Wall.Value)
                    {
                        this.Position[0]++;
                    }
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (currentMap.MapArrayOfArrays[Position[0]][Position[1] - 1] != Wall.Value)
                    {
                        this.Position[1]--;
                    }
                    break;
                case ConsoleKey.I:
                case ConsoleKey.V:
                case ConsoleKey.Escape:
                case ConsoleKey.O:
                    {
                        CurrentEncounter.DoPlayerInput(input);
                        Display_Map.DisplayMap(currentMap);
                        break;
                    }
                default:
                    break;
            }

            if (this.Position[0] != pCharacterPos[0] || this.Position[1] != pCharacterPos[1])
            {
                currentMap.UpdateCharacterPostion(pCharacterPos, this);
                DidMove = true;
            }
            else
            {
                DidMove = false;
            }

        }
    }
}
