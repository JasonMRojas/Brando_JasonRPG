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

        bool DidMove { get; set; }

        void Move(Map map);

    }
}
