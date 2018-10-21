using System;
using System.Collections.Generic;
using System.Text;

namespace Brando_Jason_RPGame.Mapping
{
    public abstract class Display_Map
    {
        public static void DisplayMap(Map map)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0); //Basically works as buffering
            Console.CursorVisible = false;
            //foreach (char letter in map.Display) //Prints out the map one letter at a time. So that we can color certain parts
            //{
            //    if (letter == 'P')
            //    {
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        Console.Write(letter);
            //    }
            //    else if (letter == 'D')
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.Write(letter);
            //    }
            //    else if (letter == 'G')
            //    {
            //        Console.ForegroundColor = ConsoleColor.DarkMagenta;
            //        Console.Write(letter);
            //    }
            //    else if (letter == 'H')
            //    {
            //        Console.ForegroundColor = ConsoleColor.DarkYellow;
            //        Console.Write(letter);
            //    }
            //    else if (letter == 'E')
            //    {
            //        Console.ForegroundColor = ConsoleColor.Cyan;
            //        Console.Write(letter);
            //    }
            //    else if (letter == 'T')
            //    {
            //        Console.ForegroundColor = ConsoleColor.Blue;
            //        Console.Write(letter);
            //    }
            //    else
            //    {
            //        Console.Write(letter);
            //    }
            //    Console.ResetColor();
            //}
            string[] rowArray = map.Display.Split("\n\r");
            for (int i = 0; i < rowArray.Length; i++)
            {
                string row = rowArray[i];
                for (int j = 0; j < row.Length;)
                {
                    bool didThing = false;
                    if ((i == 0 || i == rowArray.Length - 1) || (!row.Contains('P') && !row.Contains('T') && !row.Contains('E') && !row.Contains('C') && !row.Contains('D')))
                    {
                        Console.Write(row);
                        break;
                    }
                    else if (row[j] == 'P')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(row[j]);
                        row = row.Substring(row.IndexOf('P') + 1);
                        didThing = true;
                    }
                    else if (row[j] == 'T')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(row[j]);
                        row = row.Substring(row.IndexOf('T') + 1);
                        didThing = true;
                    }
                    else if (row[j] == 'E')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(row[j]);
                        row = row.Substring(row.IndexOf('E') + 1);
                        didThing = true;
                    }
                    else if (row[j] == 'C')
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(row[j]);
                        row = row.Substring(row.IndexOf('C') + 1);
                        didThing = true;
                    }
                    else if (row[j] == 'D')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(row[j]);
                        row = row.Substring(row.IndexOf('D') + 1);
                        didThing = true;
                    }
                    else
                    {
                        Console.Write(row[j]);
                    }
                    Console.ResetColor();
                    j++;
                    if (didThing)
                    {
                        j = 0;
                    }
                }
                Console.WriteLine();

            }
            Console.WriteLine("Controls are arrow keys or (WASD)");
            Console.Write("YOU ARE THE: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("P");
            Console.ResetColor();

        }
    }
}
