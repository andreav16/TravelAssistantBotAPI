using System;
using System.Collections.Generic;
using System.Linq;

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

        public async Task<OperationResult<List<Flight>>> GetFlightsByDepartureAndArrivalAsync(string from, string to, DateTime date)
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
                f.Departure.Airport.StartsWith(from, StringComparison.OrdinalIgnoreCase) &&
                f.Arrival.Airport.StartsWith(to, StringComparison.OrdinalIgnoreCase) &&
                f.FlightDate.Equals(date));

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

        public async Task<OperationResult<Flight>> GetFlightByFlightCodeAsync(string flightCode)
        {
            if (string.IsNullOrEmpty(flightCode))
            {
                return new OperationResult<Flight>(new Error
                {
                    Code = ErrorCode.InternalError,
                    Message = "Flight code cannot be null or empty."
                });
            }

            var flight = flightRepository.Filter(f => f.FlightInfo.IATA == flightCode).FirstOrDefault();

            if (flight == null)
            {
                return new OperationResult<Flight>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = "No flights found for the provided IATA."
                });
            }

            return new OperationResult<Flight>(flight);
        }

    }
}
