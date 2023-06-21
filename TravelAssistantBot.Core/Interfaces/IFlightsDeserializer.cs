using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities.FlightEntities;
using TravelAssistantBot.Core.Entities.FlightEntities.FileData;

namespace TravelAssistantBot.Core.Interfaces
{
    public interface IFlightsDeserializer
    {
        public FlightDataResponse Deserialize(string source);
    }
}
