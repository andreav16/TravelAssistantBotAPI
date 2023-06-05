using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities.FlightEntities;

namespace TravelAssistantBot.Infrastructure.EntityFramework.Configuration.FlightConfiguration
{
    public class ArrivalEntityConfiguration : IEntityTypeConfiguration<Arrival>
    {
        public void Configure(EntityTypeBuilder<Arrival> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Airport).IsRequired();
            builder.Property(x => x.Timezone).IsRequired();
            builder.Property(x => x.IATA).IsRequired();
            builder.Property(x => x.ICAO).IsRequired();
            builder.Property(x => x.Terminal).IsRequired();
            builder.Property(x => x.Scheduled).IsRequired();
            builder.Property(x => x.Estimated).IsRequired();

            builder.HasOne(x => x.Flight)
                .WithOne(x => x.Arrival)
                .HasForeignKey<Flight>(x => x.DepartureId);
        }
    }
}
