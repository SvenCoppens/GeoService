using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomeinLaag.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ManualTesting;
using DomeinLaag.Model;

namespace DomeinLaag.Interfaces.Tests
{
    [TestClass()]
    public class CityRepositoryTests
    {
        private TestDataAccess GetTestDataAccess()
        {
            return new TestDataAccess();
        }
        private Continent GetTestContinent(TestDataAccess Data)
        {
            Continent continent = new Continent("TestContinent");
            return Data.Continents.AddContinent(continent);
        }
        public Country GetTestCountry(TestDataAccess Data)
        {
            Continent continent = GetTestContinent(Data);
            Country country = new Country("testCountry1", 15000, 14000, continent);
                return Data.Countries.AddCountry(country);
        }
        public Country GetSecondTestCountry(TestDataAccess Data)
        {
            Continent continent = GetTestContinent(Data);
            Country country = new Country("testCountry2", 16000, 15000, continent);
            return Data.Countries.AddCountry(country);
        }
        private City GetTestCity(TestDataAccess Data)
        {
            Country country = GetTestCountry(Data);
            string name = "testname";
            int population = 35;
            bool capital = true;
            City city = new City(name, population, country, capital);
            return Data.Cities.AddCity(city);
        }
        [TestMethod()]
        public void AddCitytTest_GetCityTest_ShouldWorkCorrectly()
        {
            var data = GetTestDataAccess();
            string name = "testname";
            int population = 35;
            bool capital = true;
            Country country = GetTestCountry(data);
            City city = new City(name,population,country,capital);

            data.Cities.AddCity(city);

            var result = data.Cities.GetCityForId(1);
            Assert.IsTrue(result.Name == name);
            Assert.IsTrue(result.Id == 1);
            Assert.IsTrue(result.Population == population);
            Assert.IsTrue(result.Capital == capital);
            Assert.IsTrue(result.Country.Equals(country));
        }
        [TestMethod()]
        public void AddCityTest_ReturnsAddedCity()
        {
            var data = GetTestDataAccess();
            string name = "testname";
            int population = 35;
            bool capital = true;
            Country country = GetTestCountry(data);
            City city = new City(name, population, country, capital);

            var result1 = data.Cities.AddCity(city);
            var result2 = data.Cities.GetCityForId(1);
            Assert.IsTrue(result1.Equals(result2));
        }
        [TestMethod()]
        public void DeleteCityTest_ShouldWorkCorrectly()
        {
            var data = GetTestDataAccess();
            City city = GetTestCity(data);

            data.Cities.DeleteCity(1);
            var result = data.Cities.GetCityForId(1);

            Assert.IsTrue(result == null);
        }

        [TestMethod()]
        public void UpdateCityTest_ShouldUpdateCorrectly()
        {
            var data = GetTestDataAccess();
            City addedCity = GetTestCity(data);
            string newName = "newTestName";
            Country newCountry = GetSecondTestCountry(data);

            addedCity.Name = newName;
            addedCity.Capital = false;
            addedCity.Population = 123;
            addedCity.Country = newCountry;

            data.Cities.UpdateCity(addedCity);
            City updatedCity = data.Cities.GetCityForId(1);
            Assert.IsTrue(updatedCity.Id == 1);
            Assert.IsTrue(updatedCity.Name == newName);
            Assert.IsTrue(updatedCity.Capital == false);
            Assert.IsTrue(updatedCity.Population == 123);
            Assert.IsTrue(updatedCity.Country.Equals(newCountry));
        }

        [TestMethod()]
        public void GetCityTest_ShouldGiveEntireDataStructure()
        {
            var data = GetTestDataAccess();
            City addedCity = GetTestCity(data);

            Assert.IsTrue(addedCity.Country != null);
            Assert.IsTrue(addedCity.Country.Continent != null);
        }

    }
}