using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BrandoJason_RPGEncounterLogic.Monsters;
using Newtonsoft.Json;

namespace BrandoJason_RPGEncounterLogic.DAL
{
    class ReadMonster
    {
        public Monster GetMonster()
        {
            using (StreamReader reader = new StreamReader("DAL\\Monsters.json"))
            {
                string json = reader.ReadToEnd();
                JSONConversion newJsonConversion = JsonConvert.DeserializeObject<JSONConversion>(json);

                Random rnd = new Random();

                return newJsonConversion.Monsters[rnd.Next(0, newJsonConversion.Monsters.Count)];
            }
        }       
    }

    public class JSONConversion
    {
        public List<Monster> Monsters { get; set; }
    }
}
