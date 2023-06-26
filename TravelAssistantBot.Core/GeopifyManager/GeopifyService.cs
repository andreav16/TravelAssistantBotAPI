using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
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
using TravelAssistantBot.Core.Options;

namespace TravelAssistantBot.Core.GeopifyManager
{
    public class GeopifyService : IGeopifyServices
    {

        private readonly GeopifyOptions_Places geopifyOptionsPlaces;
        private readonly GeopifyOptions_Geocode geopifyOptionsGeocode;
        private HttpClient @object;
        private GeopifyOptions_Geocode geopifyOptions;

        public GeopifyService(IOptions<GeopifyOptions_Places> optionsPlaces, IOptions<GeopifyOptions_Geocode> optionsGeocode)
        {
            geopifyOptionsPlaces = optionsPlaces.Value;
            geopifyOptionsGeocode = optionsGeocode.Value;
        }

        public GeopifyService(HttpClient @object, GeopifyOptions_Geocode geopifyOptions)
        {
            this.@object = @object;
            this.geopifyOptions = geopifyOptions;
        }

        public async Task<OperationResult<GeocodeGroup>> GetCountryAsync(string cityName)
        {
            HttpClient httpClient = new HttpClient();
            string requestUrl = $"{geopifyOptionsGeocode.BaseUrl}?apiKey={geopifyOptionsGeocode.ApiKey}&text={cityName}";

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
                requestUrl = $"{geopifyOptionsPlaces.BaseUrl}?categories={placeCategory}&filter={countryId}&lang=en&apiKey={geopifyOptionsPlaces.ApiKey}&limit={cant}";
            }
            else if (cant == -1)
            {
                requestUrl = $"{geopifyOptionsPlaces.BaseUrl}?categories={placeCategory}&filter={countryId}&lang=en&apiKey={geopifyOptionsPlaces.ApiKey}";
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
