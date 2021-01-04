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
    public class CountryRepositoryTests
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
        public Continent GetSecondTestContinent(TestDataAccess data)
        {
            Continent continent = new Continent("SecondTestContinent");
            return data.Continents.AddContinent(continent);
        }
        public Country GetSecondTestCountry(TestDataAccess Data)
        {
            Continent continent = GetTestContinent(Data);
            Country country = new Country("testCountry2", 16000, 15000, continent);
            return Data.Countries.AddCountry(country);
        }
        //private City GetTestCity(TestDataAccess Data)
        //{
        //    Country country = GetTestCountry(Data);
        //    string name = "testname";
        //    int population = 35;
        //    bool capital = true;
        //    City city = new City(name, population, country, capital);
        //    return Data.Cities.AddCity(city);
        //}
        [TestMethod()]
        public void AddCountrytTest_GetCountryTest_ShouldWorkCorrectly()
        {
            var data = GetTestDataAccess();
            string name = "testname";
            int population = 35;
            int surfaceArea= 12000;
            Continent continent= GetTestContinent(data);
            Country country = new Country(name,population,surfaceArea,continent);

            data.Countries.AddCountry(country);

            var result = data.Countries.GetCountryForId(1);
            Assert.IsTrue(result.Name == name);
            Assert.IsTrue(result.Id == 1);
            Assert.IsTrue(result.Population == population);
            Assert.IsTrue(result.SurfaceArea == surfaceArea);
            Assert.IsTrue(result.Continent.Equals(continent));
        }
        [TestMethod()]
        public void AddCountryTest_ReturnsAddedCountry()
        {
            var data = GetTestDataAccess();
            string name = "testname";
            int population = 35;
            int surfaceArea = 12000;
            Continent continent = GetTestContinent(data);
            Country country = new Country(name, population, surfaceArea, continent);

            Country result1 = data.Countries.AddCountry(country);

            var result2 = data.Countries.GetCountryForId(1);
            Assert.IsTrue(result1.Equals(result2));
        }
        [TestMethod()]
        public void DeleteCountryTest_ShouldWorkCorrectly()
        {
            var data = GetTestDataAccess();
            Country country = GetTestCountry(data);

            data.Countries.DeleteCountry(1);
            var result = data.Countries.GetCountryForId(1);

            Assert.IsTrue(result == null);
        }

        [TestMethod()]
        public void UpdateCountryTest_ShouldUpdateCorrectly()
        {
            var data = GetTestDataAccess();
            Country addedCountry = GetTestCountry(data);
            string newName = "newTestName";
            Continent newContinent = GetSecondTestContinent(data);

            addedCountry.Name = newName;
            addedCountry.Population = 123;
            addedCountry.SurfaceArea = 456;
            addedCountry.Continent = newContinent;

            data.Countries.UpdateCountry(addedCountry);
            Country updatedCountry= data.Countries.GetCountryForId(1);
            Assert.IsTrue(updatedCountry.Id == 1);
            Assert.IsTrue(updatedCountry.Name == newName);
            Assert.IsTrue(updatedCountry.Population == 123);
            Assert.IsTrue(updatedCountry.SurfaceArea== 456);
            Assert.IsTrue(updatedCountry.Continent.Equals(newContinent));
        }

        [TestMethod()]
        public void GetCountryTest_ShouldGiveEntireDataStructure()
        {
            var data = GetTestDataAccess();
            Country addedCountry = GetTestCountry(data);
            City city = new City("testname",4,addedCountry,true);
            data.Cities.AddCity(city);

            addedCountry = data.Countries.GetCountryForId(1);
            Assert.IsTrue(addedCountry.Continent!= null);
            Assert.IsTrue(addedCountry.GetCities()[0] != null);
        }
    }
}