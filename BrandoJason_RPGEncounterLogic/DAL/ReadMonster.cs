using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BrandoJason_RPGEncounterLogic.Monsters;
using Newtonsoft.Json;

namespace BrandoJason_RPGEncounterLogic.DAL
{
    public class ReadMonster
    {
        public Monster GetMonster(int level)
        {
            using (StreamReader reader = new StreamReader("DAL\\Monsters.json"))
            {
                string json = reader.ReadToEnd();
                JSONMonsterConversion newJsonConversion = JsonConvert.DeserializeObject<JSONMonsterConversion>(json);

                Random rnd = new Random();
                Monster currentMonster = new Monster();
                while ((currentMonster.Level != level && currentMonster.Level != level - 1 && currentMonster.Level != level + 1) || currentMonster.Level == 0)
                {
                   currentMonster = newJsonConversion.Monsters[rnd.Next(0, newJsonConversion.Monsters.Count)];
                }
                ReadAbilities a = new ReadAbilities();
                currentMonster.MonsterAbilities = a.GetAbilities(currentMonster.AbilityNames);
                return currentMonster;
            }
        }       
    }

    public class JSONMonsterConversion
    {
        public List<Monster> Monsters { get; set; }
    }
}
