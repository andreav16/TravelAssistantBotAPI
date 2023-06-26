using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Core.Entities.FlightEntities.FileData
{
    public class FlightDataResponse
    {
        public PaginationData Pagination { get; set; }
        public List<FlightData> Data { get; set; }
    }
}
