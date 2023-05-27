using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities;

namespace TravelAssistantBot.Core.EventManager
{
    public class EventService : IEventService
    {
        public async Task<UserCredential> GetCredentialsAsync()
        {
            string[] scopes = { CalendarService.Scope.Calendar };

            UserCredential credential;
            using (var stream = new FileStream("C:\\Users\\leoth\\Documents\\2 tri 2023\\Vanguardia\\Proyecto VANG\\PruebaGoogleCalendar\\PruebaGoogleCalendar\\PruebaGoogleCalendar\\GoogleCalendarClient.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            return new OperationResult<UserCredential>(credential);
        }

        public async Task<OperationResult<Event>> AddAsync(CalendarEvent eventData)
        {
            UserCredential credential = GetCredentialsAsync().Result;

            // Inicializar el servicio de calendario
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "VirtualAssistant"
            });

            // Crear el objeto de evento
            Event newEvent = new Event
            {
                Summary = eventData.Summary,
                Description = eventData.Description,
                Start = new EventDateTime { DateTime = eventData.StartDateTime },
                End = new EventDateTime { DateTime = eventData.EndDateTime }
            };

            // Insertar el evento en el calendario
            EventsResource.InsertRequest request = service.Events.Insert(newEvent, "primary");
            Event createdEvent = await request.ExecuteAsync();

            return createdEvent;
        }

        public async Task<OperationResult<IList<Event>>> GetAllAsync()
        {
            UserCredential credential = GetCredentialsAsync().Result;

            // Inicializar el servicio de calendario
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "VirtualAssistant"
            });

            // Obtener eventos del calendario
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = await request.ExecuteAsync();
            IList<Event> items = events.Items;

            return items.ToList();
        }

        public async Task RemoveAsync(string eventId)
        {
            UserCredential credential = GetCredentialsAsync().Result;

            // Inicializar el servicio de calendario
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "VirtualAssistant"
            });

            // Borrar el evento del calendario
            EventsResource.DeleteRequest request = service.Events.Delete("primary", eventId);
            await request.ExecuteAsync();
        }
    }
}
