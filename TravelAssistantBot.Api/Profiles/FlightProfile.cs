
using AutoMapper;
using TravelAssistantBot.Api.DataTransferObjects.FlightDataTransferObjects;

namespace TravelAssistantBot.Api.Profiles
{
    public class FlightProfileProfile : Profile
    {
        public FlightProfileProfile()
        {
            CreateMap<FlightDetailsDataTransferObject, Core.Entities.FlightEntities.Flight>()
                .ForMember(dest => dest.Departure, opt => opt.MapFrom(src => src.FlightDeparture))
                .ForMember(dest => dest.Arrival, opt => opt.MapFrom(src => src.FlightArrival))
                .ForMember(dest => dest.Airline, opt => opt.MapFrom(src => src.FlightAirline))
                .ForMember(dest => dest.FlightInfo, opt => opt.MapFrom(src => src.Flight));

            CreateMap<Core.Entities.FlightEntities.Flight, FlightDetailsDataTransferObject>()
                .ForMember(dest => dest.FlightDeparture, opt => opt.MapFrom(src => src.Departure))
                .ForMember(dest => dest.FlightArrival, opt => opt.MapFrom(src => src.Arrival))
                .ForMember(dest => dest.FlightAirline, opt => opt.MapFrom(src => src.Airline))
                .ForMember(dest => dest.Flight, opt => opt.MapFrom(src => src.FlightInfo));

            CreateMap<Core.Entities.FlightEntities.Departure, FlightDetailsDataTransferObject.Departure>().ReverseMap();
            CreateMap<Core.Entities.FlightEntities.Arrival, FlightDetailsDataTransferObject.Arrival>().ReverseMap();
            CreateMap<Core.Entities.FlightEntities.Airline, FlightDetailsDataTransferObject.Airline>().ReverseMap();
            CreateMap<Core.Entities.FlightEntities.FlightInfo, FlightDetailsDataTransferObject.FlightInfo>().ReverseMap();
        }
    }
}
