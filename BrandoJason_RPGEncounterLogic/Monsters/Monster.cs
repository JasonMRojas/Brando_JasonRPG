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
        private bool IsDefend { get; set; }
        private int DefendTurnCount { get; set; }



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
                                this.CurrentMP = this.CurrentMP - ability.MPcost;
                                this.CurrentStamina -= 1;

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
            if (this.IsDefend)
            {
                this.DefendTurnCount++;
            }

            if (this.DefendTurnCount > 1)
            {
                this.Defense -= (int)(this.Level * 1.5);
                this.MagDefense -= (int)(this.Level * 1.5);
                this.IsDefend = false;
                this.DefendTurnCount = 0;
            }
        }

        private void Defend(PlayerCharacter player)
        {
            List<string> prompts = new List<string>()
                {
                    $"The {this.Name} hunkered down and guarded",
                    $"The {this.Name} restored 1 stamina and gained {(int)(this.Level * 1.5)} defense and {(int)(this.Level * 1.5)} magic defense for the next turn"
                };
            if (this.CurrentStamina + 1 <= this.MaxStamina)
            {
                this.CurrentStamina++;
            }
            else
            {
                this.CurrentStamina = this.MaxStamina;
            }

            this.Defense += (int)(this.Level * 1.5);
            this.MagDefense += (int)(this.Level * 1.5);
            this.IsDefend = true;

            DisplayMethods.DisplayInformation(prompts);
            DisplayMethods.DisplayInformation("Press any key to continue...", true);
        }

        private void UseAbility(PlayerCharacter player)
        {
            List<string> prompts = new List<string>()
                {
                    $"The {this.Name} used {this.AbilityToUse.Name}",
                    $"The {this.Name} {this.AbilityToUse.Dialog}"
                };
            if (this.AbilityToUse.IsAttack && this.CurrentMP > 0 && this.CurrentStamina > 0)
            {
                int physicalDealtDamage = (this.AbilityToUse.Attack + this.Attack) - player.Defense + player.TempDef;
                int magicalDealtDamage = (this.AbilityToUse.MagAttack + this.MagAttack) - player.MagDefense + player.TempMagDef;


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
                    prompts.Add($"The {this.Name} healed you {this.AbilityToUse.HP} HP");
                    if (player.CurrentHP + this.AbilityToUse.HP <= player.MaxHP)
                    {
                        player.CurrentHP += this.AbilityToUse.HP;
                    }
                    else
                    {
                        player.CurrentHP = player.MaxHP;
                    }
                }
                this.CurrentMP -= this.AbilityToUse.MP;
            }
            else
            {
                if (this.AbilityToUse.HP != 0)
                {
                    prompts.Add($"The {this.Name} {VerbForStatus(this.AbilityToUse.HP)} its health for {this.AbilityToUse.HP}");
                }
                if (this.AbilityToUse.MP != 0)
                {
                    prompts.Add($"The {this.Name} {VerbForStatus(this.AbilityToUse.MP)} its mana for {this.AbilityToUse.MP}");
                }
                if (this.AbilityToUse.Stamina != 0)
                {
                    prompts.Add($"The {this.Name} {VerbForStatus(this.AbilityToUse.Stamina)} its stamina for {this.AbilityToUse.Stamina}");
                }
                if (this.AbilityToUse.Attack != 0)
                {
                    prompts.Add($"The {this.Name} {VerbForCombat(this.AbilityToUse.Attack)} its attack by {this.AbilityToUse.Attack}");
                }
                if (this.AbilityToUse.Defense != 0)
                {
                    prompts.Add($"The {this.Name} {VerbForCombat(this.AbilityToUse.Defense)} its defense by {this.AbilityToUse.Defense}");
                }
                if (this.AbilityToUse.MagAttack != 0)
                {
                    prompts.Add($"The {this.Name} {VerbForCombat(this.AbilityToUse.MagAttack)} its magic attack by {this.AbilityToUse.MagAttack}");
                }
                if (this.AbilityToUse.MagAttack != 0)
                {
                    prompts.Add($"The {this.Name} {VerbForCombat(this.AbilityToUse.MagDefense)} its magic defense by {this.AbilityToUse.MagDefense}");
                }

                if (this.CurrentHP + this.AbilityToUse.HP <= this.MaxHP)
                {
                    this.CurrentHP += this.AbilityToUse.HP;
                }
                else
                {
                    this.CurrentHP = this.MaxHP;
                }

                if (this.CurrentMP + this.AbilityToUse.MP <= this.MaxMP)
                {
                    this.CurrentMP += this.AbilityToUse.MP;
                }
                else
                {
                    this.CurrentMP = this.MaxMP;
                }

                if (this.CurrentStamina + this.AbilityToUse.Stamina <= this.MaxStamina)
                {
                    this.CurrentStamina += this.AbilityToUse.Stamina;
                }
                else
                {
                    this.CurrentStamina = this.MaxStamina;
                }

                this.Attack += this.AbilityToUse.Attack;
                this.Defense += this.AbilityToUse.Defense;
                this.MagAttack += this.AbilityToUse.MagAttack;
                this.MagDefense += this.AbilityToUse.MagDefense;
            }

            DisplayMethods.DisplayInformation(prompts);
            DisplayMethods.DisplayInformation("Press any key to continue...", true);
        }

        public void DisplayStatus()
        {
            List<string> prompts = new List<string>
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
                "Experience Value: " + this.Exp + "xp",
                "Gold: " + this.Gold
            };
            foreach (string name in AbilityNames)
            {
                prompts.Add($"Ability: {name}");
            }
            DisplayMethods.DisplayInformation(prompts);
            DisplayMethods.DisplayWrappedInformation($"Lore: {this.Lore}");
            DisplayMethods.DisplayInformation("Press any key to continue...", true);

        }

        private string VerbForCombat(int amount)
        {
            string verbForCombat = "increased";
            if (amount < 0)
            {
                verbForCombat = "decreased";
            }

            return verbForCombat;
        }
        private string VerbForStatus(int amount)
        {
            string verbForStatus = "restored";

            if (amount < 0)
            {
                verbForStatus = "lowered";
            }

            return verbForStatus;
        }

        private void AttackPlayer(PlayerCharacter player)
        {
            int dealtDamage = this.Attack - player.Defense + player.TempDef;
            if (dealtDamage < 0)
            {
                dealtDamage = 0;
            }
            player.CurrentHP -= dealtDamage;
            List<string> prompts = new List<string>()
            {
                $"The {this.Name} attacked!!!",
                $"It dealt {dealtDamage} dmg!!!",
            };

            DisplayMethods.DisplayInformation(prompts);
            DisplayMethods.DisplayInformation("Press any key to continue...", true);
        }
    }
}
