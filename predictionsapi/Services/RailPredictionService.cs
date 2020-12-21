using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using predictionsapi.Interfaces;
using predictionsapi.Models;
using predictionsapi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace predictionsapi
{
    public class RailPredictionService : ApiServiceDC<RailPredictionService>, IPredictionService
    {
        public RailPredictionService(ILogger<RailPredictionService> logger, HttpClient client) : base(logger, client)
        {
        }

        public async Task<IEnumerable<RailPredictionDCResponse>> GetRailPredictionsDC(RailPredictionDCRequest request)
        {

            var req = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.wmata.com/StationPrediction.svc/json/GetPrediction/" + (request.StationCode ?? "All"))
            };

            return await MakeRailPredictionsDCApiCall(req);
        }

        internal async Task<IEnumerable<RailPredictionDCResponse>> MakeRailPredictionsDCApiCall(HttpRequestMessage request)
        {
            var response = await MakeApiCall(request);

            await using var responseStream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<RailPredictionDCApiResponse>(responseStream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Trains;
        }
    }
}
