using System;
using System.Collections.Generic;
using System.Text;

namespace BrandoJason_RPGEncounterLogic.Character
{
    public class Equip : IItem
    {
        public bool IsInPlayerInventory { get; set; }

        public string ItemName { get; }

        public Dictionary<string, int> ItemTypeStat { get; }

        public Equip(Dictionary<string, int> itemStatDic)
        {
            this.ItemTypeStat= itemStatDic;
        }
    }
}