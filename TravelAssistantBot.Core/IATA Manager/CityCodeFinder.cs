using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities.FlightEntities.IATA_Codes;
using TravelAssistantBot.Core.Interfaces;

namespace TravelAssistantBot.Core.IATA_Manager
{
    public class CityCodeFinder : ICityCodeFinder
    {
        string file = File.ReadAllText("C:\\Users\\leoth\\Documents\\2 tri 2023\\Vanguardia\\Proyecto VANG\\TravelAssistantBotAPI\\TravelAssistantBot.Api\\IATA_Code.json");

        public string FindCityCode(string cityName)
        {
            IATA_Codes data = JsonConvert.DeserializeObject<IATA_Codes>(file);
            int cant = data.codes.Count;
            for (int i = 0; i < cant; i++)
            {
                if (string.Equals(data.codes[i].City, cityName, StringComparison.OrdinalIgnoreCase))
                {
                    return data.codes[i].Code;
                }
            }
            return null;
        }
    }
}
