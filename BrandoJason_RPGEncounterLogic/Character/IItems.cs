using System;
using System.Collections.Generic;
using System.Text;

namespace BrandoJason_RPGEncounterLogic.Character
{
    public interface IItem
    {
        bool IsInPlayerInventory { get; set; }

        string ItemName { get; }
    }
}
