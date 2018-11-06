using System;
using System.Collections.Generic;
using System.Text;

namespace BrandoJason_RPGMapping.Mapping
{
    public class Tile
    {
        public static string Display { get => " "; }

        public static int Value { get => 0; }

        public int InstanValue { get; protected set; }

        public int[] Position { get; set; }

        public int AssociationNum { get; set; }
    }
}
