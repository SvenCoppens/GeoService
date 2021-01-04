using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoService.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using DomeinLaag;
using Microsoft.Extensions.Configuration;
using DomeinLaag.Model;
using System.IO;
using System.Linq;

namespace GeoService.Model.Tests
{
    [TestClass()]
    public class ApiWorkTests
    {
        private ApiWork Api;
        private Mock<ICountryManager> MockManager;
        private Mock<IConfiguration> MockConfig;

        private Continent GetContinentWithCountries(int amount)
        {
            Continent continent = new Continent("testContinent");
            continent.Id = 1;
            for(int i = 0; i < amount; i++)
            {
                Country c = new Country($"testCountry{i}", (i+1)* 100, (i+1)* 200, continent);
                c.Id = i + 1;
            }
            return continent;
        }
        private Country GetCountryWithCities(int amount)
        {
            Continent continent = GetContinentWithCountries(2);
            Country country = continent.GetCountries()[0];

            for (int i = 0; i < amount; i++)
            {
                City c = new City($"testCity{i}", (i + 1) * 10, country, i%2==0?true:false);
                c.Id = i + 1;
            }
            return country;
        }
        private City GetCity()
        {
            Country country = GetCountryWithCities(3);
            return country.GetCapitals()[0];
        }
        private River GetRiverWithCities()
        {
            Country country = GetCountryWithCities(1);
            List<Country> countries = new List<Country>();
            foreach(Country c in country.Continent.GetCountries())
            {
                countries.Add(c);
            }
            River river = new River("testRiver", 15000, countries);
            river.Id = 1;
            return river;
        }
        public ApiWorkTests()
        {
            
            MockManager = new Mock<ICountryManager>();
            //var MockConfig = new Mock<IConfiguration>();
            var MockConfig = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "Properties", "launchSettings.json")).Build();
            Api = new ApiWork(MockManager.Object,MockConfig);
            //MockConfig.Setup(m => m.GetValue<string>("iisSettings:IISExpress:applicationUrl")).Returns("HostString://");
        }
        [TestMethod()]
        public void GetContinentForIdTest()
        {
            Continent continent = GetContinentWithCountries(2);
            MockManager.Setup(m => m.GetContinentForId(1)).Returns(continent);
            var result = Api.GetContinentForId(1);
            string expectedId = @"http://localhost:50012/api/continent/1";
            string expectedCountryString = @"http://localhost:50012/api/continent/1/Country/1";
            string expectedSecondCountryString = @"http://localhost:50012/api/continent/1/Country/2";
            Assert.IsTrue(result is ContinentDTOOut);
            Assert.IsTrue(result.ContinentId == expectedId);
            Assert.IsTrue(result.Name == continent.Name);
            Assert.IsTrue(result.Population == continent.GetPopulation());
            Assert.IsTrue(result.Countries[0] == expectedCountryString);
            Assert.IsTrue(result.Countries[1] == expectedSecondCountryString);
        }
        [TestMethod]
        public void GetCountryForIdTest()
        {
            Country country  = GetCountryWithCities(2);
            MockManager.Setup(m => m.GetCountryForId(1)).Returns(country);
            var result = Api.GetCountryForId(1);
            string continentId = @"http://localhost:50012/api/continent/1";
            string countryId = @"http://localhost:50012/api/continent/1/Country/1";
            string cityId1 = @"http://localhost:50012/api/continent/1/Country/1/City/1";
            string cityId2 = @"http://localhost:50012/api/continent/1/Country/1/City/2";
            Assert.IsTrue(result is CountryDTOOut);
            Assert.IsTrue(result.Continent == continentId);
            Assert.IsTrue(result.Name == country.Name);
            Assert.IsTrue(result.Population == country.Population);
            Assert.IsTrue(result.Cities[0] == cityId1);
            Assert.IsTrue(result.Cities[1] == cityId2);
            Assert.IsTrue(result.Capitals[0] == cityId1);
        }

        [TestMethod]
        public void GetRiverForIdTest()
        {
            River river = GetRiverWithCities();
            MockManager.Setup(m => m.GetRiverForId(1)).Returns(river);
            var result = Api.GetRiverForId(1);
            string riverId = @"http://localhost:50012/api/river/1";
            string countryId1= @"http://localhost:50012/api/continent/1/Country/1";
            string countryId2 = @"http://localhost:50012/api/continent/1/Country/2";

            Assert.IsTrue(result is RiverDTOOut);
            Assert.IsTrue(result.Name == river.Name);
            Assert.IsTrue(result.Length == river.LengthInKm);
            Assert.IsTrue(result.Countries[0] == countryId1);
            Assert.IsTrue(result.Countries[1] == countryId2);
        }
        [TestMethod]
        public void GetCityForIdTest()
        {
            City city = GetCity();
            MockManager.Setup(m => m.GetCityForId(1)).Returns(city);
            var result = Api.GetCityForId(1);
            string cityId = @"http://localhost:50012/api/continent/1/Country/1/City/1";
            string countryId = @"http://localhost:50012/api/continent/1/Country/1";
            Assert.IsTrue(result.CityId == cityId);
            Assert.IsTrue(result.Country == countryId);
            Assert.IsTrue(result.Name == city.Name);
            Assert.IsTrue(result.Population == city.Population);
        }

        #region AddTests
        [TestMethod()]
        public void AddContinentTest()
        {
            ContinentDTOIn DTOin = new ContinentDTOIn() {Name="TestContinent"};
            Continent continent = new Continent(DTOin.Name);
            continent.Id = 1;
            MockManager.Setup(m => m.AddContinent(DTOin.Name)).Returns(continent);
            var result = Api.AddContinent(DTOin);
            string expectedId = @"http://localhost:50012/api/continent/1";
            Assert.IsTrue(result is ContinentDTOOut);
            Assert.IsTrue(result.ContinentId == expectedId);
            Assert.IsTrue(result.Name == DTOin.Name);
        }
        [TestMethod]
        public void AddCountryTest()
        {
            Continent continent = new Continent("TestString");
            CountryDTOIn DTOin = new CountryDTOIn() { Name = "TestContinent",ContinentId=1,Population=12,SurfaceArea=15};
            Country country = new Country(DTOin.Name,DTOin.Population,DTOin.SurfaceArea,continent);
            continent.Id = 1;
            country.Id = 1;
            MockManager.Setup(m => m.GetContinentForId(continent.Id)).Returns(continent);
            MockManager.Setup(m => m.AddCountry(DTOin.Name,DTOin.Population,DTOin.SurfaceArea,continent)).Returns(country);
            var result = Api.AddCountry(DTOin);
            string continentId = @"http://localhost:50012/api/continent/1";
            string countryId = @"http://localhost:50012/api/continent/1/Country/1";
            Assert.IsTrue(result is CountryDTOOut);
            Assert.IsTrue(result.Continent == continentId);
            Assert.IsTrue(result.Name == country.Name);
            Assert.IsTrue(result.CountryId == countryId);
            Assert.IsTrue(result.Population == country.Population);
            Assert.IsTrue(result.Surface == country.SurfaceArea);
        }

        [TestMethod]
        public void AddRiverTest()
        {
            River river = GetRiverWithCities();
            List<Country> RiverCountries = new List<Country>() { river.GetCountries()[0],river.GetCountries()[1] };
            RiverDTOIn DTOin = new RiverDTOIn() { Name = river.Name, Length = river.LengthInKm, CountryIds = new int[] { 1, 2 } };
            MockManager.Setup(m => m.GetCountryForId(1)).Returns(RiverCountries[0]);
            MockManager.Setup(m => m.GetCountryForId(2)).Returns(RiverCountries[1]);
            MockManager.Setup(m => m.AddRiver(river.Name,river.LengthInKm,RiverCountries)).Returns(river);
            var result = Api.AddRiver(DTOin);
            string riverId = @"http://localhost:50012/api/river/1";
            string countryId1 = @"http://localhost:50012/api/continent/1/Country/1";
            string countryId2 = @"http://localhost:50012/api/continent/1/Country/2";

            Assert.IsTrue(result is RiverDTOOut);
            Assert.IsTrue(result.Name == river.Name);
            Assert.IsTrue(result.Length == river.LengthInKm);
            Assert.IsTrue(result.Countries[0] == countryId1);
            Assert.IsTrue(result.Countries[1] == countryId2);
        }
        [TestMethod]
        public void AddCityTest()
        {
            City city = GetCity();
            CityDTOIn DTOin = new CityDTOIn()
            {
                Name = city.Name,
                Population = city.Population,
                CityId = city.Id,
                ContinentId = city.Country.Continent.Id,
                CountryId = city.Country.Id,
                Capital = city.Capital
            };
            MockManager.Setup(m => m.GetCountryForId(city.Country.Id)).Returns(city.Country);
            MockManager.Setup(m => m.AddCity(DTOin.Name,DTOin.Population,city.Country,city.Capital)).Returns(city);
            var result = Api.AddCity(DTOin);
            string cityId = @"http://localhost:50012/api/continent/1/Country/1/City/1";
            string countryId = @"http://localhost:50012/api/continent/1/Country/1";
            Assert.IsTrue(result.CityId == cityId);
            Assert.IsTrue(result.Country == countryId);
            Assert.IsTrue(result.Name == city.Name);
            Assert.IsTrue(result.Population == city.Population);
            Assert.IsTrue(result.Population == city.Population);
        }
        #endregion

        #region delete
        [TestMethod]
        public void DeleteContinentTest()
        {
            Api.DeleteContinent(1);
            MockManager.Verify(m => m.DeleteContinent(1),Times.Once);
        }
        [TestMethod]
        public void DeleteCityTest()
        {
            Api.DeleteCity(1);
            MockManager.Verify(m => m.DeleteCity(1), Times.Once);
        }
        [TestMethod]
        public void DeleteCountry()
        {
            Api.DeleteCountry(1);
            MockManager.Verify(m => m.DeleteCountry(1), Times.Once);
        }
        [TestMethod]
        public void DeleteRiver()
        {
            Api.DeleteRiver(1);
            MockManager.Verify(m => m.DeleteRiver(1), Times.Once);
        }
        #endregion

        #region Delete tests
        [TestMethod]
        public void UpdateContinentTest()
        {
            Continent continent = GetContinentWithCountries(2);
            ContinentDTOIn DTOin = new ContinentDTOIn() {Name=continent.Name,ContinentId=continent.Id };
            MockManager.Setup(m => m.GetContinentForId(continent.Id)).Returns(continent);
            MockManager.Setup(m => m.UpdateContinent(continent)).Returns(continent);
            var result = Api.UpdateContinent(DTOin);
            string expectedId = @"http://localhost:50012/api/continent/1";
            string expectedCountryString = @"http://localhost:50012/api/continent/1/Country/1";
            string expectedSecondCountryString = @"http://localhost:50012/api/continent/1/Country/2";
            Assert.IsTrue(result is ContinentDTOOut);
            Assert.IsTrue(result.ContinentId == expectedId);
            Assert.IsTrue(result.Name == continent.Name);
            Assert.IsTrue(result.Population == continent.GetPopulation());
            Assert.IsTrue(result.Countries[0] == expectedCountryString);
            Assert.IsTrue(result.Countries[1] == expectedSecondCountryString);
        }

        [TestMethod]
        public void UpdateCountryTest()
        {
            Country country = GetCountryWithCities(2);
            CountryDTOIn DTOin = new CountryDTOIn()
            {
                Name = country.Name,
                ContinentId = country.Continent.Id,
                CountryId = country.Id,
                Population = country.Population,
                SurfaceArea = country.SurfaceArea
            };

            MockManager.Setup(m => m.UpdateCountry(country)).Returns(country);
            MockManager.Setup(m => m.GetCountryForId(country.Id)).Returns(country);
            MockManager.Setup(m => m.GetContinentForId(country.Continent.Id)).Returns(country.Continent);
            var result = Api.UpdateCountry(DTOin);
            string continentId = @"http://localhost:50012/api/continent/1";
            string countryId = @"http://localhost:50012/api/continent/1/Country/1";
            string cityId1 = @"http://localhost:50012/api/continent/1/Country/1/City/1";
            string cityId2 = @"http://localhost:50012/api/continent/1/Country/1/City/2";
            Assert.IsTrue(result is CountryDTOOut);
            Assert.IsTrue(result.Continent == continentId);
            Assert.IsTrue(result.Name == country.Name);
            Assert.IsTrue(result.Population == country.Population);
            Assert.IsTrue(result.Cities[0] == cityId1);
            Assert.IsTrue(result.Cities[1] == cityId2);
            Assert.IsTrue(result.Capitals[0] == cityId1);
        }

        [TestMethod]
        public void UpdateRiverTest()
        {
            River river = GetRiverWithCities();
            int[] countryIds = new int[] { river.GetCountries()[0].Id, river.GetCountries()[1].Id };
            RiverDTOIn DTOin = new RiverDTOIn()
            {
                Name = river.Name,
                Length = river.LengthInKm,
                RiverId = river.Id,
                CountryIds = countryIds
            };
            MockManager.Setup(m => m.GetCountryForId(countryIds[0])).Returns(river.GetCountries()[0]);
            MockManager.Setup(m => m.GetCountryForId(countryIds[1])).Returns(river.GetCountries()[1]);
            MockManager.Setup(m => m.GetRiverForId(river.Id)).Returns(river);
            MockManager.Setup(m => m.UpdateRiver(river)).Returns(river);
            var result = Api.UpdateRiver(DTOin);
            string riverId = @"http://localhost:50012/api/river/1";
            string countryId1 = @"http://localhost:50012/api/continent/1/Country/1";
            string countryId2 = @"http://localhost:50012/api/continent/1/Country/2";

            Assert.IsTrue(result is RiverDTOOut);
            Assert.IsTrue(result.Name == river.Name);
            Assert.IsTrue(result.Length == river.LengthInKm);
            Assert.IsTrue(result.Countries[0] == countryId1);
            Assert.IsTrue(result.Countries[1] == countryId2);
        }
        [TestMethod]
        public void UpdateCityTest()
        {
            City city = GetCity();
            CityDTOIn DTOin = new CityDTOIn()
            {
                Name = city.Name,
                Capital = city.Capital,
                CityId = city.Id,
                ContinentId = city.Country.Continent.Id,
                CountryId = city.Country.Id,
                Population = city.Population
            };

            MockManager.Setup(m => m.UpdateCity(city)).Returns(city);
            MockManager.Setup(m => m.GetCityForId(city.Id)).Returns(city);
            MockManager.Setup(m => m.GetCountryForId(city.Country.Id)).Returns(city.Country);
            var result = Api.UpdateCity(DTOin);
            string cityId = @"http://localhost:50012/api/continent/1/Country/1/City/1";
            string countryId = @"http://localhost:50012/api/continent/1/Country/1";
            Assert.IsTrue(result.CityId == cityId);
            Assert.IsTrue(result.Country == countryId);
            Assert.IsTrue(result.Name == city.Name);
            Assert.IsTrue(result.Population == city.Population);
        }
        #endregion
    }
}