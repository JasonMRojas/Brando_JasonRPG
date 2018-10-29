using System;
using BrandoJason_RPGEncounterLogic.Character;
using System.Data.SqlClient;
using System.Data;
using BrandoJason_RPGEncounterLogic.DAL;
using BrandoJason_RPGEncounterLogic.Encounter;
using CSCore.CoreAudioAPI;
using BrandoJason_RPGEncounterLogic.Sound;
using BrandoJason_RPGEncounterLogic.Display;
using System.Collections.Generic;

namespace BrandoJason_RPGEncounterLogic
{
    public class EncounterProg
    {
        public PlayerCharacter Player { get; private set; }

        static void Main(string[] args)
        {

        }

        public void RunCharacterCreation()
        {
            List<string> getName = new List<string>()
            {
                "Enter Name: "
            };
            this.Player = new PlayerCharacter(DisplayMethods.DisplayInformation(getName, true));

            Console.Clear();

            List<string> prompts = new List<string>()
            {
                "Welcome to a Rogue Tribute",
                "Every single playthrough will be randomized and unique",
                "There are 10 Levels...",
                "Hadrian, The Stone King, Awaits you at the End...",
                "GoodLuck...",
            };

            DisplayMethods.DisplayInformation(prompts, 500);

            DisplayMethods.DisplayInformation(this.Player.Name, 300);

            DisplayMethods.DisplayInformation("Press Any Key to Continue...", true);

            Console.Clear();

            this.Player.AwardExp(1);
            this.Player.CheckIfLevelUp();         
        }

        public bool RunEncounterProg(int encounterID)
        {
            EncounterRunning newEnounter = new EncounterRunning();

            newEnounter.RunEncounter(this.Player);

            return Player.CurrentHP <= 0;
        }

        public void DoPlayerInput(ConsoleKeyInfo input)
        {
            Console.Clear();
            switch (input.Key)
            {
                case ConsoleKey.I:
                    {
                        if (this.Player.Inventory != null)
                        {
                            this.Player.ViewItemInventory();
                        }
                        else
                        {
                            DisplayMethods.DisplayInformation("Your Inventory Is Empty");
                        }
                        break;
                    }
                case ConsoleKey.V:
                    {
                        this.Player.ViewStatus();
                        break;
                    }
                case ConsoleKey.Escape:
                    {
                        DisplayMethods.DisplayInformation("Paused...");
                        break;
                    }
                case ConsoleKey.O:
                    {
                        this.Player.ViewSpells();
                        break;
                    }
            }
            DisplayMethods.DisplayInformation("Press Enter to Continue...", true);

            Console.Clear();
        }

        public void TownReplenish()
        {
            Player.ReplenishStamina();
            Player.ReplenishHp(true);
        }
    }
}
