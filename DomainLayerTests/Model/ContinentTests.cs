using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Text;
using DomeinLaag.Exceptions;

namespace DomeinLaag.Model.Tests
{
    [TestClass()]
    public class ContinentTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ContinentException))]
        public void CreateContinentTest_EmptyStringAsName_ShouldThrowContinentException()
        {
            Continent c = new Continent("");
        }

        [TestMethod()]
        [ExpectedException(typeof(ContinentException))]
        public void ContinentTest_SetNameToEmptyString_ShouldThrowContinentException()
        {
            Continent c = new Continent("testContinent");
            c.Name = "";
        }

        [TestMethod()]
        [ExpectedException(typeof(ContinentException))]
        public void CreateContinentTest_NullAsName_ShouldThrowContinentException()
        {
            Continent c = new Continent(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ContinentException))]
        public void ContinentTest_SetNameToNull_ShouldThrowContinentException()
        {
            Continent c = new Continent("testContinent");
            c.Name = null;
        }


        [TestMethod()]
        public void ContinentTest_SetAndGetCountriesWorksCorrectly()
        {
            Continent c = new Continent("testContinent");
            List<Country> countries = new List<Country>();
            Country country1 = new Country("testCountry1",15,10,c);
            Country country2 = new Country("testCountry2", 25, 20, c);
            c.SetCountries(countries);

            Assert.IsTrue(c.GetCountries().Count == 0, "The amount of countries in the continent was not correct");
            countries.Add(country1);
            countries.Add(country2);
            c.SetCountries(countries);

            Assert.IsTrue(c.GetCountries().Count == 2, "The amount of countries was not added correctly.");
        }

        [TestMethod()]
        public void ContinentTest_CreatingACountryAddsItToListOfCountriesOfTheContinent()
        {
            Continent c = new Continent("testContinent");
            Country country1 = new Country("testCountry1", 15, 10, c);
            Country country2 = new Country("testCountry2", 25, 20, c);
            Assert.IsTrue(c.GetCountries().Count == 2, "The amount of countries in the continent was not correct");
        }
        [TestMethod()]
        public void ContinentTest_PopulationCorrectlyShowTheSumOfTheCountries()
        {
            Continent c = new Continent("testContinent");
            Assert.IsTrue(c.GetPopulation() == 0, "The Population dit not start at 0");
            int population1 = 15;
            Country country1 = new Country("testCountry1",population1, 10, c);
            Assert.IsTrue(c.GetPopulation() == population1, "The population did not correctly get updated");
            int population2 = 25;
                Country country2 = new Country("testCountry2", population2, 20, c);
            Assert.IsTrue(c.GetPopulation() == population1+population2, "The population did not correctly get updated");
            c.RemoveCountryFromContinent(country1);
            Assert.IsTrue(c.GetPopulation() == population2);
        }
    }
}