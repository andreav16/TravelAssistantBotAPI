using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc;
using TravelAssistantBot.Api.DataTransferObjects.EventDataTransferObjects;
using TravelAssistantBot.Core.EventManager;

namespace TravelAssistantBot.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : TravelAssistantBotBaseController
    {
        private readonly IEventService eventService;
        private readonly IMapper mapper;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            this.eventService = eventService;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> AddEventAsync(AddEventDataTransferObject eventToAdd)
        {
            var result = await this.eventService.AddAsync(this.mapper.Map<Core.Entities.CalendarEvent>(eventToAdd));
            var addedEvent = this.mapper.Map<EventDetailsDataTransferObject>(result.Result);
            return result.Succeeded ? Ok(addedEvent) : GetErrorResult<Event>(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventsAsync()
        {
            var result = await this.eventService.GetAllAsync();
            var events = this.mapper.Map<IList<EventDetailsDataTransferObject>>(result.Result);
            return result.Succeeded ? Ok(events) : GetErrorResult<IList<Event>>(result);
        }

        [HttpDelete]
        public async Task DeleteEventAsync(string eventId)
        {
            await this.eventService.RemoveAsync(eventId);
        }
    }
}
