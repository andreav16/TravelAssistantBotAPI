using AutoMapper;
using TravelAssistantBot.Api.DataTransferObjects.GeopifyDataTransferObjects;
using TravelAssistantBot.Core.Entities.GeopifyEntities.PlacesEntities;

namespace TravelAssistantBot.Api.Profiles
{
    public class PlacesProfile : Profile
    {
        public PlacesProfile() 
        {
            CreateMap<PlacesGroup, Places_Features>();
            CreateMap<Places_Features, Places_Properties>();
            CreateMap<PlacesGroup, PlacesCategoryDataTransferObjects>();
        }
    }
}
