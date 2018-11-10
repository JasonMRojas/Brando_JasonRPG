using Brando_Jason_RPGMapping.Entities;
using BrandoJason_RPGMapping.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrandoJason_RPGMapping.Entities
{
    public class BossToken : Drone
    {
        public override int Value { get; }

        public string Display { get; }

        public override int EncounterLevel { get; }

        public BossToken()
        {
            this.Display = "B";
            this.Value = 10;
            this.EncounterLevel = 15;
        }

        public override void Move(Map map)
        {
            this.Position = this.Position;
        }
    }
}
