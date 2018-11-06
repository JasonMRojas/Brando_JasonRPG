using System;

namespace BrandoJason_RPGEncounterLogic.Character
{
    public class Spell
    {
        public int Damage { get; }
        public string Name { get; }
        public int MpCost { get; }

        public Spell(string name, int damage, int mpCost)
        {
            this.Name = name;
            this.Damage = damage;
            this.MpCost = mpCost;
        }
    }
}