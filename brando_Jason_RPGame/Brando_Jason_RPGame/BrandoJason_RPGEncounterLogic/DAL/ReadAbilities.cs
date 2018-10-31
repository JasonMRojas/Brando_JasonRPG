using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BrandoJason_RPGEncounterLogic.Monsters;
using Newtonsoft.Json;

namespace BrandoJason_RPGEncounterLogic.DAL
{
    public class ReadAbilities
    {
        public List<Ability> GetAbilities(List<string> abilities)
        {
            using (StreamReader reader = new StreamReader("DAL\\Abilities.json"))
            {
                string json = reader.ReadToEnd();
                JSONAbilityConversion newJsonConversion = JsonConvert.DeserializeObject<JSONAbilityConversion>(json);

                List<Ability> abilitiesToReturn = new List<Ability>();

                foreach (Ability ability in newJsonConversion.Abilities)
                {
                    foreach (string name in abilities)
                    {
                        if (name == ability.Name)
                        {
                            abilitiesToReturn.Add(ability);
                        }
                    }
                }
                return abilitiesToReturn;
            }
        }

        public class JSONAbilityConversion
        {
            public List<Ability> Abilities { get; set; }
        }
    }
}
