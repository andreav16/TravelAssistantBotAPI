using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Factory;
using TravelAssistantBot.Core.Interfaces;

namespace TravelAssistantBot.Core.IATA_Manager
{
    public class CityCodeFinderFactory : ICityCodeFinderFactory
    {
        public ICityCodeFinder CreateCityCodeFinder()
        {
            return new CityCodeFinder();
        }
    }
}
