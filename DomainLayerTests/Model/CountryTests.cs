using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Text;
using DomeinLaag.Exceptions;

namespace DomeinLaag.Model.Tests
{
    [TestClass()]
    public class CountryTests
    {
        private Country GetStandardCountry()
        {
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry", 40000, 10, continent);
            return country;
        }
        private Country GetSecondCountry()
        {
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry2", 40000, 10, continent);
            return country;
        }

        [TestMethod()]
        public void CountryCreationTest_ShouldSetDataCorrectly()
        {
            Continent cont = new Continent("testContinent");
            string testName = "testName";
            int population = 2500;
            int surface = 35;
            Country ctr = new Country(testName, population, surface, cont);

            Assert.IsTrue(ctr.Name == testName, "The name was not set correctly.");
            Assert.IsTrue(ctr.Population == population, "The population was not set correctly.");
            Assert.IsTrue(ctr.SurfaceArea == surface, "The surface area was not set correctly.");
            Assert.IsTrue(ctr.Continent.Equals(cont), "The continent was not set correctly.");
            Assert.IsTrue(ctr.GetCities().Count == 0, "The cities were not set correctly.");
            Assert.IsTrue(ctr.GetCapitals().Count == 0, "The capitals were not set correctly.");
            Assert.IsTrue(ctr.GetRivers().Count == 0, "The rivers were not set correctly.");
        }
        [TestMethod()]
        [ExpectedException(typeof(CountryException))]
        public void CountryCreationTest_NullAsContinent_ShouldThrowCountryException()
        {
            string testName = "testName";
            int population = 2500;
            int surface = 35;
            Country ctr = new Country(testName, population, surface, null);
        }

        [TestMethod()]
        [ExpectedException(typeof(CountryException))]
        public void CountryCreationTest_NullAsName_ShouldThrowCountryException()
        {
            Continent cont = new Continent("testContinent");
            int population = 2500;
            int surface = 35;
            Country ctr = new Country(null, population, surface, cont);
        }
        [TestMethod()]
        [ExpectedException(typeof(CountryException))]
        public void CountryCreationTest_EmptyStringAsName_ShouldThrowCountryException()
        {
            Continent cont = new Continent("testContinent");
            string testName = "";
            int population = 2500;
            int surface = 35;
            Country ctr = new Country(testName, population, surface, cont);
        }

        [TestMethod()]
        [ExpectedException(typeof(CountryException))]
        public void CountryCreationTest_PopulationAs0_ShouldThrowCountryException()
        {
            Continent cont = new Continent("testContinent");
            string testName = "testName";
            int population = 0;
            int surface = 35;
            Country ctr = new Country(testName, population, surface, cont);
        }
        [TestMethod()]
        [ExpectedException(typeof(CountryException))]
        public void CountryCreationTest_PopulationAsNegativeNumber_ShouldThrowCountryException()
        {
            Continent cont = new Continent("testContinent");
            string testName = "testName";
            int population = -1;
            int surface = 35;
            Country ctr = new Country(testName, population, surface, cont);
        }
        [TestMethod()]
        [ExpectedException(typeof(CountryException))]
        public void CountryCreationTest_SurfaceAs0_ShouldThrowCountryException()
        {
            Continent cont = new Continent("testContinent");
            string testName = "testName";
            int population = 10000;
            int surface = 0;
            Country ctr = new Country(testName, population, surface, cont);
        }
        [TestMethod()]
        [ExpectedException(typeof(CountryException))]
        public void CountryCreationTest_SurfaceAsNegativeNumber_ShouldThrowCountryException()
        {
            Continent cont = new Continent("testContinent");
            string testName = "testName";
            int population = 10000;
            int surface = -1;
            Country ctr = new Country(testName, population, surface, cont);
        }

        [TestMethod()]
        public void CountryTest_TestAddingCapitals_ShouldAddToBothCapitalsAndCities()
        {
            Country country = GetStandardCountry();
            City city = new City("testCity1", 10, country, true);

            Assert.IsTrue(country.GetCapitals().Count == 1, "The amount of capitals was not correct");
            Assert.IsTrue(country.GetCapitals() is IReadOnlyCollection<City>, "The collection was not read only.");
            Assert.IsTrue(country.GetCities().Count == 1, "The amount of capitals was not correct");
            Assert.IsTrue(country.GetCities() is IReadOnlyCollection<City>, "The collection was not read only.");
        }
        [TestMethod()]
        public void CountryTest_TestAddingCity_ShouldAddToCitiesAndNotCapitals()
        {
            Country country = GetStandardCountry();
            City city = new City("testCity1", 10, country, false);

            Assert.IsTrue(country.GetCapitals().Count == 0, "The amount of capitals was not correct");
            Assert.IsTrue(country.GetCapitals() is IReadOnlyCollection<City>, "The collection was not read only.");
            Assert.IsTrue(country.GetCities().Count == 1, "The amount of capitals was not correct");
            Assert.IsTrue(country.GetCities() is IReadOnlyCollection<City>, "The collection was not read only.");
        }

        [TestMethod()]
        [ExpectedException(typeof(CountryException))]
        public void CountryTest_AddCapitalTest_WhenCityIsNotPartOfThatCountry()
        {
            Country country1 =  GetStandardCountry();
            Country country2 = GetSecondCountry();

            City testCity = new City("testName", 15, country2, true);
            country1.AddCapital(testCity);
        }
        [TestMethod()]
        [ExpectedException(typeof(CountryException))]
        public void CountryTest_AddCityTest_WhenCityIsNotPartOfThatCountry()
        {
            Country country1 = GetStandardCountry();
            Country country2 = GetSecondCountry();

            City testCity = new City("testName", 15, country2, true);
            country1.AddCity(testCity);
        }

        [TestMethod()]
        public void CountryTest_RemoveAsCapital_CityStaysInCountry()
        {
            Country country1 = GetStandardCountry();

            City testCity = new City("testName", 15, country1, true);
            Assert.IsTrue(country1.GetCapitals().Count == 1, "The capitals in the second country was not correctly added");
            Assert.IsTrue(country1.GetCities().Count == 1, "The Cities in the second country were not correctly added.");
            country1.RemoveAsCapital(testCity);

            Assert.IsTrue(testCity.Capital == false, "The city was no longer a capital");
            Assert.IsTrue(testCity.Country.Equals(country1), "The country was not correctly updated");
            Assert.IsTrue(country1.GetCapitals().Count == 0, "The capitals in the second country was not correctly removed");
            Assert.IsTrue(country1.GetCities().Count == 1, "The Cities in the second country were not kept in the cities collection.");
        }

        [TestMethod()]
        [ExpectedException(typeof(CountryException))]
        public void CountryTest_PopulationMustAlwaysBeBiggerThanTheSumOfTheCities__ShouldThrowCountryException()
        {
            Country country1 = GetStandardCountry();
            City testCity = new City("testName", country1.Population+1, country1, true);

        }
        [TestMethod()]
        [ExpectedException(typeof(CountryException))]
        public void CountryTest_PopulationMustAlwaysBeBiggerThanTheSumOfTheCities_ShouldThrowCountryException()
        {
            Country country1 = GetStandardCountry();
            City testCity = new City("testName", country1.Population -1, country1, true);

            City testCity2 = new City("testName2",2, country1, true);
        }
        [TestMethod()]
        public void CountryTest_PopulationMustAlwaysBeBiggerThanTheSumOfTheCities_EqualToThePopulation_ShouldNotThrowCountryException()
        {
            Country country1 = GetStandardCountry();
            City testCity = new City("testName", country1.Population - 1, country1, true);

            City testCity2 = new City("testName2", 1, country1, true);

            Assert.IsTrue(country1.Population == testCity.Population + testCity2.Population, "The population did not match up.");
        }


        [TestMethod()]
        [ExpectedException(typeof(ContinentException))]
        public void CountryTest_NameMustBeUniqueWithinContinent()
        {
            Country country1 = GetStandardCountry();
            Country country2 = new Country(country1.Name, 456789, 45678, country1.Continent);

        }
    }
}