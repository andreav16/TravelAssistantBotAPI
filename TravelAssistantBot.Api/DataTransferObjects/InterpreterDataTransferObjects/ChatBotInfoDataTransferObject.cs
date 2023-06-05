namespace TravelAssistantBot.Api.DataTransferObjects.InterpreterDataTransferObjects
{
    public class ChatBotInfoDataTransferObject
    {
        public record Entity(string category, string text, int offset, int length, decimal confidenceScore);

        public string Action { get; set; }
        public ICollection<Entity> Entities { get; set; }
    }
}
