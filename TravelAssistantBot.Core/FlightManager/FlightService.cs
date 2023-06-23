using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Entities.FlightEntities;

namespace TravelAssistantBot.Core.FlightManager
{
    public class FlightsService : IFlightService
    {
        private readonly IRepository<Flight> flightRepository;

        public FlightsService(IRepository<Flight> flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public async Task<OperationResult<List<Flight>>> GetFlightsByDepartureAndArrivalAsync(string from, string to)
        {
            if (string.IsNullOrEmpty(from))
            {
                return new OperationResult<List<Flight>>(new Error
                {
                    Code = ErrorCode.InternalError,
                    Message = "Departure location is required."
                });
            }

            if (string.IsNullOrEmpty(to))
            {
                return new OperationResult<List<Flight>>(new Error
                {
                    Code = ErrorCode.InternalError,
                    Message = "Arrival location is required."
                });
            }

            // Filtrar los vuelos por lugar de partida y destino
            var flights = flightRepository.Filter(f =>
                f.Departure.Airport.Equals(from, StringComparison.OrdinalIgnoreCase) &&
                f.Arrival.Airport.Equals(to, StringComparison.OrdinalIgnoreCase));

            if (!flights.Any())
            {
                return new OperationResult<List<Flight>>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = "No flights found for the provided locations."
                });
            }

            return new OperationResult<List<Flight>>(flights.ToList());
        }
    }
}
