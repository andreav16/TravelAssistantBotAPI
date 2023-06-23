using Microsoft.EntityFrameworkCore;using TravelAssistantBot.Core.Entities.FlightEntities;
using TravelAssistantBot.Core.Entities.FlightEntities.FileData;
using TravelAssistantBot.Infrastructure.Deserializer;
using TravelAssistantBot.Infrastructure.EntityFramework.Configuration;
using TravelAssistantBot.Infrastructure.EntityFramework.Configuration.FlightConfiguration;

namespace TravelAssistantBot.Infrastructure.EntityFramework
{
    public class TravelAssistantBotContext : DbContext
    {
        public TravelAssistantBotContext(DbContextOptions<TravelAssistantBotContext> options)
        : base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Arrival> Arrivals { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<FlightInfo> FlightInfos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FlightJsonDeserializer deserializer = new FlightJsonDeserializer();
            string filePath = "aviation-data.json";
            var deserializeResponse = deserializer.Deserialize(filePath);

            List<FlightData> flightDataList = deserializeResponse.Data;

            modelBuilder.ApplyConfiguration(new FlightEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DepartureEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ArrivalEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AirlineEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FlightInfoEntityConfiguration());

            var id = 1;
            foreach (var flightData in flightDataList)
            {

                var departure = new Departure
                {
                    Id = id,
                    Airport = flightData.Departure.Airport,
                    IATA = flightData.Departure.IATA,
                    Terminal = flightData.Departure.Terminal ?? "",
                    Gate = flightData.Departure.Gate ?? "",
                    Delay = flightData.Departure.Delay ?? "",
                    Scheduled = flightData.Departure.Scheduled,
                    Estimated = flightData.Departure.Estimated,
                    Actual = flightData.Departure.Actual ?? DateTime.Now,
                    FlightId = id,
                };

                modelBuilder.Entity<Departure>().HasData(departure);

                var arrival = new Arrival
                {
                    Id = id,
                    Airport = flightData.Arrival.Airport,
                    IATA = flightData.Arrival.IATA,
                    Terminal = flightData.Arrival.Terminal ?? "",
                    Gate = flightData.Arrival.Gate ?? "",
                    Delay = flightData.Arrival.Delay ?? "",
                    Scheduled = flightData.Arrival.Scheduled,
                    Estimated = flightData.Arrival.Estimated,
                    Actual = flightData.Arrival.Actual ?? DateTime.Now,
                    FlightId = id,
                };

                modelBuilder.Entity<Arrival>().HasData(arrival);

                var airline = new Airline
                {
                    Id = id,
                    Name = flightData.Airline.Name ?? "",
                    IATA = flightData.Airline.IATA ?? "",
                    FlightId = id,
                };

                modelBuilder.Entity<Airline>().HasData(airline);

                var flightInfo = new FlightInfo
                {
                    Id = id,
                    Number = flightData.Flight.Number,
                    IATA = flightData.Flight.IATA,
                    FlightId = id,

                };

                modelBuilder.Entity<FlightInfo>().HasData(flightInfo);

                var flight = new Flight
                {
                    Id = id,
                    FlightDate = flightData.FlightDate,
                    FlightStatus = flightData.FlightStatus ?? "",
                    
                    DepartureId = departure.Id,
                    Departure = departure,

                    ArrivalId = arrival.Id,
                    Arrival = arrival,

                    AirlineId = airline.Id,
                    Airline = airline,

                    FlightInfoId = flightInfo.Id,
                    FlightInfo = flightInfo,

                    Aircraft = flightData.Aircraft ?? "",
                    Live = flightData.Live ?? "",

                };

                id++;

                modelBuilder.Entity<Flight>().HasData(flight);
            }
        }
    }
}
