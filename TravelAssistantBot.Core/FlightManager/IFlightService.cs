using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities.FlightEntities;

namespace TravelAssistantBot.Core.FlightManager
{
    public interface IFlightService
    {
        Task<OperationResult<List<Flight>>> GetFlightsByDepartureAndArrivalAsync(string from, string to, DateTime date);
        Task<OperationResult<Flight>> GetFlightByFlightCodeAsync(string flightCode);
    }
}
