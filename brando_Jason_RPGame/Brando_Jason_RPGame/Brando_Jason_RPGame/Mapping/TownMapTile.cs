using System;
using System.Collections.Generic;
using System.Text;

namespace BrandoJason_RPGMapping.Mapping
{
    public class TownMapTile : Tile
    {
        public new static string Display { get => "T"; }

        public new static int Value { get => 3; }

        public TownMapTile()
        {
            this.InstanValue = TownMapTile.Value;
        }
    }
}
