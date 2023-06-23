using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAssistantBot.Api.DataTransferObjects.FlightDataTransferObjects;
using TravelAssistantBot.Core.FlightManager;

namespace TravelAssistantBot.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : TravelAssistantBotBaseController
    {
        private readonly IFlightService flightService;
        private readonly IMapper mapper;

        public FlightsController(IFlightService flightService, IMapper mapper)
        {
            this.flightService = flightService;
            this.mapper = mapper;
        }

        [HttpGet("{flightCode}")]
        public async Task<IActionResult> GetFlightByFlightCodeAsync([FromRoute] string flightCode)
        {
            var result = await flightService.GetFlightByFlightCodeAsync(flightCode);
            var flight = mapper.Map<FlightDetailsDataTransferObject>(result.Result);
            return result.Succeeded ? Ok(flight) : GetErrorResult(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetFlightsByDepartureAndArrivalAsync([FromQuery] string from, [FromQuery] string to, [FromQuery] DateTime date)
        {
            var result = await flightService.GetFlightsByDepartureAndArrivalAsync(from, to, date);
            var flight = mapper.Map<List<FlightDetailsDataTransferObject>>(result.Result);
            return result.Succeeded ?  Ok(flight) : GetErrorResult(result);
        }
    }

}
