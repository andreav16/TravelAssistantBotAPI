using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities;

namespace TravelAssistantBot.Core.EventManager
{
    public interface IEventService
    {
        Task<UserCredential> GetCredentialsAsync();
        Task<OperationResult<IList<Event>>> GetAllAsync();
        Task<OperationResult<Event>> AddAsync(CalendarEvent eventData);
        Task RemoveAsync(string eventId);

    }
}
