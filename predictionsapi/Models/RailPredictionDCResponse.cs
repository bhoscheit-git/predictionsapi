using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace predictionsapi.Models
{
    public class RailPredictionDCResponse
    {
        public string Car { get; set; }
        public string Destination { get; set; }
        public string DestinationCode { get; set; }
        public string DestinationName { get; set; }
        public string Group { get; set; }
        public string Line { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string Min { get; set; }
    }
}
