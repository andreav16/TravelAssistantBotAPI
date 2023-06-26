using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAssistantBot.Core.Interfaces;

namespace TravelAssistantBot.Core.Factory
{
    public interface ICityCodeFinderFactory
    {
        ICityCodeFinder CreateCityCodeFinder();
    }
}
