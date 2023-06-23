using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Core.Entities.FlightEntities
{
    public class FlightInfo
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string IATA { get; set; }

        //public int CodeShareId { get; set; }
        //public CodeShare? CodeShare { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}
