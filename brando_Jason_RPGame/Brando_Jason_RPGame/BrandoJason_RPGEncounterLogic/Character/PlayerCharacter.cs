using System;
using System.Collections.Generic;
using System.Text;
using BrandoJason_RPGEncounterLogic.Display;
using BrandoJason_RPGEncounterLogic.Monsters;

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
        public int Luck { get; set; }
        public int Exp { get; private set; }
        public int Gold { get; private set; }
        public int Level { get; private set; }
        public int MaxExp { get; }
        public int[] LevelingArray { get; }

        public List<IItem> Inventory { get; private set; }
        public List<Spell> Spells { get; private set; }


        public PlayerCharacter(string name)
        {
            this.Name = name;
            LevelingArray = new int[] { 0, 1, 5, 15, 25, 50, 100, 150, 225, 300, 500 };
            this.MaxStamina = 5;
            this.CurrentStamina = this.MaxStamina;
            this.Level = 0;
            this.Exp = 0;
            this.Luck = 2;
            this.MaxExp = LevelingArray[LevelingArray.Length - 1];
            this.Spells = new List<Spell>();
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
                "Luck: " + this.Luck,
                "Current Experience: " + this.Exp + "xp" + " | to next level " + (LevelingArray[this.Level] - this.Exp) + "xp",
                "Gold: " + this.Gold
            };
            DisplayMethods.DisplayInformation(newPrompts);
        }

        public void ReplenishStamina()
        {
            this.CurrentStamina = this.MaxStamina;
        }

        public void ReplenishStamina(int amountReplenish)
        {
            this.CurrentStamina += amountReplenish;
        }

        public void AddGold(int amountGold)
        {
            this.Gold += amountGold;
        }

        public void RemoveGold(int amountRemoveGold)
        {
            this.Gold -= amountRemoveGold;
        }

        public void CheckIfLevelUp()
        {
            if (this.Exp > this.LevelingArray[this.Level])
            {
                this.LevelUp();
            }
        }

        public void ReplenishHp(bool isTown)
        {
            if (this.CurrentHP < this.MaxHP / 2)
            {
                this.CurrentHP = this.MaxHP / 2;
            }
        }

        public void AwardExp(int exp)
        {
            if (this.Exp + exp <= this.MaxExp)
            {
                this.Exp += exp;
            }
            else
            {
                this.Exp = this.MaxExp;
            }
        }

        internal void ViewSpells()
        {
            List<string> prompts = new List<string>();
            foreach (Spell spell in Spells)
            {
                prompts.Add($"{spell.Name} || Cost: {spell.MpCost} || Base Damage: {spell.Damage}");
            }
            DisplayMethods.DisplayInformation(prompts);
        }

        /// <summary>
        /// add item 
        /// </summary>
        public void AddItem(IItem newItem)
        {
            Inventory.Add(newItem);
        }

        private void LevelUp()
        {
            List<string> prompts = new List<string>();
            prompts.Add("Level: " + this.Level);

            Random randomLevelSeed = new Random();
            int randomHPAddition = randomLevelSeed.Next(3, this.Luck / randomLevelSeed.Next(1, 3) + 3);
            int randomMPAddition = randomLevelSeed.Next(2, this.Luck / randomLevelSeed.Next(1, 3) + 1);
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
                prompts.Add($"MP: {this.MaxMP} + {randomMPAddition} = {(this.MaxMP + randomMPAddition)}");

                prompts.Add($"You have {count} stats to allot: ");
                prompts.Add($"(1) Attack: {this.Attack}");
                prompts.Add($"(2) Defense: {this.Defense}");
                prompts.Add($"(3) Magic Attack: {this.MagAttack}");
                prompts.Add($"(4) Magic Defense: {this.MagDefense}");
                prompts.Add($"(5) Stamina: {this.MaxStamina}");
                prompts.Add($"(6) Luck: {this.Luck}");
                prompts.Add("");
                prompts.Add("Player input (1,2,3,4,5,6): ");


                string input;
                while (true)
                {
                    input = DisplayMethods.DisplayInformation(prompts, true);
                    input = input.Substring(0, 1);
                    if (input == "1" || input == "2" || input == "3" || input == "4" || input == "5" || input == "6")
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
                    case "6":
                        this.Luck++;
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
                    randomHPAddition = randomLevelSeed.Next(3, this.Luck / randomLevelSeed.Next(1, 3) + 3);
                    randomMPAddition = randomLevelSeed.Next(2, this.Luck / randomLevelSeed.Next(1, 3) + 1);
                }
                prompts.Clear();
                Console.Clear();
            }

            if (this.Level == 1)
            {
                Spell fireBall = new Spell("FireBall", 2, 2);
                DisplayMethods.DisplayInformation($"For reaching {this.Level} level you have been awarded the {fireBall.Name} Spell || {fireBall.Damage} damage || {fireBall.MpCost} cost. ");
                this.AddSpell(fireBall);
            }

            DisplayMethods.DisplayInformation("Finished Leveling", true);
            Console.Clear();
        }

        private void AddSpell(Spell spell)
        {
            Spells.Add(spell);
        }

        public void PhysicalAttack(Monster monster)
        {
            this.CurrentStamina -= 1;
            if (this.CurrentStamina > -1)
            {
                int dealtDamage = this.Attack - monster.Defense;
                if (dealtDamage < 0)
                {
                    dealtDamage = 0;
                }
                monster.CurrentHP -= dealtDamage;
                List<string> prompts = new List<string>()
            {
                $"{this.Name} Punched the {monster.Name}",
                $"{this.Name} dealt {dealtDamage} dmg!!!",
                $"You spent 1 stamina || Current Stamina {this.CurrentStamina}",
                $"The {monster.Name} trembles... probably..."
            };
                DisplayMethods.DisplayInformation(prompts);
            }
            else
            {
                this.CurrentStamina = 0;
                DisplayMethods.DisplayInformation("You were too tired to attack... (Stamina too low)");
            }

            System.Threading.Thread.Sleep(1000);
        }

        public void MagicalAttack(Monster monster, string spellName)
        {
            Spell spellToCast = new Spell("You Have No Spells", 0, 0);
            foreach (Spell spell in Spells)
            {
                if (spellName == spell.Name)
                {
                    spellToCast = spell;
                }
            }
            int dealtDamage = (this.MagAttack + spellToCast.Damage) - monster.MagDefense;
            if (dealtDamage < 0)
            {
                dealtDamage = 0;
            }
            this.CurrentMP -= spellToCast.MpCost;
            monster.CurrentHP -= dealtDamage;

            List<string> prompts = new List<string>()
            {
                $"{this.Name} Casts: {spellToCast.Name}",
                $"{this.Name} does {dealtDamage} damage",
                $"{this.Name} used {spellToCast.MpCost} mp and now has {this.CurrentMP} mp left",
                $"The {monster.Name} steps back... probably"
            };
            DisplayMethods.DisplayInformation(prompts);
            System.Threading.Thread.Sleep(1000);
        }
    }
}
