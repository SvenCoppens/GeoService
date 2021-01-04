using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DomeinLaag.Exceptions;

namespace DomeinLaag.Model.Tests
{
    [TestClass()]
    public class RiverTests
    {
        private Country GetFirstCountry()
        {
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry", 40000, 10, continent);
            return country;
        }
        private Country GetSecondCountry()
        {
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry2", 4000, 100, continent);
            return country;
        }
        [TestMethod()]
        public void CreateRiverTest_ShouldSetDataCorrectly()
        {
            string name = "TestRiver";
            int length = 25;
            List<Country> countries = new List<Country>() { GetFirstCountry() };

            River river = new River(name, length, countries);

            Assert.IsTrue(river.Name == name, "The name of the river did not match.");
            Assert.IsTrue(river.LengthInKm == length, "The length of the river did not match.");
            Assert.IsTrue(river.GetCountries().SequenceEqual(countries), "The countries of the river did not match.");
        }
        [TestMethod()]
        [ExpectedException(typeof(RiverException))]
        public void CreateRiverTest_CreateRiverWithoutCountries()
        {
            string name = "TestRiver";
            int length = 25;
            List<Country> countries = new List<Country>() { };

            River river = new River(name, length, countries);
        }
        [TestMethod()]
        [ExpectedException(typeof(RiverException))]
        public void CreateRiverTest_CreateRiverWithDoublesInListOfCountries()
        {
            string name = "TestRiver";
            int length = 25;
            List<Country> countries = new List<Country>() { GetFirstCountry(), GetFirstCountry() };

            River river = new River(name, length, countries);
        }
        [TestMethod()]
        public void RiverTest_ChangeCountriesOfRiver_ListOfRiverInCountryNoLongerContainsRiver()
        {
            string name1 = "TestRiver1";
            int length1 = 25;
            List<Country> countries1 = new List<Country>() { GetFirstCountry() };

            string name2 = "TestRiver2";
            int length2 = 225;
            List<Country> countries2 = new List<Country>() { GetSecondCountry() };

            River river1 = new River(name1, length1, countries1);
            River river2 = new River(name2, length2, countries2);
            river1.SetCountries(countries2);

            Assert.IsTrue(countries1[0].GetRivers().Count == 0, "The rivers were not correctly removed from the country.");
        }
        [TestMethod()]
        [ExpectedException(typeof(RiverException))]
        public void CreateRiverTest_WithNullAsName_ShouldThrowRiverException()
        {
            string name = null;
            int length = 25;
            List<Country> countries = new List<Country>() { GetFirstCountry(), GetFirstCountry() };

            River river = new River(name, length, countries);
        }
        [TestMethod()]
        [ExpectedException(typeof(RiverException))]
        public void CreateRiverTest_WithEmptyStringAsName_ShouldThrowRiverException()
        {
            string name = "";
            int length = 25;
            List<Country> countries = new List<Country>() { GetFirstCountry(), GetFirstCountry() };

            River river = new River(name, length, countries);
        }
        [TestMethod()]
        [ExpectedException(typeof(RiverException))]
        public void CreateRiverTest_With0AsLength_ShouldThrowRiverException()
        {
            string name = "TestName";
            int length = 0;
            List<Country> countries = new List<Country>() { GetFirstCountry(), GetFirstCountry() };

            River river = new River(name, length, countries);
        }
        [TestMethod()]
        [ExpectedException(typeof(RiverException))]
        public void CreateRiverTest_WithNegativeNumberAsLength_ShouldThrowRiverException()
        {
            string name = "TestName";
            int length = -1;
            List<Country> countries = new List<Country>() { GetFirstCountry(), GetFirstCountry() };

            River river = new River(name, length, countries);
        }
    }
}