using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BrandoJason_RPGEncounterLogic.Monster
{
    public class Ability
    {
        public int MonsterID { get; }


        public Ability(int monsterID)
        {
            this.MonsterID = monsterID;
            //this.Ge
        }

        public List<string> GetAbilites(DataTable abilities)
        {
            List<string> abilityNameList = new List<string>();
            foreach (DataRow row in abilities.Rows)
            {
                if (this.MonsterID == int.Parse(row["monster_id"].ToString()))
                {
                    foreach (var item in row.ItemArray)
                    {
                        abilityNameList.Add(item.ToString());
                    }
                }
            }
            return abilityNameList;
        }
    }
}
