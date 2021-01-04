using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomeinLaag.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ManualTesting;
using DomeinLaag.Model;
using System.Linq;

namespace DomeinLaag.Interfaces.Tests
{
    [TestClass()]
    public class RiverRepositoryTests
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
        private River GetTestRiver(TestDataAccess data)
        {
            Country country = GetTestCountry(data);
            List<Country> countries = new List<Country> { country };
            string name = "testRiver";
            int length = 4567;
            River river = new River(name, length, countries);

            return data.Rivers.AddRiver(river);
        }
        [TestMethod()]
        public void AddRiverTest_GetRiverTest_ShouldWorkCorrectly()
        {
            var data = GetTestDataAccess();
            Country country = GetTestCountry(data);
            List<Country> countries = new List<Country> { country };
            string name = "testRiver";
            int length = 4567;
            River river = new River(name, length, countries);

            data.Rivers.AddRiver(river);

            var result = data.Rivers.GetRiverForId(1);
            Assert.IsTrue(result.Name == name);
            Assert.IsTrue(result.Id == 1);
            Assert.IsTrue(result.LengthInKm == length);
            Assert.IsTrue(result.GetCountries().SequenceEqual(countries));
        }
        [TestMethod()]
        public void AddRiverTest_ReturnsAddedRiver()
        {
            var data = GetTestDataAccess();
            Country country = GetTestCountry(data);
            List<Country> countries = new List<Country> { country };
            string name = "testRiver";
            int length = 4567;
            River river = new River(name, length, countries);

            var result2 = data.Rivers.AddRiver(river);

            var result1 = data.Rivers.GetRiverForId(1);
            Assert.IsTrue(result1.Equals(result2));
        }
        [TestMethod()]
        public void DeleteRiverTest_ShouldWorkCorrectly()
        {
            var data = GetTestDataAccess();
            River river = GetTestRiver(data);

            data.Rivers.DeleteRiver(1);
            var result = data.Rivers.GetRiverForId(1);

            Assert.IsTrue(result == null);
        }

        [TestMethod()]
        public void UpdateRiverTest_ShouldUpdateCorrectly()
        {
            var data = GetTestDataAccess();
            River addedRiver = GetTestRiver(data);
            string newName = "newTestName";
            int newLength = 12;
            Country newCountry = GetSecondTestCountry(data);
            List<Country> newCountries = new List<Country> { newCountry };

            addedRiver.Name = newName;
            addedRiver.LengthInKm = newLength;
            addedRiver.SetCountries(newCountries);

            data.Rivers.UpdateRiver(addedRiver);
            River updatedRiver = data.Rivers.GetRiverForId(1);
            Assert.IsTrue(updatedRiver.Id == 1);
            Assert.IsTrue(updatedRiver.Name == newName);
            Assert.IsTrue(updatedRiver.LengthInKm== newLength);
            Assert.IsTrue(updatedRiver.GetCountries().SequenceEqual(newCountries));
        }

        [TestMethod()]
        public void GetRiverTest_ShouldGiveEntireDataStructure()
        {
            var data = GetTestDataAccess();
            River addedRiver = GetTestRiver(data);

            Assert.IsTrue(addedRiver.GetCountries().Count != 0);
            Assert.IsTrue(addedRiver.GetCountries()[0].Continent != null);
        }
    }
}