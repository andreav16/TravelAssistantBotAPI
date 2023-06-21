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
    public class FlightInfoEntityConfiguration : IEntityTypeConfiguration<FlightInfo>
    {
        public void Configure(EntityTypeBuilder<FlightInfo> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.IATA).IsRequired();

            builder.HasOne(x => x.Flight)
                .WithOne(x => x.FlightInfo)
                .HasForeignKey<Flight>(x => x.AirlineId);
        }
    }
}
