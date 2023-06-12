using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Core.Entities.GeopifyEntities.PlacesEntities
{
    public class Places_Properties
    {
        public string Name { get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public string Formatted { get; set; }
    }
}
