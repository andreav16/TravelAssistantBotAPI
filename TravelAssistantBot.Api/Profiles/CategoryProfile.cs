using System;
using AutoMapper;
using TravelAssistantBot.Api.DataTransferObjects.EventDataTransferObjects;
using TravelAssistantBot.Core.Entities;

namespace TravelAssistantBot.Api.Profiles
{
    public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<EventDetailsDataTransferObject, AddEventDataTransferObject>();
            CreateMap<AddEventDataTransferObject, EventDetailsDataTransferObject>();
			CreateMap<AddEventDataTransferObject, Core.Entities.CalendarEvent>();
			CreateMap<Core.Entities.CalendarEvent, AddEventDataTransferObject>();
			CreateMap<EventDetailsDataTransferObject, Core.Entities.CalendarEvent>();
			CreateMap<Core.Entities.CalendarEvent, EventDetailsDataTransferObject>();
        }
	}
}

