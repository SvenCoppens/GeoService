using DomeinLaag.Exceptions;
using GeoService.Controllers;
using GeoService.Interfaces;
using GeoService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;

namespace GeoService.Controllers.Tests
{
    [TestClass]
    public class ContinentControllerTests
    {
        private readonly Mock<IApiWork> MockRepo;
        private readonly Mock<ILogger<ContinentController>> MockLogger;
        private readonly ContinentController ContinentMockController;
         public ContinentControllerTests()
        {
            MockLogger = new Mock<ILogger<ContinentController>>();
            MockRepo = new Mock<IApiWork>();

            ContinentMockController = new ContinentController(MockRepo.Object,MockLogger.Object);
        }

        #region Continent

        [TestMethod]
        public void CreateContinentTest_ReturnsCreatedAtAction()
        {
            ContinentDTOIn c = new ContinentDTOIn();
            ContinentDTOOut cOut = new ContinentDTOOut();
            MockRepo.Setup(m => m.AddContinent(c)).Returns(cOut);
            var result = ContinentMockController.CreateContinent(c);


            Assert.IsTrue(result.Result is CreatedAtActionResult);
        }
        [TestMethod]
        public void CreateContinentTest_ThrowsDomainException_ReturnsNotFound()
        {
            ContinentDTOIn c = new ContinentDTOIn();
            ContinentDTOOut cOut = new ContinentDTOOut();
            MockRepo.Setup(m => m.AddContinent(c)).Throws(new DomainException(""));
            var result = ContinentMockController.CreateContinent(c);


            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void CreateContinentTest_ThrowsOtherException_ReturnsBadRequest()
        {
            ContinentDTOIn c = new ContinentDTOIn();
            ContinentDTOOut cOut = new ContinentDTOOut();
            MockRepo.Setup(m => m.AddContinent(c)).Throws(new Exception(""));
            var result = ContinentMockController.CreateContinent(c);


            Assert.IsTrue(result.Result is BadRequestResult);
        }
        [TestMethod]
        public void CreateContinentTest_ReturnsContinentDTOOutWithCorrectValues()
        {
            ContinentDTOIn c = new ContinentDTOIn() { Name="testName"};
            ContinentDTOOut cOut = new ContinentDTOOut() {Name=c.Name};
            MockRepo.Setup(m => m.AddContinent(c)).Throws(new Exception(""));
            var result = ContinentMockController.CreateContinent(c);

            Assert.IsTrue(cOut.Name == c.Name);
            
        }


        [TestMethod]
        public void GetContinent_ReturnsCorrectValues()
        {
            ContinentDTOOut continent = new ContinentDTOOut() { ContinentId = "testLink", Name = "testName", Population = 35 };
            MockRepo.Setup(m => m.GetContinentForId(1)).Returns(continent);
            var result = ContinentMockController.GetContinent(1);
            var temp = result.Result as OkObjectResult;
            Assert.IsTrue(temp.Value.Equals(continent));
        }
        [TestMethod]
        public void GetContinent_WithNonExistingId_Returns404()
        {
            MockRepo.Setup(m => m.GetContinentForId(1)).Throws(new DomainException(""));
            var result = ContinentMockController.GetContinent(1);
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void GetContinent_WithCorrectId_ReturnsOK()
        {
            MockRepo.Setup(m => m.GetContinentForId(1)).Returns(new ContinentDTOOut());
            var result = ContinentMockController.GetContinent(1);
            Assert.IsTrue(result.Result is OkObjectResult);
        }
        [TestMethod]
        public void UpdateContinent_IncorrectId_ReturnsBadRequest()
        {
            ContinentDTOIn continent = new ContinentDTOIn() { ContinentId = 2 };
            MockRepo.Setup(m => m.GetContinentForId(1)).Returns(new ContinentDTOOut());
            var result = ContinentMockController.UpdateContinent(1, continent);
            Assert.IsTrue(result.Result is BadRequestObjectResult);
        }
        [TestMethod]
        public void UpdateContinent_CorrectId_ReturnsCreatedAtAction()
        {
            ContinentDTOIn continent = new ContinentDTOIn() { ContinentId = 1 };
            ContinentDTOOut output = new ContinentDTOOut() {ContinentId="1" };
            MockRepo.Setup(m => m.UpdateContinent(continent)).Returns(output);
            var result = ContinentMockController.UpdateContinent(1, continent);
            var temp = result.Result as CreatedAtActionResult;
            Assert.IsTrue(result.Result is CreatedAtActionResult);
            Assert.IsTrue(temp.Value.Equals(output));
        }
        [TestMethod]
        public void UpdateContinent_DomainLayerThrowsDomainException_ReturnsNotFound()
        {
            ContinentDTOIn continent = new ContinentDTOIn() { ContinentId = 1 };
            MockRepo.Setup(m => m.UpdateContinent(continent)).Throws(new DomainException("test"));
            var result = ContinentMockController.UpdateContinent(1, continent);
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void UpdateContinent_ExceptionIsThrown_ReturnsBadRequest()
        {
            ContinentDTOIn continent = new ContinentDTOIn() { ContinentId = 1 };
            MockRepo.Setup(m => m.UpdateContinent(continent)).Throws(new Exception("test"));
            var result = ContinentMockController.UpdateContinent(1, continent);
            Assert.IsTrue(result.Result is BadRequestResult);
        }
        [TestMethod]
        public void DeleteContinent_DomainLayerThrowsDomainException_ReturnsNotFound()
        {
            MockRepo.Setup(m => m.DeleteContinent(1)).Throws(new DomainException("test"));
            var result = ContinentMockController.DeleteContinent(1);
            Assert.IsTrue(result is NotFoundObjectResult);
        }
        [TestMethod]
        public void DeleteContinent_DomainLayerThrowsException_ReturnsBadRequest()
        {
            MockRepo.Setup(m => m.DeleteContinent(1)).Throws(new Exception("test"));
            var result = ContinentMockController.DeleteContinent(1);
            Assert.IsTrue(result is BadRequestResult);
        }
        #endregion

        #region Countries
        [TestMethod]
        public void CreateCountry_ReturnsCorrectObject_CreatedAtAction()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.AddCountry(countryIn)).Returns(countryOut);
            var result = ContinentMockController.CreateCountry(1, countryIn);
            var temp = result.Result as CreatedAtActionResult;
           Assert.IsTrue(temp.Value.Equals(countryOut));
            Assert.IsTrue(result.Result is CreatedAtActionResult);
        }
        [TestMethod]
        public void CreateCountry_IdsDoNotMatch_ReturnsBadRequest()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.AddCountry(countryIn)).Returns(countryOut);
            var result = ContinentMockController.CreateCountry(2, countryIn);
            Assert.IsTrue(result.Result is BadRequestObjectResult);
        }
        [TestMethod]
        public void CreateCountry_DomainThrowsDomainException_ReturnsNotFound()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.AddCountry(countryIn)).Throws(new DomainException("test"));
            var result = ContinentMockController.CreateCountry(1, countryIn);
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void CreateCountry_DomainThrowsException_ReturnsBadRequest()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.AddCountry(countryIn)).Throws(new Exception("test"));
            var result = ContinentMockController.CreateCountry(1, countryIn);
            Assert.IsTrue(result.Result is BadRequestResult);
        }
        [TestMethod]
        public void GetCountry_ReturnsOk_CorrectObject()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1,CountryId=1, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.GetCountryForId(1)).Returns(countryOut);
            var result = ContinentMockController.GetCountry(1, 1);
            var temp = result.Result as OkObjectResult;
            Assert.IsTrue(result.Result is OkObjectResult);
            Assert.IsTrue(temp.Value.Equals(countryOut));
        }
        [TestMethod]
        public void GetCountry_DomainThrowsDomainExcpetion_ReturnsNotFound()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, CountryId = 1, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.GetCountryForId(1)).Throws(new DomainException("test"));
            var result = ContinentMockController.GetCountry(1, 1);
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void GetCountry_DomainThrowsExcpetion_ReturnsBadRequest()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, CountryId = 1, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.GetCountryForId(1)).Throws(new Exception("test"));
            var result = ContinentMockController.GetCountry(1, 1);
            Assert.IsTrue(result.Result is BadRequestResult);
        }
        [TestMethod]
        public void DeleteCountry_DomainThrowDomainException_ReturnsNotFound()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, CountryId = 1, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.DeleteCountry(1)).Throws(new DomainException("test"));
            var result = ContinentMockController.DeleteCountry(1, 1);
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void DeleteCountry_DomainThrowException_ReturnsBadRequest()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, CountryId = 1, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.DeleteCountry(1)).Throws(new Exception("test"));
            var result = ContinentMockController.DeleteCountry(1, 1);
            Assert.IsTrue(result.Result is BadRequestResult);
        }
        [TestMethod]
        public void UpdateCountry_IdsDoNotMatch_ReturnsBadRequest()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.UpdateCountry(countryIn)).Returns(countryOut);
            var result = ContinentMockController.UpdateCountry(1, 1,countryIn);
            Assert.IsTrue(result.Result is BadRequestObjectResult);
        }
        [TestMethod]
        public void UpdateCountry_ReturnsCreatedAtActionResultWithCorrectValues()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.UpdateCountry(countryIn)).Returns(countryOut);
            var result = ContinentMockController.UpdateCountry(1, 2, countryIn);
            var temp = result.Result as CreatedAtActionResult;
            Assert.IsTrue(temp.Value.Equals(countryOut));
            Assert.IsTrue(result.Result is CreatedAtActionResult);
        }
        [TestMethod]
        public void UpdateCountry_DomainThrowsDomainException_ReturnsNotFound()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.UpdateCountry(countryIn)).Throws(new DomainException("test"));
            var result = ContinentMockController.UpdateCountry(1, 2, countryIn);
            var temp = result.Result as CreatedAtActionResult;
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }

        [TestMethod]
        public void UpdateCountry_DomainThrowsException_ReturnsNotFound()
        {
            CountryDTOIn countryIn = new CountryDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CountryDTOOut countryOut = new CountryDTOOut() { Continent = "1", Name = "testName2" };
            MockRepo.Setup(x => x.UpdateCountry(countryIn)).Throws(new Exception("test"));
            var result = ContinentMockController.UpdateCountry(1, 2, countryIn);
            var temp = result.Result as CreatedAtActionResult;
            Assert.IsTrue(result.Result is BadRequestResult);
        }
        #endregion


        #region cities
        [TestMethod]
        public void CreateCity_ReturnsCorrectObject_CreatedAtAction()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.AddCity(CityIn)).Returns(CityOut);
            var result = ContinentMockController.CreateCity(1,2, CityIn);
            var temp = result.Result as CreatedAtActionResult;
            Assert.IsTrue(temp.Value.Equals(CityOut));
            Assert.IsTrue(result.Result is CreatedAtActionResult);
        }
        [TestMethod]
        public void CreateCity_IdsDoNotMatch_ReturnsBadRequest()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.AddCity(CityIn)).Returns(CityOut);
            var result = ContinentMockController.CreateCity(1, 1, CityIn);
            Assert.IsTrue(result.Result is BadRequestObjectResult);
        }
        [TestMethod]
        public void CreateCity_DomainThrowsDomainException_ReturnsNotFound()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.AddCity(CityIn)).Throws(new DomainException("test"));
            var result = ContinentMockController.CreateCity(1, 2, CityIn);
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void CreateCity_DomainThrowsException_ReturnsBadRequest()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.AddCity(CityIn)).Throws(new Exception("test"));
            var result = ContinentMockController.CreateCity(1, 1, CityIn);
            Assert.IsTrue(result.Result is BadRequestObjectResult);
        }
        [TestMethod]
        public void GetCity_ReturnsOk_CorrectObject()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.GetCityForId(3)).Returns(CityOut);
            var result = ContinentMockController.GetCity(1, 2,3);
            var temp = result.Result as OkObjectResult;
            Assert.IsTrue(result.Result is OkObjectResult);
            Assert.IsTrue(temp.Value.Equals(CityOut));
        }
        [TestMethod]
        public void GetCity_DomainThrowsDomainExcpetion_ReturnsNotFound()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.GetCityForId(3)).Throws(new DomainException("test"));
            var result = ContinentMockController.GetCity(1, 2, 3);
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void GetCity_DomainThrowsExcpetion_ReturnsBadRequest()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.GetCityForId(3)).Throws(new Exception("test"));
            var result = ContinentMockController.GetCity(1, 2, 3);
            Assert.IsTrue(result.Result is BadRequestResult);
        }
        [TestMethod]
        public void DeleteCity_DomainThrowDomainException_ReturnsNotFound()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.DeleteCity(3)).Throws(new DomainException("test"));
            var result = ContinentMockController.DeleteCity(1, 2, 3);
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }
        [TestMethod]
        public void DeleteCity_DomainThrowException_ReturnsBadRequest()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.DeleteCity(3)).Throws(new Exception("test"));
            var result = ContinentMockController.DeleteCity(1, 2, 3);
            Assert.IsTrue(result.Result is BadRequestResult);
        }
        [TestMethod]
        public void UpdateCity_IdsDoNotMatch_ReturnsBadRequest()
        {

            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2,CityId=2, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.UpdateCity(CityIn)).Returns(CityOut);
            var result = ContinentMockController.UpdateCity(1, 2,3, CityIn);
            Assert.IsTrue(result.Result is BadRequestObjectResult);
        }
        [TestMethod]
        public void UpdateCity_ReturnsCreatedAtActionResultWithCorrectValues()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, CityId = 3, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.UpdateCity(CityIn)).Returns(CityOut);
            var result = ContinentMockController.UpdateCity(1, 2, 3, CityIn);
            var temp = result.Result as CreatedAtActionResult;
            Assert.IsTrue(temp.Value.Equals(CityOut));
            Assert.IsTrue(result.Result is CreatedAtActionResult);
        }
        [TestMethod]
        public void UpdateCity_DomainThrowsDomainException_ReturnsNotFound()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, CityId = 3, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.UpdateCity(CityIn)).Throws(new DomainException("test"));
            var result = ContinentMockController.UpdateCity(1, 2, 3, CityIn);
            var temp = result.Result as CreatedAtActionResult;
            Assert.IsTrue(result.Result is NotFoundObjectResult);
        }

        [TestMethod]
        public void UpdateCity_DomainThrowsException_ReturnsNotFound()
        {
            CityDTOIn CityIn = new CityDTOIn() { ContinentId = 1, CountryId = 2, CityId = 3, Name = "testName" };
            CityDTOOut CityOut = new CityDTOOut() { Country = "1", Name = "testName2" };
            MockRepo.Setup(x => x.UpdateCity(CityIn)).Throws(new Exception("test"));
            var result = ContinentMockController.UpdateCity(1, 2, 3, CityIn);
            var temp = result.Result as CreatedAtActionResult;
            Assert.IsTrue(result.Result is BadRequestResult);
        }



        #endregion
    }
}
