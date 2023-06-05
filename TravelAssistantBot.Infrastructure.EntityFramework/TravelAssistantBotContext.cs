using Microsoft.EntityFrameworkCore;
using TravelAssistantBot.Core.Entities.FlightEntities;
using TravelAssistantBot.Infrastructure.EntityFramework.Configuration;
using TravelAssistantBot.Infrastructure.EntityFramework.Configuration.FlightConfiguration;

namespace TravelAssistantBot.Infrastructure.EntityFramework
{
    public class TravelAssistantBotContext : DbContext
    {
        public TravelAssistantBotContext(DbContextOptions<TravelAssistantBotContext> options) : base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Arrival> Arrivals { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<FlightInfo> FlightInfos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FlightEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DepartureEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ArrivalEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AirlineEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FlightInfoEntityConfiguration());
        }
    }
}
