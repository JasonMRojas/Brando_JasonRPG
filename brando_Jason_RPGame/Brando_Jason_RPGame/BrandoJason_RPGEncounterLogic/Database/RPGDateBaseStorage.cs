using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace BrandoJason_RPGEncounterLogic.Database
{
    public class RPGDateBaseStorage
    {
        public DataTable Monster { get; private set; }
        public DataTable Usuable_Item { get; private set; }
        public DataTable Equipable_Item { get; private set; }
        public DataTable Abilities { get; private set; }
        public DataTable Effects { get; private set; }
        public DataTable StatValues { get; private set; }

        public void PopulateDataBase(string sqlConnectionString)
        {
            this.Monster = new DataTable();
            this.Usuable_Item = new DataTable();
            this.Equipable_Item = new DataTable();
            this.Effects = new DataTable();
            this.Abilities = new DataTable();
            this.StatValues = new DataTable();


            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {


                    conn.Open();

                    SqlDataAdapter sqlMonster = new SqlDataAdapter("Select * from monsters", conn);
                    SqlDataAdapter sqlUsuableItem = new SqlDataAdapter("Select * from Usuable_Items", conn);
                    SqlDataAdapter sqlEquipItem = new SqlDataAdapter("Select * from Equipable_Items", conn);
                    SqlDataAdapter sqlAbilities = new SqlDataAdapter("Select * from Abilities", conn);
                    SqlDataAdapter sqlEffects = new SqlDataAdapter("Select * from Effects", conn);
                    SqlDataAdapter sqlStatValues = new SqlDataAdapter("Select * from Stat_Values", conn);

                    sqlMonster.Fill(this.Monster);
                    sqlUsuableItem.Fill(this.Usuable_Item);
                    sqlEquipItem.Fill(this.Equipable_Item);
                    sqlAbilities.Fill(this.Abilities);
                    sqlEffects.Fill(this.Effects);
                    sqlStatValues.Fill(this.StatValues);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection to SQL databse failed");
                throw;
            }


        }
    }
}

