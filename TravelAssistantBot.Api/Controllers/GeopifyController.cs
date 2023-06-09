using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAssistantBot.Api.DataTransferObjects.GeopifyDataTransferObjects;
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
            Console.WriteLine(cityname);

            var result = await this.geopifyService.GetCountryAsync(cityname);
            var geocode = this.mapper.Map<CountryIdDataTransferObjects>(result.Result.Features[0].Properties.Place_id);
            //Console.WriteLine(result.Result.Features[0].Properties.Place_id);
            return result.Succeeded ? Ok(result.Result.Features[0].Properties.Place_id) : BadRequest(geocode);
        }

        [HttpGet("{placeCategory}/{countryId}/{cantDatos}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlacesByCategoriesAsync(string placeCategory, string countryId, int cantDatos)
        {
            var result = await this.geopifyService.GetPlacesDataAsync(countryId, placeCategory, cantDatos);
            if(result.Succeeded)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest(result.Result);
            }
        }


    }
}
