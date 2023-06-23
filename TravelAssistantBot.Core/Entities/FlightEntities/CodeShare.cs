using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Core.Entities.FlightEntities
{
    public class CodeShare
    {
        public int Id { get; set; }
        public string? AirlineName { get; set; }
        public string? AirlineIATA { get; set; }
        public string? FlightNumber { get; set; }
        public string? FlightIATA { get; set; }

        public int FlighInfoId { get; set; }
        public FlightInfo FlightInfo { get; set; }
    }
}
