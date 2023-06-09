using System;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using TravelAssistantBot.Api.DataTransferObjects.EventDataTransferObjects;
using TravelAssistantBot.Core.Entities;

namespace TravelAssistantBot.Api.Profiles
{
    public class EventProfile : Profile
	{
		public EventProfile()
		{
			CreateMap<AddEventDataTransferObject, Core.Entities.CalendarEvent>();
            CreateMap<EventDetailsDataTransferObject, Event>(); //origen a destino
            CreateMap<Event, EventDetailsDataTransferObject>();
        }
	}
}

