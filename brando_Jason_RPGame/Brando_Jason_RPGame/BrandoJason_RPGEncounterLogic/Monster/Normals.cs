using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BrandoJason_RPGEncounterLogic.Character;
using BrandoJason_RPGEncounterLogic.Display;


namespace BrandoJason_RPGEncounterLogic.Monster
{
    public class Normals
    {
        public int MonsterID { get; }
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
        public int Exp { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        //public Ability MonsterAbilities { get; }

        public Normals(DataRow monster) //DataTable abilities)
        {

            this.MonsterID = int.Parse(monster["monster_id"].ToString());
            this.Name = monster["name"].ToString();
            this.CurrentHP = int.Parse(monster["hp"].ToString());
            this.MaxHP = int.Parse(monster["hp"].ToString());
            this.CurrentMP = int.Parse(monster["mp"].ToString());
            this.MaxMP = int.Parse(monster["mp"].ToString());
            this.CurrentStamina = int.Parse(monster["stam"].ToString());
            this.MaxStamina = int.Parse(monster["stam"].ToString());
            this.Defense = int.Parse(monster["def"].ToString());
            this.Attack = int.Parse(monster["atk"].ToString());
            this.MagAttack = int.Parse(monster["matk"].ToString());
            this.MagDefense = int.Parse(monster["mdef"].ToString());
            this.Luck = int.Parse(monster["luck"].ToString());
            this.Exp = int.Parse(monster["exp"].ToString());
            this.Gold = int.Parse(monster["gold"].ToString());
            this.Level = int.Parse(monster["lvl"].ToString());

            //this.MonsterAbilities = new Ability(this.MonsterID);
        }

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
                $"Wow such spook"
            };

            DisplayMethods.DisplayInformation(prompts);
            System.Threading.Thread.Sleep(1000);
        }
    }
}
