using predictionsapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace predictionsapi.Interfaces
{
    public interface IPredictionService
    {
        Task<IEnumerable<RailPredictionDCResponse>> GetRailPredictionsDC(RailPredictionDCRequest request);
    }
}
