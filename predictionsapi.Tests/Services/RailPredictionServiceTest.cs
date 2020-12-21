using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using predictionsapi.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace predictionsapi.Tests.Services
{
    [TestFixture]
    public class RailPredictionServiceTest
    {
        private Mock<ILogger<RailPredictionService>> _logger;
        private RailPredictionDCRequest _request;
        private RailPredictionDCApiResponse _response;

        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<ILogger<RailPredictionService>>();
            _request = new RailPredictionDCRequest();
            _response = new RailPredictionDCApiResponse();
        }
        
        [Test]
        public async Task MakeRailPredictionsDCApiCall_WithStationCode_ReturnsListOfRailPredictionDCResponse()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(_response, typeof(RailPredictionDCApiResponse), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }))
                }).Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://test.com")
            };

            var service = new RailPredictionService(_logger.Object, httpClient);
            var response = await service.GetRailPredictionsDC(_request);

            Assert.IsNotNull(_response);
        }
    }
}
