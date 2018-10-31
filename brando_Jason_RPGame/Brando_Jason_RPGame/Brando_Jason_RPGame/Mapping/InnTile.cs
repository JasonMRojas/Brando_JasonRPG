using BrandoJason_RPGMapping.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandoJason_RPGMapping.Mapping
{
    public class InnTile : Tile
    {
        public new static string Display { get => "I"; }

        public new static int Value { get => 5; }

        public InnTile()
        {
            this.InstanValue = InnTile.Value;
        }
    }

}
