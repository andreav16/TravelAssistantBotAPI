using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TravelAssistantBot.Core.Entities.FlightEntities;
using TravelAssistantBot.Infrastructure.EntityFramework.Configuration;
using TravelAssistantBot.Infrastructure.EntityFramework.Configuration.FlightConfiguration;

namespace TravelAssistantBot.Infrastructure.EntityFramework
{
    public class TravelAssistantBotContext : DbContext
    {
        List<FlightData> flightDataList;
        public TravelAssistantBotContext(DbContextOptions<TravelAssistantBotContext> options) : base(options)
        {
            string json = File.ReadAllText("../aviation-data.json");
            this.flightDataList = JsonConvert.DeserializeObject<List<FlightData>>(json);
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Arrival> Arrivals { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<FlightInfo> FlightInfos { get; set; }
        public DbSet<CodeShare> CodeShares { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FlightEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DepartureEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ArrivalEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AirlineEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FlightInfoEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CodeShareEntityConfiguration());

            foreach (var flightData in flightDataList)
            {
                modelBuilder.Entity<Flight>().HasData(new Flight
                {                   
                    Departure = new Departure
                    {
                        Airport = flightData.Departure.Airport,
                        Timezone = flightData.Departure.Timezone,
                        IATA = flightData.Departure.IATA,
                        ICAO = flightData.Departure.ICAO,
                        Terminal = flightData.Departure.Terminal,
                        Gate = flightData.Departure.Gate,
                        Delay = flightData.Departure.Delay,
                        Scheduled = flightData.Departure.Scheduled,
                        Estimated = flightData.Departure.Estimated,
                        Actual = flightData.Departure.Actual,
                        EstimatedRunway = flightData.Departure.EstimatedRunway,
                        ActualRunway = flightData.Departure.ActualRunway
                    },
                    Arrival = new Arrival
                    {
                        Airport = flightData.Arrival.Airport,
                        Timezone = flightData.Arrival.Timezone,
                        IATA = flightData.Arrival.IATA,
                        ICAO = flightData.Arrival.ICAO,
                        Terminal = flightData.Arrival.Terminal,
                        Gate = flightData.Arrival.Gate,
                        Baggage = flightData.Arrival.Baggage,
                        Delay = flightData.Arrival.Delay,
                        Scheduled = flightData.Arrival.Scheduled,
                        Estimated = flightData.Arrival.Estimated,
                        Actual = flightData.Arrival.Actual,
                        EstimatedRunway = flightData.Arrival.EstimatedRunway,
                        ActualRunway = flightData.Arrival.ActualRunway
                    },
                    Airline = new Airline
                    {
                        Name = flightData.Airline.Name,
                        IATA = flightData.Airline.IATA,
                        ICAO = flightData.Airline.ICAO,
                    },
                    FlightInfo = new FlightInfo
                    {
                        Number = flightData.Flight.Number,
                        IATA = flightData.Flight.IATA,
                        ICAO = flightData.Flight.ICAO,

                        CodeShare = new CodeShare
                        {
                            AirlineName = flightData.Flight.CodeShare.AirlineName,
                            AirlineIATA = flightData.Flight.CodeShare.AirlineIATA,
                            AirlineICAO = flightData.Flight.CodeShare.AirlineICAO,
                            FlightNumber = flightData.Flight.CodeShare.FlightNumber,
                            FlightIATA = flightData.Flight.CodeShare.FlightIATA,
                            FlightICAO = flightData.Flight.CodeShare.FlightICAO
                        }
                    },
                });
            }
        }
    }
}
