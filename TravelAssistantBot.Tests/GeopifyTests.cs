using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities.GeopifyEntities.CountryEntities;
using TravelAssistantBot.Core.GeopifyManager;
using TravelAssistantBot.Core;
using Moq;
using FluentAssertions;
using TravelAssistantBot.Core.Options;

namespace TravelAssistantBot.Tests
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
    }

    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return _httpClient.GetAsync(requestUri);
        }
    }

    public class GeopifyTests
    {
        [Fact]
        public async Task GetCountryAsync_Returns_PlaceId()
        {
            // Arrange
            string cityName = "London";
            string expectedPlaceId = "519fcdaacfd556c0bf591a4bfd61f0c04940f00101f9014600010000000000c002089203064c6f6e646f6e";

            // Mock the options for GeopifyService
            var geocodeOptionsMock = new Mock<IOptions<GeopifyOptions_Geocode>>();
            geocodeOptionsMock.Setup(o => o.Value)
                .Returns(new GeopifyOptions_Geocode
                {
                    BaseUrl = "https://api.geoapify.com/v1/geocode/search",
                    ApiKey = "396ffeb46b7948a599f3faad332d71d9"
                });

            var placesOptionsMock = new Mock<IOptions<GeopifyOptions_Places>>();
            placesOptionsMock.Setup(o => o.Value)
                .Returns(new GeopifyOptions_Places
                {
                    BaseUrl = "https://api.geoapify.com/v2/places",
                    ApiKey = "YourPlacesApiKeyHere"
                });

            // Mock the HttpClientWrapper
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            string responseContent = JsonConvert.SerializeObject(new GeocodeGroup
            {
                Features = new System.Collections.Generic.List<Geopify_Features>
                {
                    new Geopify_Features
                    {
                        Properties = new Geopify_Properties
                        {
                            Place_id = expectedPlaceId
                        }
                    }
                }
            });
            httpResponseMessage.Content = new StringContent(responseContent);
            httpClientWrapperMock
                .Setup(c => c.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);

            // Create the GeopifyService instance with mocked dependencies
            var geopifyService = new GeopifyService(placesOptionsMock.Object, geocodeOptionsMock.Object);

            // Act
            var result = await geopifyService.GetCountryAsync(cityName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPlaceId, result.Result.Features[0].Properties.Place_id);
        }

        [Fact]
        public async Task GetCountryAsync_WrongPlaceId_ReturnsDifferentId()
        {
            // Arrange
            string cityName = "Miami";
            string expectedPlaceId = "558fcdaacfd556c0bf591a4bfd61f0c04940f00101f9014600010000000000c002089203064c6f6e646f6e";

            // Mock the options for GeopifyService
            var geocodeOptionsMock = new Mock<IOptions<GeopifyOptions_Geocode>>();
            geocodeOptionsMock.Setup(o => o.Value)
                .Returns(new GeopifyOptions_Geocode
                {
                    BaseUrl = "https://api.geoapify.com/v1/geocode/search",
                    ApiKey = "396ffeb46b7948a599f3faad332d71d9"
                });

            var placesOptionsMock = new Mock<IOptions<GeopifyOptions_Places>>();
            placesOptionsMock.Setup(o => o.Value)
                .Returns(new GeopifyOptions_Places
                {
                    BaseUrl = "https://api.geoapify.com/v2/places",
                    ApiKey = "YourPlacesApiKeyHere"
                });

            // Mock the HttpClientWrapper
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
            httpClientWrapperMock
                .Setup(c => c.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);

            var geopifyService = new GeopifyService(placesOptionsMock.Object, geocodeOptionsMock.Object);

            // Act
            var result = await geopifyService.GetCountryAsync(cityName);

            // Assert
            Assert.NotEqual(expectedPlaceId, result.Result.Features[0].Properties.Place_id);
        }
    }
}
