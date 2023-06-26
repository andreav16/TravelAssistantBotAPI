using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAssistantBot.Core.IATA_Manager
{
    public class IATAFileReader
    {
        public string getFilePath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string solutionDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.FullName;
            string filePath = Path.Combine(solutionDirectory, "IATA_Code.json");
            return File.ReadAllText(filePath);
        }
    }
}
