using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BrandoJason_RPGEncounterLogic.Character;
using BrandoJason_RPGEncounterLogic.Display;
using System.IO;
using Newtonsoft.Json;

namespace BrandoJason_RPGEncounterLogic.Monsters
{
    public class Monster
    {
        public string Name { get; set; }
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
        public int Exp { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        //public Ability MonsterAbilities { get; }



        public void Act(PlayerCharacter player)
        {
            //Decide action smartly...
            this.AttackPlayer(player);
        }

        private void AttackPlayer(PlayerCharacter player)
        {
            int dealtDamage = this.Attack - player.Defense;
            if (dealtDamage < 0)
            {
                dealtDamage = 0;
            }
            player.CurrentHP -= dealtDamage;
            List<string> prompts = new List<string>()
            {
                $"The {this.Name} Attacked!!!",
                $"It dealt {dealtDamage} dmg!!!",
                $"Wow such spoop"
            };

            DisplayMethods.DisplayInformation(prompts);
            System.Threading.Thread.Sleep(1000);
        }
    }
}
