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
    public class DepartureEntityConfiguration : IEntityTypeConfiguration<Departure>
    {
        public void Configure(EntityTypeBuilder<Departure> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Airport).IsRequired();
            builder.Property(x => x.IATA).IsRequired();
            builder.Property(x => x.Scheduled).IsRequired();
            builder.Property(x => x.Estimated).IsRequired();

            builder.HasOne(x => x.Flight)
                .WithOne(x => x.Departure)
                .HasForeignKey<Flight>(x => x.DepartureId);
        }
    }
}
