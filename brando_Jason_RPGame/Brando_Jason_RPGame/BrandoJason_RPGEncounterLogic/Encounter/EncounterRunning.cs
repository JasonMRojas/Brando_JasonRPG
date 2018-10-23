using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BrandoJason_RPGEncounterLogic.Encounter;
using BrandoJason_RPGEncounterLogic.Database;
using BrandoJason_RPGEncounterLogic.Monster;
using BrandoJason_RPGEncounterLogic.Character;
using BrandoJason_RPGEncounterLogic.Display;

namespace BrandoJason_RPGEncounterLogic.Encounter
{
    public class EncounterRunning
    {
        RPGDateBaseStorage CurrentData { get; }
        public EncounterRunning(RPGDateBaseStorage data)
        {
            this.CurrentData = data;
        }

        public void RunEncounter(int encounterID, PlayerCharacter player)
        {
            foreach (DataRow row in CurrentData.Monster.Rows)
            {
                if (int.Parse(row["monster_id"].ToString()) == encounterID)
                {
                   Normals newMonster = new Normals(row);
                   RunEncounterLoop(newMonster, player);
                }
            }

        }

        private void RunEncounterLoop(Normals newMonster, PlayerCharacter player)
        {
            Console.Clear();
            while (true)
            {
                bool winState = false;
                bool loseState = false;
                string input;
                List<string> prompts = new List<string>()
                {
                    $"You face the mighty {newMonster.Name}",
                    $"HP: {newMonster.CurrentHP}",
                    $"Attack: {newMonster.Attack}",
                    "",
                    "Choose your choice...",
                    $"(1) Attack the {newMonster.Name}",
                    "",
                    "",
                    $"{player.Name} | HP: {player.CurrentHP} | MP: {player.CurrentMP} | Stam: {player.CurrentStamina}",
                    "Player Input (1):"
                };
                while (true)
                {
                    input = DisplayMethods.DisplayInformation(prompts, true);
                    input = input.Substring(0, 1);
                    if (input == "1")
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
                Console.Clear();
                switch (input)
                {
                    case "1":
                        player.PhysicalAttack(newMonster);
                        break;
                }
                DisplayMethods.DisplayInformation("Press any button to continue...");
                Console.Clear();
                winState = newMonster.CurrentHP <= 0;
                if (winState)
                {
                    Console.WriteLine($"Beat {newMonster.Name} Yay.");
                    break;
                }
                newMonster.Act(player);
                DisplayMethods.DisplayInformation("Press any button to continue...");
                Console.Clear();


                loseState = player.CurrentHP <= 0;



                if (loseState)
                {
                    Console.WriteLine("You fucking suck you lost to a slime nerd.");
                    break;
                }
                prompts.Clear();
            }
            Console.WriteLine("Donzo");
            Console.ReadLine();
        }
    }
}
