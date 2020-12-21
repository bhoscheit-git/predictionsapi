using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using predictionsapi.Interfaces;
using predictionsapi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace predictionsapi.Controllers
{
    [Route("railpredictions")]
    [ApiController]
    public class RailPredictionsController : PredictionsControllerBase
    {
        private readonly ILogger<RailPredictionsController> _logger;
        private readonly IPredictionService _predictionService;

        public RailPredictionsController(ILogger<RailPredictionsController> logger, IPredictionService predictionService)
        {
            _logger = logger;
            _predictionService = predictionService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get DC Rail Predictions")]
        [Route("rail")]
        public async Task<ActionResult<IEnumerable<RailPredictionDCResponse>>> GetRailPredictionsDC([FromQuery] RailPredictionDCRequest request)
        {
            try
            {
                _logger.LogInformation("Rail Predictions Get Rail Predictions DC Endpoint");
                var response = await _predictionService.GetRailPredictionsDC(request);
                return response.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in Rail Prediction Endpoint: {ex.Message}");
                _logger.LogInformation($"Exception stack trace: {ex.StackTrace}");
                return InternalServerError(ex.Message);
            }
        }
    }
}
