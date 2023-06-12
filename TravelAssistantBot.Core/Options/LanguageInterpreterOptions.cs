using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Core.Options
{
    public class LanguageInterpreterOptions
    {
        public const string ConfigurationKey = "LanguageInterpreter";
        public string BaseUrl { get; set; }
        public string SubscriptionKey { get; set; }
        public string ProjectName { get; set; }
    }
}