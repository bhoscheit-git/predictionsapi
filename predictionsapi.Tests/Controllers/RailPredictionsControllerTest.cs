using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using predictionsapi.Controllers;
using predictionsapi.Interfaces;
using predictionsapi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace predictionsapi.Tests.Controllers
{
    [TestFixture]
    public class RailPredictionsControllerTest
    {
        private RailPredictionsController _controller;
        private Mock<ILogger<RailPredictionsController>> _loggerMock;
        private Mock<IPredictionService> _predictionServiceMock;
        private RailPredictionDCRequest _request;
        private RailPredictionDCResponse _response;

        private const int StatusCode500 = 500;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<RailPredictionsController>>();
            _predictionServiceMock = new Mock<IPredictionService>();
            _controller = new RailPredictionsController(_loggerMock.Object, _predictionServiceMock.Object);
            _request = new RailPredictionDCRequest();
            _response = new RailPredictionDCResponse();
        }
        
        [Test]
        public async Task GetDCRailPredictions_ExceptionThrown_ReturnsObjectResult()
        {
            _predictionServiceMock.Setup(mock => mock.GetRailPredictionsDC(It.IsAny<RailPredictionDCRequest>())).Throws(new Exception());
            
            var response = await _controller.GetRailPredictionsDC(_request);
            var objectResult = (ObjectResult)response.Result;

            _predictionServiceMock.Verify(mock => mock.GetRailPredictionsDC(It.IsAny<RailPredictionDCRequest>()), Times.Once);
            Assert.That(response.Result, Is.TypeOf<ObjectResult>());
            Assert.AreEqual(StatusCode500, objectResult.StatusCode);

        }

        [Test]
        public async Task GetDCRailPredictions_WithResults_ReturnsResults()
        {
            _predictionServiceMock.Setup(mock => mock.GetRailPredictionsDC(It.IsAny<RailPredictionDCRequest>())).ReturnsAsync(new List<RailPredictionDCResponse>() { _response });

            var response = await _controller.GetRailPredictionsDC(_request);
            var responseValue = response.Value;

            _predictionServiceMock.Verify(mock => mock.GetRailPredictionsDC(It.IsAny<RailPredictionDCRequest>()), Times.Once);
            Assert.That(response.Value, Is.TypeOf<RailPredictionDCResponse[]>());
            Assert.IsNotEmpty(responseValue);
        }
    }
}
