using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGMapping.Entities;
using BrandoJason_RPGMapping.Mapping;

namespace Brando_Jason_RPGMapping.GameRunning
{
    public static class IsColliding
    {
        public static bool IsCurrentlyColliding(ICharacter currentEnemy, ICharacter player)
        {
            return currentEnemy.Position[0] == player.Position[0] && currentEnemy.Position[1] == player.Position[1];
        }
        public static bool IsCurrentlyColliding(Tile specialTile, ICharacter player)
        {
            return specialTile.Position[0] == player.Position[0] && specialTile.Position[1] == player.Position[1];
        }
    }
}
