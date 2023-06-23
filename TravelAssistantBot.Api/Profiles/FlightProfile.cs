
using AutoMapper;
using TravelAssistantBot.Api.DataTransferObjects.FlightDataTransferObjects;

namespace TravelAssistantBot.Api.Profiles
{
    public class FlightProfileProfile : Profile
    {
        public FlightProfileProfile()
        {
            CreateMap<FlightDetailsDataTransferObject, Core.Entities.FlightEntities.Flight>();
            CreateMap<Core.Entities.FlightEntities.Flight, FlightDetailsDataTransferObject>();
        }

    }
}
