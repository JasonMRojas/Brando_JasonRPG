using System;
using BrandoJason_RPGEncounterLogic.Character;

namespace BrandoJason_RPGEncounterLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerCharacter player = new PlayerCharacter("Sajor Nosaj");
            player.LevelUp();
            player.ViewStatus();
        }
    }
}
