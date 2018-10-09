using System;
using System.Collections.Generic;
using System.Text;

namespace Brando_Jason_RPGame.Mapping
{
    public class Wall
    {
        public string Display { get; }

        public int Value { get; }

        public Wall()
        {
            Display = "X";
            Value = -1;
        }
    }
}
