using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAssistantBot.Core.Entities.FlightEntities;

namespace TravelAssistantBot.Infrastructure.EntityFramework.Configuration.FlightConfiguration
{
    public class CodeShareEntityConfiguration : IEntityTypeConfiguration<CodeShare>
    {
        public void Configure(EntityTypeBuilder<CodeShare> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AirlineName).IsRequired();
            builder.Property(x => x.AirlineIATA).IsRequired();
            builder.Property(x => x.AirlineICAO).IsRequired();
            builder.Property(x => x.FlightNumber).IsRequired();
            builder.Property(x => x.FlightIATA).IsRequired();
            builder.Property(x => x.FlightICAO).IsRequired();

            builder.HasOne(x => x.FlightInfo)
                .WithOne(x => x.CodeShare)
                .HasForeignKey<FlightInfo>(x => x.CodeShareId);
        }
    }
}
