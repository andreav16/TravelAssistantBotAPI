using Microsoft.EntityFrameworkCore;

using TravelAssistantBot.Core.Entities.FlightEntities;
using TravelAssistantBot.Core.Factory;
using TravelAssistantBot.Core.IATA_Manager;
using TravelAssistantBot.Core.Interfaces;


namespace TravelAssistantBot.Core.FlightManager
{
    public class FlightsService : IFlightService
    {
        
        private readonly IRepository<Flight> flightRepository;
        ICityCodeFinderFactory cityCodeFinderFactory = new CityCodeFinderFactory();

        public FlightsService(IRepository<Flight> flightRepository)
        {
            this.flightRepository = flightRepository;
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

            var flight = await flightRepository.GetQueryable()
                .Include(f => f.Departure)
                .Include(f => f.Arrival)
                .Include(f => f.Airline)
                .Include(f => f.FlightInfo)
                .FirstOrDefaultAsync(f => f.FlightInfo.IATA == flightCode);

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

        public async Task<OperationResult<List<Flight>>> GetFlightsByDepartureAndArrivalAsync(string from, string to, DateTime date)
        {
            //Implementar el factory
            ICityCodeFinder cityCodeFinder = cityCodeFinderFactory.CreateCityCodeFinder();

            string departure = cityCodeFinder.FindCityCode(from);
            string arrival = cityCodeFinder.FindCityCode(to);


            if (string.IsNullOrEmpty(departure))
            {
                return new OperationResult<List<Flight>>(new Error
                {
                    Code = ErrorCode.InternalError,
                    Message = "Departure location is required."
                });
            }

            if (string.IsNullOrEmpty(arrival))
            {
                return new OperationResult<List<Flight>>(new Error
                {
                    Code = ErrorCode.InternalError,
                    Message = "Arrival location is required."
                });
            }

            var flights = await flightRepository.GetQueryable()
                .Include(f => f.Departure)
                .Include(f => f.Arrival)
                .Include(f => f.Airline)
                .Include(f => f.FlightInfo)
                .Where(f => f.Departure.IATA == departure && f.Arrival.IATA == arrival && f.FlightDate.CompareTo(date) == 0)
                .ToListAsync();

            if (!flights.Any())
            {
                return new OperationResult<List<Flight>>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = "No flights found for the provided locations."
                });
            }

            return new OperationResult<List<Flight>>(flights);
        }
    }
}
