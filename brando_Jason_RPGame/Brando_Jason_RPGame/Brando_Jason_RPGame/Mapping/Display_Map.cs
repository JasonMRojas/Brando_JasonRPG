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
            //for (int i = 0; i < rowArray.Length; i++)
            //{
            //    string row = rowArray[i];
            //    for (int j = 0; j < row.Length;)
            //    {
            //        bool didThing = false;
            //        if ((i == 0 || i == rowArray.Length - 1) || (!row.Contains('P') && !row.Contains('T') && !row.Contains('E') && !row.Contains('C') && !row.Contains('D')))
            //        {
            //            Console.Write(row);
            //            break;
            //        }
            //        if (row[j] == 'P')
            //        {
            //            Console.ForegroundColor = ConsoleColor.Green;
            //            Console.Write(row[j]);
            //            row = row.Substring(row.IndexOf('P') + 1);
            //            didThing = true;
            //        }
            //        else if (row[j] == 'T')
            //        {
            //            Console.ForegroundColor = ConsoleColor.Blue;
            //            Console.Write(row[j]);
            //            row = row.Substring(row.IndexOf('T') + 1);
            //            didThing = true;
            //        }
            //        else if (row[j] == 'E')
            //        {
            //            Console.ForegroundColor = ConsoleColor.Cyan;
            //            Console.Write(row[j]);
            //            row = row.Substring(row.IndexOf('E') + 1);
            //            didThing = true;
            //        }
            //        else if (row[j] == 'C')
            //        {
            //            Console.ForegroundColor = ConsoleColor.Magenta;
            //            Console.Write(row[j]);
            //            row = row.Substring(row.IndexOf('C') + 1);
            //            didThing = true;
            //        }
            //        else if (row[j] == 'D')
            //        {
            //            Console.ForegroundColor = ConsoleColor.DarkRed;
            //            Console.Write(row[j]);
            //            row = row.Substring(row.IndexOf('D') + 1);
            //            didThing = true;
            //        }
            //        else
            //        {
            //            Console.Write(row[j]);
            //        }
            //        Console.ResetColor();
            //        j++;
            //        if (didThing)
            //        {
            //            j = 0;
            //        }
            //    }
            //    Console.WriteLine();

            //}


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
            return character != 'P' && character != 'D' && character != 'C' && character != 'E' && character != 'T' && character != 'I';
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
            else if (character == 'I')
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(character);
            }
        }
    }
}
