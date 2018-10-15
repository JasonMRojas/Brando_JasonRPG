using System;
using System.Collections.Generic;
using System.Text;
using BrandoJason_RPGEncounterLogic.Display;

namespace BrandoJason_RPGEncounterLogic.Character
{
    public class Character
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
        public int[] LevelingArray { get; }

        public List<IItem> Inventory { get; private set; }

        public Character()
        {
            LevelingArray = new int[] { 0, 1, 5, 15, 25, 50, 100, 150, 225, 300 };
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
                "Health " + this.CurrentHP + " | " + this.MaxHP,
                "Mana: " + this.CurrentMP + " | " + this.MaxMP,
                "Stamina: " + this.CurrentStamina + " | " + this.MaxStamina,
                "Attack: " + this.Attack,
                "Defense: " + this.Defense,
                "Magic Attack: " + this.MagAttack,
                "Magic Defense: " + this.MagDefense,
                "Level: " + this.Level,
                "Experience: " + this.Exp + " | " + LevelingArray[this.Level],
                "Gold: " + this.Gold
            };
            DisplayMethods.DisplayInformation(newPrompts);
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
        }




    }
}
