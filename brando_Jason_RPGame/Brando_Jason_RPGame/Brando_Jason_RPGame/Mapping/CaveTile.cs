using System;
using System.Collections.Generic;
using System.Text;

namespace Brando_Jason_RPGMapping.Mapping
{
    public class CaveTile : Tile
    {
        public new static string Display { get => "C"; }

        public new static int Value { get => 4; }

        public CaveTile()
        {
            this.InstanValue = CaveTile.Value;
        }
    }
}
