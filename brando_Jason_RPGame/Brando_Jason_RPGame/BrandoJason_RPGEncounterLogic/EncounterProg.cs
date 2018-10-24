using System;
using BrandoJason_RPGEncounterLogic.Character;
using System.Data.SqlClient;
using System.Data;
using BrandoJason_RPGEncounterLogic.Database;
using BrandoJason_RPGEncounterLogic.Encounter;
using CSCore.CoreAudioAPI;
using BrandoJason_RPGEncounterLogic.Sound;


namespace BrandoJason_RPGEncounterLogic
{
    public class EncounterProg
    {
        public PlayerCharacter Player { get; private set; }
        public RPGDateBaseStorage Data { get; private set; }

        static void Main(string[] args)
        {

        }

        public void RunCharacterCreation()
        {
            this.Player = new PlayerCharacter("Name");
            this.Player.LevelUp();

            this.Data = new RPGDateBaseStorage();
            Data.PopulateDataBase(@"Data Source = (local)\SQLEXPRESS; Initial Catalog = RPG; Integrated Security = True;");
        }

        public bool RunEncounterProg(int encounterID)
        {
            EncounterRunning newEnounter = new EncounterRunning(Data);

            var song = new Playable(new MusicPlayer());
            song.Play();

            System.Threading.Thread.Sleep(1000);

            newEnounter.RunEncounter(encounterID, this.Player);

            song.Stop();


            return Player.CurrentHP <= 0;
        }
    }
}
