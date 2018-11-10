using System;
using System.Collections.Generic;
using System.Text;

namespace BrandoJason_RPGMapping.Mapping
{
    public abstract class Display_Map
    {
        public static void DisplayMap(Map map)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0); //Basically works as buffering
            Console.CursorVisible = false;
            string strPrint = "";
            foreach (char character in map.Display)
            {
                if (CheckIfSpecial(character))
                {
                    strPrint += character;
                }
                else
                {
                    Console.Write(strPrint);
                    PlaceColoredCharacter(character);
                    Console.ResetColor();
                    strPrint = "";
                }
            }
            Console.WriteLine(strPrint);

            Console.WriteLine("Controls are arrow keys or (WASD)");
            Console.WriteLine("I for Inventory || Escape to pause || V for status || O for spells");
            if (map.GetType() == typeof(TownMap))
            {
                Console.WriteLine("Stamina and HP Replenished!!!");
            }
            else
            {
                Console.WriteLine("Replenish Stamina to Full by being in town");
                Console.WriteLine("Replenish half of your hp by being in town");
            }
            Console.Write("YOU ARE THE: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("P");
            Console.ResetColor();


        }

        private static bool CheckIfSpecial(char character)
        {
            return character != 'P' && character != 'D' && character != 'C' && character != 'E' && character != 'T' && character != 'I' && character != 'B';
        }

        private static void PlaceColoredCharacter(char character)
        {
            if (character == 'P')
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(character);
            }
            else if (character == 'T')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(character);
            }
            else if (character == 'E')
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(character);
            }
            else if (character == 'C')
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(character);
            }
            else if (character == 'D')
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(character);
            }
            else if (character == 'B')
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(character);
            }
            else if (character == 'I')
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(character);
            }
        }
    }
}
