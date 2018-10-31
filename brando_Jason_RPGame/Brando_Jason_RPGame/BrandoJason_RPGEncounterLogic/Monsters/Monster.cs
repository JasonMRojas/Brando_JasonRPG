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
        public List<string> AbilityNames { get; set; }
        public string Lore { get; set; }
        public List<Ability> MonsterAbilities { get; set; }
        private Ability AbilityToUse { get; set; }



        public void Act(PlayerCharacter player)
        {
            //Ability usable = new Ability();
            if (this.Level <= 5)
            {
                NormalAiLogic(player);
            }
            else
            {
                NormalAiLogic(player);
            }
            this.AbilityToUse = null;
        }

        private void NormalAiLogic(PlayerCharacter player)
        {
            Random rand = new Random();
            if (player.CurrentHP - this.Attack < 1)
            {
                this.AttackPlayer(player);
            }
            else if (MonsterAbilities != null)
            {
                foreach (Ability ability in MonsterAbilities)
                {
                    if (ability.IsAttack)
                    {
                        if (ability.MP < this.CurrentMP)
                        {
                            if (player.CurrentHP - (this.MagAttack + ability.MagAttack) < 1 || player.CurrentHP - (this.Attack + ability.Attack) < 1)
                            {
                                this.AbilityToUse = ability;
                                break;
                            }
                            else if (rand.Next(1, 101) > 100 - ability.Chance)
                            {
                                this.AbilityToUse = ability;
                            }
                        }
                    }
                    else
                    {
                        if (this.CurrentHP - player.Attack < 1 || this.CurrentHP - player.MagAttack < 1)
                        {
                            if (ability.HP > 0 && rand.Next(1, 101) > 100 - ability.Chance)
                            {
                                this.AbilityToUse = ability;
                                break;
                            }
                        }
                        else
                        {
                            if (rand.Next(1, 101) > 100 - ability.Chance)
                            {
                                this.AbilityToUse = ability;
                            }
                        }
                    }
                }
                if (this.AbilityToUse != null && rand.Next(1, 101) > this.AbilityToUse.Chance)
                {
                    this.UseAbility(player);
                }
                else
                {
                    if (rand.Next(1, 101) > 50)
                    {
                        AttackPlayer(player);
                    }
                    else
                    {
                        Defend(player);
                    }
                }
            }
            else
            {
                if (rand.Next(1, 101) > 50)
                {
                    AttackPlayer(player);
                }
                else
                {
                    Defend(player);
                }
            }
        }

        private void Defend(PlayerCharacter player)
        {
            throw new NotImplementedException();
        }

        private void UseAbility(PlayerCharacter player)
        {
            if (this.AbilityToUse.IsAttack)
            {
                int physicalDealtDamage = (this.AbilityToUse.Attack + this.Attack) - player.Defense;
                int magicalDealtDamage = (this.AbilityToUse.MagAttack + this.MagAttack) - player.MagDefense;

                List<string> prompts = new List<string>()
                {
                    $"The {this.Name} used {this.AbilityToUse.Name}",
                    $"The {this.Name} {this.AbilityToUse.Dialog}"
                };

                if (this.AbilityToUse.Attack < 0)
                {

                }
                if (physicalDealtDamage > 0)
                {
                    prompts.Add($"The {this.Name} dealt {physicalDealtDamage} physical damage");
                    player.CurrentHP -= physicalDealtDamage;
                }

                if (magicalDealtDamage > 0)
                {
                    prompts.Add($"The {this.Name} dealt {magicalDealtDamage} magic damage");
                    player.CurrentHP -= magicalDealtDamage;
                }

                if (this.AbilityToUse.HP > 0)
                {
                    prompts.Add($"It healed you {this.AbilityToUse.HP} HP");
                    if (player.CurrentHP + this.AbilityToUse.HP <= player.MaxHP)
                    {
                        player.CurrentHP += this.AbilityToUse.HP;
                    }
                    else
                    {
                        player.CurrentHP = player.MaxHP;
                    }
                }
            }
            else
            {

            }
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
