using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BrandoJason_RPGEncounterLogic.Display
{
    public static class DisplayMethods
    {
        public static void DisplayInformation(List<string> prompts)
        {
            foreach (string prompt in prompts)
            {
                Console.WriteLine(prompt);
            }
        }

        public static void DisplayInformation(List<string> prompts, int timeToWaitBetweenDisplay)
        {
            foreach (string prompt in prompts)
            {
                Console.WriteLine(prompt);
                System.Threading.Thread.Sleep(timeToWaitBetweenDisplay);
            }
        }

        public static void DisplayInformation(Dictionary<string, int> prompts)
        {
            foreach (KeyValuePair<string, int> kvp in prompts)
            {
                Console.WriteLine($"{kvp.Key} | {kvp.Value}");
            }
        }

        public static void DisplayInformation(string prompt)
        {
            WordWrap(prompt);
        }

        public static string DisplayInformation(string prompt, bool userInput)
        {
            Console.WriteLine(prompt);

            string input = "";
            input = Console.ReadLine();
            return input;
        }

        public static void DisplayInformation(string prompt, int timeToWaitBetweenEachLetter)
        {
            foreach (char character in prompt)
            {
                Console.Write(character);
                System.Threading.Thread.Sleep(timeToWaitBetweenEachLetter);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Displays information
        /// </summary>
        /// <param name="prompts"></param>
        /// <param name="returnString">uses the overload that does user input</param>
        /// <returns></returns>
        public static string DisplayInformation(List<string> prompts, bool userInput)
        {
            foreach (string prompt in prompts)
            {
                Console.WriteLine(prompt);
            }
            string input = "";
            while (input.Length == 0)
            {
                input = Console.ReadLine();
            }

            return input;
        }

        private static void WordWrap(string paragraph)
        {
            paragraph = new Regex(@" {2,}").Replace(paragraph.Trim(), @" ");
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            var lines = new List<string>();
            for (var i = 0; paragraph.Length > 0; i++)
            {
                lines.Add(paragraph.Substring(0, Math.Min(Console.WindowWidth, paragraph.Length)));
                var length = lines[i].LastIndexOf(" ", StringComparison.Ordinal);
                if (length > 0)
                {
                    lines[i] = lines[i].Remove(length);
                }
                paragraph = paragraph.Substring(Math.Min(lines[i].Length + 1, paragraph.Length));
                Console.SetCursorPosition(left, top + i);
                Console.WriteLine(lines[i]);
            }
        }
    }
}
