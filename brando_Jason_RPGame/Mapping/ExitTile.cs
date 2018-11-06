using System;
using System.Collections.Generic;
using System.Text;

namespace BrandoJason_RPGMapping.Mapping
{
    public class ExitTile : Tile
    {
        public new static string Display { get => "E"; }

        public new static int Value { get => 2; }

        public ExitTile()
        {
            this.InstanValue = ExitTile.Value;
        }
    }
}
