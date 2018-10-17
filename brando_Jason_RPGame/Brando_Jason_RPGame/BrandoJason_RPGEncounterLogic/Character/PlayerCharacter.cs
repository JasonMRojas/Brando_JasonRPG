using System;
using System.Collections.Generic;
using System.Text;
using BrandoJason_RPGEncounterLogic.Display;

namespace BrandoJason_RPGEncounterLogic.Character
{
    public class PlayerCharacter
    {
        public string Name { get; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int CurrentMP { get; set; }
        public int MaxMP { get; set; }
        public int CurrentStamina { get; set; }
        public int MaxStamina { get; set; }
        public int Defense { get; set; }
        public int Attack { get; set; }
        public int MagDefense { get; set; }
        public int MagAttack { get; set; }
        public int Exp { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int MaxExp { get; }
        public int[] LevelingArray { get; }

        public List<IItem> Inventory { get; private set; }

        public PlayerCharacter(string name)
        {
            this.Name = name;
            LevelingArray = new int[] { 0, 1, 5, 15, 25, 50, 100, 150, 225, 300, 500 };
            this.Level = 0;
            this.Exp = 500;
            this.MaxExp = LevelingArray[LevelingArray.Length - 1];
        }

        public void ViewItemInventory()
        {
            Dictionary<string, int> newPrompts = new Dictionary<string, int>();
            foreach (IItem item in Inventory)
            {
                newPrompts[item.ItemName] += 1;
            }
            DisplayMethods.DisplayInformation("Item Inventory");
            DisplayMethods.DisplayInformation(newPrompts);
        }

        public void ViewStatus()
        {
            List<string> newPrompts = new List<string>
            {
                "Name: " + this.Name,
                "Level: " + this.Level,
                "Health: " + this.CurrentHP + " | " + this.MaxHP,
                "Mana: " + this.CurrentMP + " | " + this.MaxMP,
                "Stamina: " + this.CurrentStamina + " | " + this.MaxStamina,
                "Attack: " + this.Attack,
                "Defense: " + this.Defense,
                "Magic Attack: " + this.MagAttack,
                "Magic Defense: " + this.MagDefense,
                "Current Experience: " + this.Exp + "xp" + " | to next level " + (LevelingArray[this.Level] - this.Exp) + "xp",
                "Gold: " + this.Gold
            };
            DisplayMethods.DisplayInformation(newPrompts);
            DisplayMethods.DisplayInformation("Press any button to continue...");
        }

        public void AddGold (int amountGold)
        {
            this.Gold += amountGold;
        }

        public void removeGold(int amountRemoveGold)
        {

        }
        /// <summary>
        /// add item 
        /// </summary>
        public void AddItem(IItem newItem)
        {
            Inventory.Add(newItem);
        }

        public void LevelUp()
        {
            List<string> prompts = new List<string>();
            prompts.Add("Level: " + this.Level);

            Random randomLevelSeed = new Random();
            int randomHPAddition = randomLevelSeed.Next(5, 10);
            int randomMPAddition = randomLevelSeed.Next(5, 10);
            int count = 3;
            bool didLevelNineCount = false;


            while (this.LevelingArray[this.Level] < this.Exp)
            {
                if (this.Level >= 9 && !didLevelNineCount)
                {
                    count = 9;
                    didLevelNineCount = true;
                }
                prompts.Add("LEVEL UP!!!");
                prompts.Add("New Level: " + (this.Level + 1));
                prompts.Add($"HP: {this.MaxHP} + {randomHPAddition} = {(this.MaxHP + randomHPAddition)}");
                prompts.Add($"HP: {this.MaxMP} + {randomMPAddition} = {(this.MaxHP + randomMPAddition)}");

                prompts.Add($"You have {count} stats to allot: ");
                prompts.Add($"(1) Attack: {this.Attack}");
                prompts.Add($"(2) Defense: {this.Defense}");
                prompts.Add($"(3) Magic Attack: {this.MagAttack}");
                prompts.Add($"(4) Magic Defense: {this.MagDefense}");
                prompts.Add($"(5) Stamina: {this.MaxStamina}");
                prompts.Add("");
                prompts.Add("Player input (1,2,3,4,5): ");


                string input;
                while (true)
                {
                    input = DisplayMethods.DisplayInformation(prompts, true);
                    input = input.Substring(0, 1);
                    if (input == "1" || input == "2" || input == "3" || input == "4" || input == "5")
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
                switch (input)
                {
                    case "1":
                        this.Attack++;
                        break;
                    case "2":
                        this.Defense++;
                        break;
                    case "3":
                        this.MagAttack++;
                        break;
                    case "4":
                        this.MagDefense++;
                        break;
                    case "5":
                        this.MaxStamina++;
                        this.CurrentStamina++;
                        break;
                }
                count--;
                if (count < 1)
                {
                    this.Level++;
                    count = 3;
                    this.MaxHP += randomHPAddition;
                    this.CurrentHP += randomHPAddition;
                    this.MaxMP += randomMPAddition;
                    this.CurrentMP += randomMPAddition;
                    randomHPAddition = randomLevelSeed.Next(1, 10);
                    randomMPAddition = randomLevelSeed.Next(1, 10);
                }
                prompts.Clear();
                Console.Clear();
            }
            DisplayMethods.DisplayInformation("Finished Leveling");
            Console.ReadLine();
        }
    }
}
