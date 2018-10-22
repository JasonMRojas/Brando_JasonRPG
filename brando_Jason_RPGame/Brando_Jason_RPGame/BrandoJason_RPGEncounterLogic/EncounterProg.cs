using System;
using BrandoJason_RPGEncounterLogic.Character;
using System.Data.SqlClient;
using System.Data;
using BrandoJason_RPGEncounterLogic.Database;
using BrandoJason_RPGEncounterLogic.Encounter;

namespace BrandoJason_RPGEncounterLogic
{
    public static class EncounterProg
    {
        static void Main(string[] args)
        {

        }
        public static bool RunEncounterProg(int encounterID)
        {
            RPGDateBaseStorage data = new RPGDateBaseStorage();
            data.PopulateDataBase(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = RPG; Integrated Security = True;");

            EncounterRunning newEnounter = new EncounterRunning(data);

            PlayerCharacter player = new PlayerCharacter("Sajor Nosaj");
            player.LevelUp();
            player.ViewStatus();

            newEnounter.RunEncounter(encounterID, player);

            return false;
        }
    }
}
