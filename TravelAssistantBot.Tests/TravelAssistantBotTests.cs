using NUnit.Framework;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelAssistantBot.Core.Entities.GeopifyEntities.CountryEntities;
using TravelAssistantBot.Core;
using TravelAssistantBot.Core.GeopifyManager;
using Assert = NUnit.Framework.Assert;

namespace TravelAssistantBot.Tests;
public class GeocodeServiceTests
{
    private GeopifyService geocodeService;
    private Mock<HttpClient> httpClientMock;
    private GeopifyOptions_Geocode geopifyOptions;

    [SetUp]
    public void Setup()
    {
        httpClientMock = new Mock<HttpClient>();
        geopifyOptions = new GeopifyOptions_Geocode
        {
            BaseUrl = geopifyOptions.BaseUrl,
            ApiKey = geopifyOptions.ApiKey
        };
        geocodeService = new GeopifyService(httpClientMock.Object, geopifyOptions);
    }

    [Test]
    public async Task GetCountryAsync_WhenSuccessfulResponse_ReturnsGeocodeGroup()
    {
        // Arrange
        string cityName = "London";
        string apiUrl = $"{geopifyOptions.BaseUrl}?apiKey={geopifyOptions.ApiKey}&text={cityName}";

        GeocodeGroup expectedGeocodes = new GeocodeGroup
        {
            // Set your expected geocode data here
        };

        HttpResponseMessage successfulResponse = new HttpResponseMessage(HttpStatusCode.OK);
        successfulResponse.Content = new StringContent(JsonConvert.SerializeObject(expectedGeocodes));

        httpClientMock
            .Setup(client => client.GetAsync(apiUrl, It.IsAny<CancellationToken>()))
            .ReturnsAsync(successfulResponse);

        // Act
        OperationResult<GeocodeGroup> result = await geocodeService.GetCountryAsync(cityName);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Succeeded);
        Assert.AreEqual(expectedGeocodes, result.Result);
    }

    [Test]
    public void GetCountryAsync_WhenUnsuccessfulResponse_ThrowsException()
    {
        // Arrange
        string cityName = "London";
        string apiUrl = $"{geopifyOptions.BaseUrl}?apiKey={geopifyOptions.ApiKey}&text={cityName}";

        HttpResponseMessage unsuccessfulResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);

        httpClientMock
            .Setup(client => client.GetAsync(apiUrl, It.IsAny<CancellationToken>()))
            .ReturnsAsync(unsuccessfulResponse);

        // Act & Assert
        Assert.ThrowsAsync<Exception>(() => geocodeService.GetCountryAsync(cityName));
    }
}
