using System;
using System.Collections.Generic;
using System.Text;


namespace BrandoJason_RPGEncounterLogic.Character
{
    public class Item : IItem
    {
        public string ItemName { get; }
        public bool IsInPlayerInventory { get; set; }
    }
}