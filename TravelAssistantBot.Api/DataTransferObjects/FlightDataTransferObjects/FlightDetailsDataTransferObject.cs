namespace TravelAssistantBot.Api.DataTransferObjects.FlightDataTransferObjects
{
    public class FlightDetailsDataTransferObject
    {
        public DateTime? FlightDate { get; set; }
        public string FlightStatus { get; set; }
        public Departure Departure { get; set; }
        public Arrival Arrival { get; set; }
        public Airline Airline { get; set; }
        public FlightInfo Flight { get; set; }
    }

    public class Departure
    {
        public string Airport { get; set; }
        public string Timezone { get; set; }
        public string IATA { get; set; }
        public DateTime? Scheduled { get; set; }
    }

    public class Arrival
    {
        public string Airport { get; set; }
        public string Timezone { get; set; }
        public string IATA { get; set; }
        public DateTime? Scheduled { get; set; }
    }

    public class Airline
    {
        public string Name { get; set; }
        public string IATA { get; set; }
    }

    public class FlightInfo
    {
        public string Number { get; set; }
        public string IATA { get; set; }
        
    }

    //public class CodeShare
    //{
    //    public string AirlineName { get; set; }
    //    public string AirlineIATA { get; set; }
    //}
}
