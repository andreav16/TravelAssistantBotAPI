using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Api.DataTransferObjects.EventDataTransferObjects
{
    public class EventDetailsDataTransferObject
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }
    }
}
