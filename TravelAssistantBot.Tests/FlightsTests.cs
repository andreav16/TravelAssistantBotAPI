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

namespace TravelAssistantBot.Tests
{
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
