using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Infrastructure.EntityFramework.Repositories;
using TravelAssistantBot.Infrastructure.EntityFramework;
using TravelAssistantBot.Core.Entities.FlightEntities;
using FluentAssertions;
using Moq;
using TravelAssistantBot.Core.FlightManager;
using TravelAssistantBot.Core;
using TravelAssistantBot.Api.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAssistantBot.Api.DataTransferObjects.FlightDataTransferObjects;

namespace TravelAssistantBot.Tests
{
    //Repository tests
    public class FlightsTests
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

        //Controller tests
        [Fact]
        public async Task GetFlightByFlightCodeAsync_ValidFlightCode_ReturnsFlightDetailsDataTransferObject()
        {
            // Arrange
            string flightCode = "ABC123";
            var expectedFlight = new Flight
            {
                Id = 1,
                FlightDate = DateTime.Now,
                FlightStatus = "On Time",
                Departure = new Departure
                {
                    Airport = "Airport A",
                    IATA = "DEP",
                    Scheduled = DateTime.Now.AddHours(1)
                },
                Arrival = new Arrival
                {
                    Airport = "Airport B",
                    IATA = "ARR",
                    Scheduled = DateTime.Now.AddHours(2)
                   
                },
                Airline = new Airline
                {
                    Name = "Airline A",
                    IATA = "AIR"
                },
                FlightInfo = new FlightInfo
                {
                    Number = "123",
                    IATA = "FLI"
                },
                Aircraft = "Aircraft A",
                Live = "Live A"
            };

            // Configurar el mock del servicio de vuelo
            var flightServiceMock = new Mock<IFlightService>();
            flightServiceMock.Setup(s => s.GetFlightByFlightCodeAsync(flightCode))
                .ReturnsAsync(new OperationResult<Flight>(expectedFlight));

            // Configurar el mock del AutoMapper
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<FlightDetailsDataTransferObject>(expectedFlight))
                .Returns(new FlightDetailsDataTransferObject
                {
                    FlightDate = expectedFlight.FlightDate,
                    FlightStatus = expectedFlight.FlightStatus,
                    FlightDeparture = new FlightDetailsDataTransferObject.Departure
                    {
                        Airport = expectedFlight.Departure.Airport,
                        IATA = expectedFlight.Departure.IATA,
                        Scheduled = expectedFlight.Departure.Scheduled
                    },
                    FlightArrival = new FlightDetailsDataTransferObject.Arrival
                    {
                        Airport = expectedFlight.Arrival.Airport,
                        IATA = expectedFlight.Arrival.IATA,
                        Scheduled = expectedFlight.Arrival.Scheduled
                    },
                    FlightAirline = new FlightDetailsDataTransferObject.Airline
                    {
                        Name = expectedFlight.Airline.Name,
                        IATA = expectedFlight.Airline.IATA
                    },
                    Flight = new FlightDetailsDataTransferObject.FlightInfo
                    {
                        Number = expectedFlight.FlightInfo.Number,
                        IATA = expectedFlight.FlightInfo.IATA
                    }
                });

            // Crear el controlador con los mocks
            var controller = new FlightsController(flightServiceMock.Object, mapperMock.Object);

            // Act
            var result = await controller.GetFlightByFlightCodeAsync(flightCode);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

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
}
