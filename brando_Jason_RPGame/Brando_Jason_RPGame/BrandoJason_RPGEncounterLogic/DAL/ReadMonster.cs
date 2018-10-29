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
            using (StreamReader reader = new StreamReader("Monsters.json"))
            {
                string json = reader.ReadToEnd();
                List<Monster> monsters = JsonConvert.DeserializeObject<List<Monster>>(json);

                Random rnd = new Random();

                return monsters[rnd.Next(0, monsters.Count)];
            }
       
        }       
    }
}
