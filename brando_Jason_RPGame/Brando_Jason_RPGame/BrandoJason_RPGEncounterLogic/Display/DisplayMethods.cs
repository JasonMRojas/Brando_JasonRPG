using System;
using System.Collections.Generic;
using System.Text;

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

        public static void DisplayInformation(Dictionary<string, int> prompts)
        {
            foreach (KeyValuePair<string, int> kvp in prompts)
            {
                Console.WriteLine($"{kvp.Key} | {kvp.Value}");
            }
        }

        public static void DisplayInformation(string prompt)
        {
            Console.WriteLine(prompt);
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
            string input = Console.ReadLine();

            return input;
        }
    }
}
