using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities.GeopifyEntities.CountryEntities;
using TravelAssistantBot.Core.Entities.GeopifyEntities.PlacesEntities;

namespace TravelAssistantBot.Core.GeopifyManager
{
    public interface IGeopifyServices
    {
        Task<OperationResult<GeocodeGroup>> GetCountryAsync(string cityName);
        Task<OperationResult<IList<PlacesGroup>>> GetPlacesDataAsync(string countryId, string placeCategory, int cant);
    }
}