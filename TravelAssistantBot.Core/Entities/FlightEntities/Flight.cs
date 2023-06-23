using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Core.Entities.FlightEntities
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime FlightDate { get; set; }
        public string FlightStatus { get; set; }

        public int DepartureId { get; set; }
        public Departure Departure { get; set; }

        public int ArrivalId { get; set; }
        public Arrival Arrival { get; set; }

        public int AirlineId { get; set; }
        public Airline Airline { get; set; }

        public int FlightInfoId { get; set; }
        public FlightInfo FlightInfo { get; set; }


        public string? Aircraft { get; set; }
        public string? Live { get; set; }
    }
}
