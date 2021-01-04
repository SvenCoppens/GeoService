using DomeinLaag.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomeinLaag.Model.Tests
{
    [TestClass]
    public class CityTests
    {
        private City GetStandardCity()
        {
            string cityName = "testCity";
            int population = 35000;
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry", 40000, 10, continent);
            City city = new City(cityName, population, country, true);

            return city;
        }
        private Country GetStandardCountry()
        {
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry", 40000, 10, continent);
            return country;
        }
        [TestMethod()]
        public void CreateCityTest_IsACapital_DataShouldBeCorrect()
        {
            string cityName = "testCity";
            int population = 35000;
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry", 40000, 10, continent);
            City city = new City(cityName, population, country, true);

            Assert.IsTrue(city.Name == cityName, "The name of the city was not correct");
            Assert.IsTrue(city.Population == population, "The population of the city was not correct");
            Assert.IsTrue(city.Country.Equals(country), "The country of the city was not correct");
            Assert.IsTrue(country.GetCities().Contains(city), "The country did not contain the city");
            Assert.IsTrue(country.GetCapitals().Contains(city), "The country's capitals did not contain the city");
            Assert.IsTrue(city.Capital == true, "The city did not show it was a capital");
        }
        [TestMethod()]
        public void CreateCityTest_IsNotACapital_DataShouldBeCorrect()
        {
            string cityName = "testCity";
            int population = 35000;
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry", 40000, 10, continent);
            City city = new City(cityName, population, country, false);

            Assert.IsTrue(city.Name == cityName, "The name of the city was not correct");
            Assert.IsTrue(city.Population == population, "The population of the city was not correct");
            Assert.IsTrue(city.Country.Equals(country), "The country of the city was not correct");
            Assert.IsTrue(country.GetCities().Contains(city), "The country did not contain the city");
            Assert.IsTrue(!country.GetCapitals().Contains(city), "The country's capitals did not contain the city");
            Assert.IsTrue(city.Capital == false, "The city did not show it was a capital");
        }
        [TestMethod()]
        [ExpectedException(typeof(CityException))]
        public void CreateCity_PopulationLessThan0_ShouldThrowCityException()
        {
            Country c = GetStandardCountry();
            City city = new City("testCity", -1, c, false);
        }
        [TestMethod()]
        [ExpectedException(typeof(CityException))]
        public void CreateCity_PopulationIs0_ShouldThrowCityException()
        {
            Country c = GetStandardCountry();
            City city = new City("testCity", 0, c, false);
        }
        [TestMethod()]
        [ExpectedException(typeof(CityException))]
        public void CreatedCity_SetPopulationTo0_ShouldThrowCityException()
        {
            City c = GetStandardCity();
            c.Population = 0;
        }
        [TestMethod()]
        [ExpectedException(typeof(CityException))]
        public void CreatedCity_SetPopulationToLessThan0_ShouldThrowCityException()
        {
            City c = GetStandardCity();
            c.Population = -1;
        }

        [TestMethod()]
        [ExpectedException(typeof(CityException))]
        public void CreateCity_NullValueAsCountry_ShouldThrowCityException()
        {
            City city = new City("testCity", -1, null, false);
        }

        [TestMethod()]
        [ExpectedException(typeof(CityException))]
        public void CreatedCity_SetCountryToNull_ShouldThrowCityException()
        {
            City city = GetStandardCity();
            city.Country = null;
        }


    }
}
