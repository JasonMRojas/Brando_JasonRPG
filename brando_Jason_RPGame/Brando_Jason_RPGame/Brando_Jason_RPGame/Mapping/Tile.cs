using System;
using System.Collections.Generic;
using System.Text;

namespace Brando_Jason_RPGame.Mapping
{
    public class Tile
    {
        public static string Display { get => " "; }

        public static int Value { get => 0; }

        public int[] Position { get; set; }

        public int AssociationNum { get; set; }
    }
}
