using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities.FlightEntities;
using TravelAssistantBot.Core.Entities.FlightEntities.FileData;
using TravelAssistantBot.Core.Interfaces;

namespace TravelAssistantBot.Infrastructure.Deserializer
{
    public class FlightJsonDeserializer : IFlightsDeserializer
    {
        public FlightDataResponse Deserialize(string source)
        {
            var json = File.ReadAllText(source);
            var jsonResponse = JsonConvert.DeserializeObject<FlightDataResponse>(json);

            if(jsonResponse == null)
            {
                return new FlightDataResponse();
            }   
            return jsonResponse;
        }
    }
}
