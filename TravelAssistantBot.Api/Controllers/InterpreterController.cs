using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAssistantBot.Api.DataTransferObjects.InterpreterDataTransferObjects;
using TravelAssistantBot.Core.ConversationalLanguageInterpreter;

namespace TravelAssistantBot.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterpreterController : TravelAssistantBotBaseController
    {
        public record Entity(string category, string text, int offset, int length, decimal confidenceScore);
        public record Intent(string category, decimal confidenceScore);
        public record Prediction(string topIntent, string projectKind, Intent[] intents, Entity[] entities);
        public record Result(string query, Prediction prediction);
        public record LanguageInterpreterResult(string kind, Result result);
        private readonly ILanguageInterpreter languageInterpreter;

        public InterpreterController(ILanguageInterpreter languageInterpreter)
        {
            this.languageInterpreter = languageInterpreter;
        }

        [HttpGet("{query}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> InterpretAsync([FromRoute] string query)
        {
            var result = await this.languageInterpreter.InterpretAsync(query);
            var eventToAdd = new ChatBotInfoDataTransferObject
            {
                Action = result.result.prediction.topIntent,
                Entities = result.result.prediction.entities.Select(e => new ChatBotInfoDataTransferObject.Entity(e.category, e.text, e.offset, e.length, e.confidenceScore)).ToList()
            };
            return Ok(eventToAdd);

        }
    }
}