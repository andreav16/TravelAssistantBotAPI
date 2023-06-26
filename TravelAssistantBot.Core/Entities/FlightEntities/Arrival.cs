using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Core.Entities.FlightEntities
{
    public class Arrival
    {
        public int Id { get; set; }
        public string Airport { get; set; }
        public string IATA { get; set; }
        public string? Terminal { get; set; }
        public string? Gate { get; set; }
        public string? Delay { get; set; }
        public DateTime Scheduled { get; set; }
        public DateTime Estimated { get; set; }
        public DateTime? Actual { get; set; }
       
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}
