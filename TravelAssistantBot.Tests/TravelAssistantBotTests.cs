using FluentAssertions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Reflection;
using TravelAssistantBot.Core;
using TravelAssistantBot.Core.Entities;
using TravelAssistantBot.Core.Entities.FlightEntities;
using TravelAssistantBot.Core.EventManager;
using TravelAssistantBot.Infrastructure.EntityFramework;
using TravelAssistantBot.Infrastructure.EntityFramework.Repositories;
using TypeMock.ArrangeActAssert;
using Xunit;


namespace TravelAssistantBot.Tests;
public class GeocodeServiceTests
{
 
    [Theory]
    [InlineData("American Airlines", 1)]
    public void FilterAirportByName_ReturnsCorrectResults(string name, int expectedCount)
    {
        var airlines = new List<Airline>
        {
            new Airline
            {
                Name = "American Airlines",
                Id = 1,
                IATA = "AA",
                FlightId = 1,
            },
            new Airline
            {
                Name = "Copa Airlines",
                Id = 2,
                IATA = "CM",
                FlightId = 2,
            },
            new Airline
            {
                Name = "Delta Air Lines",
                Id = 3,
                IATA = "DL",
                FlightId = 3,
            },
            new Airline
            {
                Name = "United Airlines",
                Id = 4,
                IATA = "UA",
                FlightId = 4,
            },
            new Airline
            {
                Name = "Air France",
                Id = 5,
                IATA = "AF",
                FlightId = 5,
            },
        };

        TravelAssistantBotContext context = GetTravelAssistantBotContext(airlines, "FilterAirportByName");

        var repository = new BaseRepository<Airline>(context);

        var result = repository.Filter(x => x.Name.StartsWith(name));

        result.Count().Should().Be(expectedCount);
    }

    [Fact]
    public async Task GetEventByName_ValidName_ReturnCorrectEvent()
    {
        // Arrange
        var eventServiceMock = new Mock<IEventService>();
        eventServiceMock.Setup(e => e.GetEventByNameAsync("Go to the doctor")).ReturnsAsync(new Event
        {
            Id = "1",
            Summary = "Go to the doctor",
            Start = new EventDateTime { DateTime = DateTime.Now.AddDays(1) },
            End = new EventDateTime { DateTime = DateTime.Now.AddDays(2) }
        });

        var eventService = eventServiceMock.Object;

        // Act
        var result = await eventService.GetEventByNameAsync("Go to the doctor");

        // Assert
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal("Go to the doctor", result.Result.Summary);
    }

    private TravelAssistantBotContext GetTravelAssistantBotContext(List<Airline> airlines, string name)
    {

      
        var contextOptions = new DbContextOptionsBuilder<TravelAssistantBotContext>()
            .UseInMemoryDatabase(databaseName: name)
            .Options;

        var context = new TravelAssistantBotContext(contextOptions);
        context.Airlines.AddRange(airlines);
        context.SaveChanges();
        return context;
    }
}
