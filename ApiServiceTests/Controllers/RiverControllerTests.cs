using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoService.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using GeoService.Interfaces;
using Microsoft.Extensions.Logging;
using GeoService.Model;
using Microsoft.AspNetCore.Mvc;
using DomeinLaag.Exceptions;

namespace GeoService.Controllers.Tests
{
    [TestClass()]
    public class RiverControllerTests
    {
        private readonly Mock<IApiWork> MockRepo;
        private readonly Mock<ILogger<ContinentController>> MockLogger;
        private readonly RiverController RiverMockController;
        public RiverControllerTests()
        {
            MockLogger = new Mock<ILogger<ContinentController>>();
            MockRepo = new Mock<IApiWork>();

            RiverMockController = new RiverController(MockRepo.Object, MockLogger.Object);
        }

        [TestMethod]
        public void CreateRiverTest_ReturnsCreatedAtAction()
        {
            RiverDTOIn r = new RiverDTOIn();
            RiverDTOOut rOut = new RiverDTOOut() {Name="testName" };
            MockRepo.Setup(m => m.AddRiver(r)).Returns(rOut);
            var result = RiverMockController.CreateRiver(r);
            var temp = result.Result as CreatedAtActionResult;


            Assert.IsTrue(result.Result is CreatedAtActionResult);
            Assert.IsTrue(temp.Value.Equals(rOut));
        }
        [TestMethod]
        public void CreateRiverTest_DomainThrowsDomainException_ReturnsNotFound()
        {
            RiverDTOIn r = new RiverDTOIn();
            RiverDTOOut rOut = new RiverDTOOut() { Name = "testName" };
            MockRepo.Setup(m => m.AddRiver(r)).Throws(new DomainException(""));
            var result = RiverMockController.CreateRiver(r);


            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void CreateRiverTest_ThrowsOtherException_ReturnsBadRequest()
        {
            RiverDTOIn r = new RiverDTOIn();
            RiverDTOOut rOut = new RiverDTOOut() { Name = "testName" };
            MockRepo.Setup(m => m.AddRiver(r)).Throws(new Exception(""));
            var result = RiverMockController.CreateRiver(r);


            Assert.IsTrue(result.Result is BadRequestResult);
        }

        [TestMethod]
        public void GetRiver_ReturnsCorrectValues()
        {

            RiverDTOIn r = new RiverDTOIn();
            RiverDTOOut rOut = new RiverDTOOut() { Name = "testName" };
            MockRepo.Setup(m => m.GetRiverForId(1)).Returns(rOut);
            var result = RiverMockController.GetRiver(1);
            var temp = result.Result as OkObjectResult;
            Assert.IsTrue(temp.Value.Equals(rOut));
        }
        [TestMethod]
        public void GetRiver_WithNonExistingId_Returns404()
        {
            RiverDTOIn r = new RiverDTOIn();
            RiverDTOOut rOut = new RiverDTOOut() { Name = "testName" };
            MockRepo.Setup(m => m.GetRiverForId(1)).Throws(new DomainException("test"));
            var result = RiverMockController.GetRiver(1);
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void UpdateRiver_IncorrectId_ReturnsBadRequest()
        {
            RiverDTOIn r = new RiverDTOIn() { RiverId = 2 };
            RiverDTOOut rOut = new RiverDTOOut() { Name = "testName" };
            MockRepo.Setup(m => m.UpdateRiver(r)).Returns(rOut);
            var result = RiverMockController.UpdateRiver(1,r);
            Assert.IsTrue(result.Result is BadRequestObjectResult);
        }
        [TestMethod]
        public void UpdateRiver_CorrectId_ReturnsCreatedAtAction()
        {
            RiverDTOIn r = new RiverDTOIn() { RiverId = 1 };
            RiverDTOOut rOut = new RiverDTOOut() { Name = "testName" };
            MockRepo.Setup(m => m.UpdateRiver(r)).Returns(rOut);
            var result = RiverMockController.UpdateRiver(1, r);
            var temp = result.Result as CreatedAtActionResult;
            Assert.IsTrue(result.Result is CreatedAtActionResult);
            Assert.IsTrue(temp.Value.Equals(rOut));
        }
        [TestMethod]
        public void UpdateRiver_DomainLayerThrowsDomainException_ReturnsNotFound()
        {
            RiverDTOIn r = new RiverDTOIn() { RiverId = 1 };
            RiverDTOOut rOut = new RiverDTOOut() { Name = "testName" };
            MockRepo.Setup(m => m.UpdateRiver(r)).Throws(new DomainException("Test"));
            var result = RiverMockController.UpdateRiver(1, r);
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void UpdateRiver_ExceptionIsThrown_ReturnsBadRequest()
        {
            RiverDTOIn r = new RiverDTOIn() { RiverId = 1 };
            RiverDTOOut rOut = new RiverDTOOut() { Name = "testName" };
            MockRepo.Setup(m => m.UpdateRiver(r)).Throws(new Exception("Test"));
            var result = RiverMockController.UpdateRiver(1, r);
            Assert.IsTrue(result.Result is BadRequestResult);
        }
        [TestMethod]
        public void DeleteRiver_DomainLayerThrowsDomainException_ReturnsNotFound()
        {
            RiverDTOIn r = new RiverDTOIn() { RiverId = 1 };
            RiverDTOOut rOut = new RiverDTOOut() { Name = "testName" };
            MockRepo.Setup(m => m.DeleteRiver(1)).Throws(new DomainException("Test"));
            var result = RiverMockController.DeleteRiver(1);
            Assert.IsTrue(result is NotFoundObjectResult);
        }
        [TestMethod]
        public void DeleteRiver_DomainLayerThrowsException_ReturnsBadRequest()
        {
            RiverDTOIn r = new RiverDTOIn() { RiverId = 1 };
            RiverDTOOut rOut = new RiverDTOOut() { Name = "testName" };
            MockRepo.Setup(m => m.DeleteRiver(1)).Throws(new Exception("Test"));
            var result = RiverMockController.DeleteRiver(1);
            Assert.IsTrue(result is BadRequestResult);
        }
    }
}