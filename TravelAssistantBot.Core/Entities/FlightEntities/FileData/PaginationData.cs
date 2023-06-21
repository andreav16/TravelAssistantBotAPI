using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Core.Entities.FlightEntities.FileData
{
    public class PaginationData
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
    }
}
