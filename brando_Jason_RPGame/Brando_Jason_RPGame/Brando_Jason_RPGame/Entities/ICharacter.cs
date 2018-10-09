using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGame.Mapping;

namespace Brando_Jason_RPGame.Entities
{
    interface ICharacter
    {
        int[] Position { get; set; }

        void Move(Map map);

    }
}
