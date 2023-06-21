using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Core.Entities.FlightEntities
{
    public class FlightData
    {
        public DateTime FlightDate { get; set; }
        public string FlightStatus { get; set; }
        public DepartureData Departure { get; set; }
        public ArrivalData Arrival { get; set; }
        public AirlineData Airline { get; set; }
        public FlightInfoData Flight { get; set; }
        public string Aircraft { get; set; }
        public string Live { get; set; }
    }

    public class DepartureData
    {
        public string Airport { get; set; }
        public string Timezone { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public string Terminal { get; set; }
        public string Gate { get; set; }
        public string Delay { get; set; }
        public DateTime Scheduled { get; set; }
        public DateTime Estimated { get; set; }
        public DateTime Actual { get; set; }
        public DateTime EstimatedRunway { get; set; }
        public DateTime ActualRunway { get; set; }
    }

    public class ArrivalData
    {
        public string Airport { get; set; }
        public string Timezone { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public string Terminal { get; set; }
        public string Gate { get; set; }
        public string Baggage { get; set; }
        public string Delay { get; set; }
        public DateTime Scheduled { get; set; }
        public DateTime Estimated { get; set; }
        public DateTime Actual { get; set; }
        public DateTime EstimatedRunway { get; set; }
        public DateTime ActualRunway { get; set; }
    }

    public class AirlineData
    {
        public string Name { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
    }

    public class FlightInfoData
    {
        public string Number { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public CodeShareData CodeShare { get; set; }
    }

    public class CodeShareData
    {
        public string AirlineName { get; set; }
        public string AirlineIATA { get; set; }
        public string AirlineICAO { get; set; }
        public string FlightNumber { get; set; }
        public string FlightIATA { get; set; }
        public string FlightICAO { get; set; }
    }

}
