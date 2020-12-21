using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using predictionsapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace predictionsapi.Services
{
    public abstract class ApiServiceDC<T>
    {
        private readonly ILogger<T> _logger;
        private readonly HttpClient _client;
        private readonly string _key;
        protected ApiServiceDC(IOptions<ApiSettings> settings, ILogger<T> logger, HttpClient client)
        {
            _key = settings.Value.APIKeyDC;
            _logger = logger;
            _client = client;
        }

        protected async Task<HttpResponseMessage> MakeApiCall(HttpRequestMessage request)
        {
            try
            {
                request.Headers.Add("api_key", _key);
                return await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            }
            catch (Exception ex)
            {
                _logger.LogError($"In ApiServiceDC.MakeApiCall() method: Error when executing Get request: {ex.Message}");
                throw ex;
            }
        }
    }
}
