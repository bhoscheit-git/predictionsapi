﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace predictionsapi.Models
{
    public class RailPredictionDCApiResponse
    {
        public List<RailPredictionDCResponse> Trains { get; set; }
    }
}
