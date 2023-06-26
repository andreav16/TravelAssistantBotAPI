using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TravelAssistantBot.Core;
using TravelAssistantBot.Core.Entities.GeopifyEntities.CountryEntities;
using TravelAssistantBot.Core.Entities.GeopifyEntities.PlacesEntities;
using TravelAssistantBot.Core.GeopifyManager;
using TravelAssistantBot.Tests.Fakes;
using Xunit;

namespace TravelAssistantBot.Tests
{
    public class GeopifyServiceTests
    {
        [Fact]
        public async Task GetCountryAsync_ReturnsGeocodeGroup()
        {
            // Arrange
            var optionsGeocodeMock = new Mock<IOptions<GeopifyOptions_Geocode>>();
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            var geocodeGroup = new GeocodeGroup();

            httpClientWrapperMock.Setup(c => c.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(geocodeGroup))
            });

            var httpClientWrapper = httpClientWrapperMock.Object;
            var geopifyService = new GeopifyService(httpClientWrapper, optionsGeocodeMock.Object);

            // Act
            var result = await geopifyService.GetCountryAsync("CityName");

            // Assert
            Assert.Equal(geocodeGroup, result.Result);
        }

        [Fact]
        public async Task GetPlacesDataAsync_ReturnsPlacesGroupList()
        {
            // Arrange
            var optionsPlacesMock = new Mock<IOptions<GeopifyOptions_Places>>();
            var optionsGeocodeMock = new Mock<IOptions<GeopifyOptions_Geocode>>();
            var httpClientWrapperMock = new Mock<IHttpClientWrapper>();
            var placesGroup = new PlacesGroup();

            httpClientWrapperMock.Setup(c => c.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(placesGroup))
            });

            var httpClientWrapper = httpClientWrapperMock.Object;
            var geopifyService = new GeopifyService(httpClientWrapper, optionsGeocodeMock.Object);

            // Act
            var result = await geopifyService.GetPlacesDataAsync("CountryId", "PlaceCategory", 10);

            // Assert
            Assert.Equal(placesGroup, result.Result.First());
        }
    }
}
