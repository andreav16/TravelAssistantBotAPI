using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAssistantBot.Core.EventManager;
using TravelAssistantBot.Core.GeopifyManager;

namespace TravelAssistantBot.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeopifyController : TravelAssistantBotBaseController
    {
        private readonly IGeopifyServices geopifyService;
        private readonly IMapper mapper;

        public GeopifyController(IGeopifyServices geopifyService, IMapper mapper)
        {
            this.geopifyService = geopifyService;
            this.mapper = mapper;
        }

        [HttpGet("{cityname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCountryIdAsync([FromRoute] string cityname)
        {
            var result = await this.geopifyService.GetCountryAsync(cityname);
            return result.Succeeded ? Ok(result.Result.Features[0].Properties.Place_id) : BadRequest(result.Result);
        }

        [HttpGet("{placeCategory}/{countryId}/{cantDatos}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlacesByCategoriesAsync([FromRoute] string placeCategory, [FromRoute] string countryId, [FromRoute] int cantDatos)
        {
            var result = await this.geopifyService.GetPlacesDataAsync(countryId, placeCategory, cantDatos);
            return result.Succeeded ? Ok(result.Result) : BadRequest(result.Result);
        }


    }
}
