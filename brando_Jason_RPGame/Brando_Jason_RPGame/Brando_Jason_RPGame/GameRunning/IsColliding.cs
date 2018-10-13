using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGame.Entities;
using Brando_Jason_RPGame.Mapping;

namespace Brando_Jason_RPGame.GameRunning
{
    public static class IsColliding
    {
        public static bool IsCurrentlyColliding(Drone currentEnemy, PlayerCharacter player)
        {
            return currentEnemy.Position[0] == player.Position[0] && currentEnemy.Position[1] == player.Position[1];
        }
        public static bool IsCurrentlyColliding(Tile specialTile, ICharacter player)
        {
            return specialTile.Position[0] == player.Position[0] && specialTile.Position[1] == player.Position[1];
        }
    }
}
