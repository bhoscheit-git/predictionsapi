using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace predictionsapi.Controllers
{
    [Route("predictionsapi/v1")]
    [ApiController]
    public class PredictionsControllerBase : ControllerBase
    {
        protected ObjectResult InternalServerError(string message)
        {
            return StatusCode(500, message);
        }
    }
}
