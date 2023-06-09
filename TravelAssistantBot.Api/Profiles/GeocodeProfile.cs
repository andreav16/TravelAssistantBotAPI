using AutoMapper;
using TravelAssistantBot.Api.DataTransferObjects.GeopifyDataTransferObjects;
using TravelAssistantBot.Core.Entities.GeopifyEntities.CountryEntities;

namespace TravelAssistantBot.Api.Profiles
{
    public class GeocodeProfile :Profile
    {
        public GeocodeProfile() 
        {
            CreateMap<string, CountryIdDataTransferObjects>();
            CreateMap<CountryIdDataTransferObjects, GeocodeGroup>();
            CreateMap<GeocodeGroup, CountryIdDataTransferObjects>();
        }
    }
}
