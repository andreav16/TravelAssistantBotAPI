using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAssistantBot.Core.Entities.FlightEntities;

namespace TravelAssistantBot.Infrastructure.EntityFramework.Configuration
{
    public class FlightEntityConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder) 
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FlightDate).IsRequired();
            builder.Property(x => x.FlightStatus).IsRequired();

            builder.HasOne(x => x.Departure)
                .WithOne(x => x.Flight)
                .HasForeignKey<Departure>(x => x.FlightId);

            builder.HasOne(x => x.Arrival)
                .WithOne(x => x.Flight)
                .HasForeignKey<Arrival>(x => x.FlightId);

            builder.HasOne(x => x.Airline)
                .WithOne(x => x.Flight)
                .HasForeignKey<Airline>(x => x.FlightId);

            builder.HasOne(x => x.FlightInfo)
                .WithOne(x => x.Flight)
                .HasForeignKey<FlightInfo>(x => x.FlightId);
        }
    }
}
