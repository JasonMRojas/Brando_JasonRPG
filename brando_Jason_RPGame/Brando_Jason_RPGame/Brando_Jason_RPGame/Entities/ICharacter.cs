using System;
using System.Collections.Generic;
using System.Text;
using Brando_Jason_RPGMapping.Mapping;

namespace Brando_Jason_RPGMapping.Entities
{
    public interface ICharacter
    {
        int[] Position { get; set; }

        int Value { get; }

        void Move(Map map);

    }
}
