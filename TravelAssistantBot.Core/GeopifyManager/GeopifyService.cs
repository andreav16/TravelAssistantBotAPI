using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities.GeopifyEntities.CountryEntities;
using TravelAssistantBot.Core.Entities.GeopifyEntities.PlacesEntities;

namespace TravelAssistantBot.Core.GeopifyManager
{
    public class GeopifyService : IGeopifyServices
    {
        // Configuración de la API de Geoapify Places
        string apiKey_PLACES_API = "b50871dd5acb475ca6a8e969f6679626";
        string baseUrl_PLACES_API = "https://api.geoapify.com/v2/places";

        // Configuracion de la API de Geopify Geocode
        string apiKey_GEOCODE_API = "396ffeb46b7948a599f3faad332d71d9";
        string baseUrl_GEOCODE_API = "https://api.geoapify.com/v1/geocode/search";


        public async Task<OperationResult<GeocodeGroup>> GetCountryAsync(string cityName)
        {
            HttpClient httpClient = new HttpClient();
            string requestUrl = $"{baseUrl_GEOCODE_API}?apiKey={apiKey_GEOCODE_API}&text={cityName}";

            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                GeocodeGroup geocodes = JsonConvert.DeserializeObject<GeocodeGroup>(result);
                return geocodes;
            }
            else
            {
                throw new Exception("La solicitud no fue exitosa. Código de estado: " + response.StatusCode);
            }

        }

        public async Task<OperationResult<IList<PlacesGroup>>> GetPlacesDataAsync(string countryId, string placeCategory, int cant)
        {
            HttpClient httpClient = new HttpClient();

            string requestUrl = "";

            if (cant > 0)
            {
                requestUrl = $"{baseUrl_PLACES_API}?categories={placeCategory}&filter={countryId}&lang=en&apiKey={apiKey_PLACES_API}&limit={cant}";
            }
            else if (cant == -1)
            {
                requestUrl = $"{baseUrl_PLACES_API}?categories={placeCategory}&filter={countryId}&lang=en&apiKey={apiKey_PLACES_API}";
            }

            HttpResponseMessage response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                PlacesGroup places = JsonConvert.DeserializeObject<PlacesGroup>(responseContent);
                return new OperationResult<IList<PlacesGroup>>(new List<PlacesGroup> { places });
            }
            else
            {
                throw new Exception("La solicitud no fue exitosa. Código de estado: " + response.StatusCode);
            }
        }
    }
}
