using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomeinLaag;
using System;
using System.Collections.Generic;
using System.Text;
using ManualTesting;
using DomeinLaag.Model;
using System.Linq;
using DomeinLaag.Exceptions;

namespace DomeinLaag.Tests
{
    [TestClass()]
    public class CountryManagerTests
    {
        public CountryManager GetTestingManager()
        {
            return new CountryManager(new TestDataAccess());
        }
        public River CreateFirstTestRiver(CountryManager cM)
        {
            Country country = CreateFirstTestCountry(cM);
            List<Country> countries = new List<Country> { country };
            string name = "testRiver";
            int length = 13000;
            return cM.AddRiver(name, length, countries);
        }
        public Country CreateSecondTestCountry(CountryManager cM)
        {
            Continent continent = CreateSecondTestContinent(cM);
            string name = "TestCountry2";
            int population = 500000;
            int surfaceArea = 8000;
            return cM.AddCountry(name, population, surfaceArea, continent);
        }
        public Continent CreateFirstTestContinent(CountryManager cM)
        {
            string name = "TestContinent1";
            return cM.AddContinent(name);
        }
        public Continent CreateSecondTestContinent(CountryManager cM)
        {
            string name = "TestContinent2";
            return cM.AddContinent(name);
        }
        public City CreateFirstTestCity(CountryManager cM)
        {
            Country country = CreateFirstTestCountry(cM);
            string name = "testCity";
            int population = 13000;
            bool capital = true;
            return cM.AddCity(name, population, country, capital);
        }
        public Country CreateFirstTestCountry(CountryManager cM)
        {
            Continent continent = CreateFirstTestContinent(cM);
            string name = "TestCountry1";
            int population = 300000;
            int surfaceArea = 18000;
            return cM.AddCountry(name, population, surfaceArea, continent);
        }

        [TestMethod()]
        public void AddContinentTest_AddsCorrectly()
        {
            CountryManager cM = GetTestingManager();
            string name = "TestString";
            cM.AddContinent(name);
            Continent continent = cM.GetContinentForId(1);

            Assert.IsTrue(continent.Name == name, "The name was not correct");
            Assert.IsTrue(continent.Id == 1, "The name was not correct");
        }

        [TestMethod()]
        public void AddContinentTest_ReturnsAddedContinent()
        {
            CountryManager cM = GetTestingManager();
            string name = "TestString";
            Continent continent2 = cM.AddContinent(name);
            Continent continent1 = cM.GetContinentForId(1);

            Assert.IsTrue(continent1.Equals(continent2), "the continent were not equal");
        }
        [TestMethod()]
        public void AddCountryTest_AddsCorrectly()
        {
            CountryManager cM = GetTestingManager();
            Continent continent = CreateFirstTestContinent(cM);
            string name = "TestCountry";
            int population = 300;
            int surfaceArea = 18000;
            Country c = cM.AddCountry(name, population, surfaceArea, continent);
            Country country = cM.GetCountryForId(1);

            Assert.IsTrue(country.Name == name, "The name was not correct.");
            Assert.IsTrue(country.Population == population, "the population was not correct.");
            Assert.IsTrue(country.SurfaceArea == surfaceArea, "The surface area was not correct.");

        }
        [TestMethod()]
        public void AddCountryTest_ReturnsAdddedCountry()
        {
            CountryManager cM = GetTestingManager();
            Continent continent = CreateFirstTestContinent(cM);
            string name = "TestCountry";
            int population = 300;
            int surfaceArea = 18000;
            Country c = cM.AddCountry(name, population, surfaceArea, continent);
            Country country = cM.GetCountryForId(1);

            Assert.IsTrue(country.Equals(c), "The countries were not equal.");

        }

        [TestMethod()]
        public void AddCityTest_AddedCorrectly()
        {
            CountryManager cM = GetTestingManager();
            Country country = CreateFirstTestCountry(cM);
            string name = "testCity";
            int population = 13000;
            bool capital = false;
            City c = cM.AddCity(name, population, country, capital);
            City result = cM.GetCityForId(1);

            Assert.IsTrue(result.Capital == capital, "The capital check was not correct.");
            Assert.IsTrue(result.Country.Equals(country), "The countries were not equal.");
            Assert.IsTrue(result.Id == 1, "The id was not correct.");
            Assert.IsTrue(result.Name == name, "The name was not correct.");
            Assert.IsTrue(result.Population == population, "The population was not correct");

            Assert.IsTrue(result.Country.GetCities().Contains(result), "The country did not add the city to its collection.");
        }

        [TestMethod()]
        public void AddCityTest_ReturnsAddedCity()
        {
            CountryManager cM = GetTestingManager();
            Country country = CreateFirstTestCountry(cM);
            string name = "testCity";
            int population = 13000;
            bool capital = false;
            City c = cM.AddCity(name, population, country, capital);
            City result = cM.GetCityForId(1);

            Assert.IsTrue(c.Equals(result), "The cities were not equal.");
        }


        [TestMethod()]
        public void AddRiverTest_AddedCorrectly()
        {
            CountryManager cM = GetTestingManager();
            Country country = CreateFirstTestCountry(cM);
            List<Country> countries = new List<Country> { country };
            string name = "testRiver";
            int length = 13000;
            River r = cM.AddRiver(name, length, countries);
            River result = cM.GetRiverForId(1);

            Assert.IsTrue(result.LengthInKm == length, "The length was not correct.");
            Assert.IsTrue(result.Id == 1, "The id was not correct.");
            Assert.IsTrue(result.Name == name, "The name was not correct.");
            Assert.IsTrue(result.GetCountries().SequenceEqual(countries), "The countries were not correct");

            Assert.IsTrue(result.GetCountries()[0].GetRivers()[0].Equals(result), "The country did not add the river to its collection.");
        }

        [TestMethod()]
        public void AddRiverTest_ReturnsAddedRiver()
        {
            CountryManager cM = GetTestingManager();
            Country country = CreateFirstTestCountry(cM);
            List<Country> countries = new List<Country> { country };
            string name = "testRiver";
            int length = 13000;
            River r = cM.AddRiver(name, length, countries);
            River result = cM.GetRiverForId(1);

            Assert.IsTrue(r.Equals(result), "The rivers were not equal.");
        }


        #region updateTests
        [TestMethod()]
        public void UpdateContinentTest_ShouldUpdateNameCorrectly()
        {
            CountryManager cM = GetTestingManager();
            Continent c = CreateFirstTestContinent(cM);
            string newName = "newTestingContinentnew";
            c.Name = newName;
            cM.UpdateContinent(c);

            Continent result = cM.GetContinentForId(1);
            Assert.IsTrue(result.Name == newName, "The name was not updated correctly.");
        }
        [TestMethod()]
        public void UpdateContinentTest_ShouldReturnTheCorrectContinent()
        {
            CountryManager cM = GetTestingManager();
            Continent c = CreateFirstTestContinent(cM);
            string newName = "newTestingContinentnew";
            c.Name = newName;
            Continent secondresult = cM.UpdateContinent(c);

            Continent result = cM.GetContinentForId(1);
            Assert.IsTrue(result.Equals(secondresult), "The continents were not equal.");
        }
        [TestMethod()]
        public void UpdateCountryTest_ShouldUpdateCorrectly()
        {
            CountryManager cM = GetTestingManager();
            Country c = CreateFirstTestCountry(cM);
            Continent newContinent = CreateSecondTestContinent(cM);
            string newName = "newTestingCountrynew";
            int population = 15;
            int surface = 10;
            c.Name = newName;
            c.Population = population;
            c.SurfaceArea = surface;
            c.Continent = newContinent;
            Country result = cM.UpdateCountry(c);
            Country country = cM.GetCountryForId(1);

            Continent firstContinent = cM.GetContinentForId(1);

            Assert.IsTrue(country.Id == 1, "The id was not correct.");
            Assert.IsTrue(country.Name == newName, "The name was not updated correctly.");
            Assert.IsTrue(country.Population == population, "The population was not updated correctly.");
            Assert.IsTrue(country.SurfaceArea == surface, "The surface area was not updated correctly.");
            Assert.IsTrue(country.Continent.Equals(newContinent), "The continent was not updated correctly");

            Assert.IsTrue(firstContinent.GetCountries().Count == 0, "The country was not properly removed from the first continent.");
        }

        [TestMethod()]
        public void UpdateCountryTest_ShouldReturnUpdatedCountry()
        {
            CountryManager cM = GetTestingManager();
            Country c = CreateFirstTestCountry(cM);
            Continent newContinent = CreateSecondTestContinent(cM);
            string newName = "newTestingCountrynew";
            int population = 15;
            int surface = 10;
            c.Name = newName;
            c.Population = population;
            c.SurfaceArea = surface;
            c.Continent = newContinent;
            Country result = cM.UpdateCountry(c);
            Country country = cM.GetCountryForId(1);

            Assert.IsTrue(country.Equals(result), "The countries were not equal.");
        }

        [TestMethod()]
        public void UpdateCityTest_ShouldUpdateCorrectly()
        {
            CountryManager cM = GetTestingManager();
            City city = CreateFirstTestCity(cM);
            Country country = CreateSecondTestCountry(cM);
            string name = "NewTestNameStringNew";
            int population = 12345;
            city.Country = country;
            city.Name = name;
            city.Population = population;
            cM.UpdateCity(city);

            Country firstCountry = cM.GetCountryForId(1);

            City result = cM.GetCityForId(1);
            Assert.IsTrue(result.Population == population, "The population was not properly updated");
            Assert.IsTrue(result.Name == name, "The name was not properly updated");
            Assert.IsTrue(result.Country.Equals(country), "The country was not properly updated.");
            Assert.IsTrue(country.GetCities()[0].Equals(city), "the city was not properly added to the country");
            Assert.IsTrue(firstCountry.GetCities().Count == 0, "The city was not properly removed from the original country");
            Assert.IsTrue(country.GetCities().Count == 1, "The city was not properly added to the new country.");
            Assert.IsTrue(country.GetCapitals().Count == 1, "The city was not properly added to capitals of the new country.");

        }

        [TestMethod()]
        public void UpdateCityTest_ShouldReturnUpdatedCity()
        {
            CountryManager cM = GetTestingManager();
            City city = CreateFirstTestCity(cM);
            Country country = CreateSecondTestCountry(cM);
            string name = "NewTestNameStringNew";
            int population = 12345;
            city.Country = country;
            city.Name = name;
            city.Population = population;
            City updated = cM.UpdateCity(city);

            City firstCity = cM.GetCityForId(1);


            Assert.IsTrue(firstCity.Equals(updated), "The cities were not equal.");

        }

        [TestMethod()]
        public void UpdateRiverTest_ShouldReturnUpdatedRiver()
        {
            CountryManager cM = GetTestingManager();
            River r = CreateFirstTestRiver(cM);
            string name = "NewTestNameStringNew";
            int length = 12345;
            r.LengthInKm = length;
            r.Name = name;
            River updated = cM.UpdateRiver(r);

            River firstRiver = cM.GetRiverForId(1);

            Assert.IsTrue(firstRiver.Equals(updated), "The cities were not equal.");

        }
        [TestMethod()]
        public void UpdateRiverTest_ShouldUpdateCorrectly()
        {
            CountryManager cM = GetTestingManager();
            River r = CreateFirstTestRiver(cM);
            List<Country> countries = new List<Country> { CreateSecondTestCountry(cM) };
            string name = "NewTestNameStringNew";
            int length = 12345;
            r.LengthInKm = length;
            r.Name = name;
            r.SetCountries(countries);
            River updated = cM.UpdateRiver(r);

            Country firstCountry = cM.GetCountryForId(1);
            River firstRiver = cM.GetRiverForId(1);

            Assert.IsTrue(firstRiver.LengthInKm == length, "The length was not properly updated.");
            Assert.IsTrue(firstRiver.Name == name, "The name was not properly updated.");
            Assert.IsTrue(firstCountry.GetRivers().Count() == 0, "The rivers of the original country did not get updated correctly.");
            Assert.IsTrue(firstRiver.GetCountries()[0].GetRivers().Count() == 1, "The rivers of the new country was not properly updated.");

        }

        #endregion



        #region
        [TestMethod()]
        public void DeleteCityTest_ShouldProperlyDeleteCity()
        {
            CountryManager cM = GetTestingManager();
            City city = CreateFirstTestCity(cM);
            var y = cM.GetCityForId(1);
            cM.DeleteCity(1);
            Assert.ThrowsException<DomainException>(() => cM.GetCityForId(1),"The City was not properly deleted");
        }
        [TestMethod()]
        public void DeleteCountryTest_ShouldProperlyDeleteCountry()
        {
            CountryManager cM = GetTestingManager();
            Country country = CreateFirstTestCountry(cM);
            cM.GetCountryForId(1);
            cM.DeleteCountry(1);
            Assert.ThrowsException<DomainException>(() => cM.GetCountryForId(1), "The Country was not properly deleted");
        }
        [TestMethod()]
        [ExpectedException(typeof(DomainException))]
        public void DeleteCountryTest_StillHasCities_ShouldPreventDeletingCountry()
        {
            CountryManager cM = GetTestingManager();
            City city = CreateFirstTestCity(cM);
            cM.DeleteCountry(1);
        }
        [TestMethod()]
        public void DeleteRiverTest_ShouldProperlyDeleteRiver()
        {
            CountryManager cM = GetTestingManager();
            River river = CreateFirstTestRiver(cM);
            cM.GetRiverForId(1);
            cM.DeleteRiver(1);
            Assert.ThrowsException<DomainException>(() => cM.GetRiverForId(1), "The river was not properly deleted");
        }
        [TestMethod()]
        public void DeleteContinentTest_ShouldProperlyDeleteContinent()
        {
            CountryManager cM = GetTestingManager();
            Continent continent = CreateFirstTestContinent(cM);
            cM.GetContinentForId(1);
            cM.DeleteContinent(1);
            Assert.ThrowsException<DomainException>(() => cM.GetContinentForId(1), "The continent was not properly deleted");

        }
        #endregion

        [TestMethod()]
        [ExpectedException(typeof(DomainException))]
        public void CreateContinentTest_NameMustBeUnique()
        {
            CountryManager cM = GetTestingManager();
            Continent firstContinent = CreateFirstTestContinent(cM);
            cM.AddContinent(firstContinent.Name);
        }


    }
}