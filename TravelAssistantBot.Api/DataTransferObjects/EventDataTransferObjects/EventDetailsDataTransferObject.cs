using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Api.DataTransferObjects.EventDataTransferObjects
{
    public class EventDetailsDataTransferObject
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
