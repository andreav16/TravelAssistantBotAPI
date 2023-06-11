using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using TravelAssistantBot.Core.Options;

namespace TravelAssistantBot.Core.ConversationalLanguageInterpreter
{
    public class LanguageInterpreter : ILanguageInterpreter
    {
        private readonly LanguageInterpreterOptions _options;
        private readonly HttpClient _httpClient;
        private readonly ILogger<LanguageInterpreter> _logger;

        public LanguageInterpreter(
       IOptions<LanguageInterpreterOptions> options,
       HttpClient httpClient,
       ILogger<LanguageInterpreter> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _options = options.Value;
        }
        public async Task<LanguageInterpreterResult> InterpretAsync(string query)
        {
            //en el header va la key
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _options.SubscriptionKey);
            var request = new LanguageInterpreterRequest(
                "Conversation",
                new AnalysisInput(
                    new ConversationItem(
                        "1",
                        "1",
                        query))
                , new Parameter(_options.ProjectName, "first-deployment", "TextElement_V8"));

            var requestJson = JsonSerializer.Serialize(request);
            var response = await _httpClient.PostAsync(_options.BaseUrl, new StringContent(requestJson, Encoding.UTF8, "application/json"));
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LanguageInterpreterResult>(data);
            _logger.LogInformation(result.result.ToString());
            /*   var x = result.result.prediction.entities;
               Console.WriteLine(x);*/
            return result;
        }
    }
}
