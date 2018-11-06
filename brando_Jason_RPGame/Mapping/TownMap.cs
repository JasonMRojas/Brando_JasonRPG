using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGMapping.Entities;
using System.IO;

namespace BrandoJason_RPGMapping.Mapping
{
    public class TownMap : Map
    {
        public TownMap(int width, int height) : base(width, height)
        {
            this.MapArrayOfArrays = ChangeStringToArray(ReadMapFile());
            this.MapArrayOfArrays[1][1] = -2;
            //this.Display = this.BuildMapDisplay();
        }

        private string ReadMapFile()
        {
            string townMap = "";
            Random rand = new Random();
            int randomStartSpot = rand.Next(0, 11);
            using (StreamReader sr = new StreamReader("TownMaps.txt"))
            {
                string currentLine = sr.ReadLine(); // sr.ReadLine();
                while(currentLine != randomStartSpot.ToString())
                {
                    currentLine = sr.ReadLine();
                }
                currentLine = sr.ReadLine();
                while (currentLine != (randomStartSpot + 1).ToString() && !sr.EndOfStream)
                {
                    townMap += currentLine + "\n\r";
                    Console.WriteLine("Reading Line from map");
                    currentLine = sr.ReadLine();
                }
            }
            return townMap;
        }

        private int[][] ChangeStringToArray(string townMap)
        {
            string[] rowArray = townMap.Split("\n\r");
            int[][] townMapArrayArray = new int[rowArray.Length][];
            for (int i = 0; i < rowArray.Length; i++)
            {
                townMapArrayArray[i] = new int[rowArray[i].Length];
                for (int j = 0; j < rowArray[i].Length; j++)
                {
                    if (rowArray[i][j].ToString() == Wall.Display)
                    {
                        townMapArrayArray[i][j] = Wall.Value;
                    }
                    else if (rowArray[i][j].ToString() == Tile.Display)
                    {
                        townMapArrayArray[i][j] = Tile.Value;
                    }
                    else if (rowArray[i][j].ToString() == InnTile.Display)
                    {
                        townMapArrayArray[i][j] = InnTile.Value;
                    }
                }
            }
            return townMapArrayArray;
        }



    }
}
