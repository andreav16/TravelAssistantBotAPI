using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAssistantBot.Core.Entities.FlightEntities;

namespace TravelAssistantBot.Infrastructure.EntityFramework.Configuration.FlightConfiguration
{
    public class AirlineEntityConfiguration : IEntityTypeConfiguration<Airline>
    {
        public void Configure(EntityTypeBuilder<Airline> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.IATA).IsRequired();

            builder.HasOne(x => x.Flight)
                .WithOne(x => x.Airline)
                .HasForeignKey<Flight>(x => x.AirlineId);
        }
    }
}
