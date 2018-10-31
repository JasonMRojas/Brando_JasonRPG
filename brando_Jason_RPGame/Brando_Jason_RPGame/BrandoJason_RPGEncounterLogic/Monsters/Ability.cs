using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BrandoJason_RPGEncounterLogic.Monsters
{
    public class Ability
    {
        public string Name { get; set; }
        public string Dialog { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int Stamina { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int MagAttack { get; set; }
        public int MagDefense { get; set; }
        public bool IsAttack { get; set; }
        public int Chance { get; set; }
    }
}
