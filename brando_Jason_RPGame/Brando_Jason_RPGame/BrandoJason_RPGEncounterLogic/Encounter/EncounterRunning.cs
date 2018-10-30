using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BrandoJason_RPGEncounterLogic.Encounter;
using BrandoJason_RPGEncounterLogic.DAL;
using BrandoJason_RPGEncounterLogic.Monsters;
using BrandoJason_RPGEncounterLogic.Character;
using BrandoJason_RPGEncounterLogic.Display;
using BrandoJason_RPGEncounterLogic.Sound;

namespace BrandoJason_RPGEncounterLogic.Encounter
{
    public class EncounterRunning
    {
      

        public void RunEncounter(PlayerCharacter player)
        {
            ReadMonster RM = new ReadMonster();

            RunEncounterLoop(RM.GetMonster(), player);

        }

        private void RunEncounterLoop(Monster newMonster, PlayerCharacter player)
        {
            var song = new Playable(new MusicPlayer(), Track.Battle);
            song.Play();

            System.Threading.Thread.Sleep(1000);

            Console.Clear();
            while (true)
            {
                bool winState = false;
                bool loseState = false;
                bool ranAwayState = false;
                bool restartMenu = false;

                string input;
                List<string> prompts = new List<string>()
                {
                    $"You face the mighty {newMonster.Name}",
                    $"HP: {newMonster.CurrentHP}",
                    $"Attack: {newMonster.Attack}",
                    "",
                    "Choose your choice...",
                    $"(1) Attack the {newMonster.Name}",
                    $"(2) Use a Spell on the {newMonster.Name}",
                    $"(R) Attempt to run away based on Luck Stat",
                    "",
                    "",
                    $"{player.Name} | HP: {player.CurrentHP} | MP: {player.CurrentMP} | Stam: {player.CurrentStamina}",
                    "Player Input (1, 2):"
                };
                while (true)
                {
                    input = DisplayMethods.DisplayInformation(prompts, true);
                    input = input.Substring(0, 1);
                    input = input.ToUpper();
                    if (input == "1" || input == "2" || input == "R")
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
                    case "2":
                        {
                            bool doAttack = false;
                            while (!doAttack)
                            {
                                List<string> magicPrompts = new List<string>()
                                {
                                "Choose a spell to cast: "
                                };
                                for (int i = 0; i < player.Spells.Count; i++)
                                {
                                    magicPrompts.Add($"({i + 1}) {player.Spells[i].Name}");
                                }
                                magicPrompts.Add("(Q) Quit Back to Select");
                                magicPrompts.Add("Player Input: ");
                                string magicInput = DisplayMethods.DisplayInformation(magicPrompts, true);
                                magicInput = magicInput.Substring(0, 1);

                                if (magicInput.ToUpper() == "Q")
                                {
                                    Console.Clear();
                                    restartMenu = true;
                                    break;
                                }

                                for (int i = 0; i < player.Spells.Count; i++)
                                {
                                    if ((i+1).ToString() == magicInput)
                                    {
                                        doAttack = true;
                                    }
                                }

                                if (doAttack)
                                {
                                    if (player.CurrentMP >= player.Spells[int.Parse(magicInput) - 1].MpCost)
                                    {
                                        player.MagicalAttack(newMonster, player.Spells[int.Parse(magicInput) - 1].Name);
                                    }
                                    else
                                    {
                                        doAttack = false;
                                        DisplayMethods.DisplayInformation("You do not have enough mana!!!");
                                        DisplayMethods.DisplayInformation("Press enter to continue...", true);
                                    }
                                }
                                Console.Clear();
                            }
                            break;
                        }
                    case "R":
                        Random runAwayChance = new Random();
                        if (runAwayChance.Next(0, newMonster.Luck) < runAwayChance.Next(0, player.Luck))
                        {
                            DisplayMethods.DisplayInformation("Successfully got away!!!");
                            ranAwayState = true;
                        }
                        else
                        {
                            DisplayMethods.DisplayInformation("Failed to get away!!!");
                        }
                        break;
                }

                if (restartMenu)
                {
                    continue;
                }

                if(ranAwayState)
                {
                    song.Stop();
                    break;
                }

                winState = newMonster.CurrentHP <= 0;

                DisplayMethods.DisplayInformation("");
                if (winState)
                {
                    DisplayMethods.DisplayInformation($"Beat {newMonster.Name} Yay.");
                    player.AwardExp(newMonster.Exp);
                    Console.Clear();
                    song.Stop();
                    player.CheckIfLevelUp();
                    break;
                }
                newMonster.Act(player);
                DisplayMethods.DisplayInformation("Press Enter to Continue...", true);
                Console.Clear();

                loseState = player.CurrentHP <= 0;



                if (loseState)
                {
                    DisplayMethods.DisplayInformation("You lose");
                    song.Stop();
                    break;
                }
                prompts.Clear();
            }
            DisplayMethods.DisplayInformation("Encounter Finished");
            DisplayMethods.DisplayInformation("Press Enter to Continue...", true);
            song.Stop();
        }
    }
}
