using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities.GeopifyEntities.CountryEntities;

namespace TravelAssistantBot.Core.Entities.GeopifyEntities.CountryEntities
{
    public class GeocodeGroup
    {
        public List<Geopify_Features> Features { get; set; }
    }
}

//GeocodeGroup ->features[0] -> properties -> place_id