using DataLaag;
using DomeinLaag;
using DomeinLaag.Model;
using System;
using System.Collections.Generic;

namespace ManualTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccess DA = new DataAccess("Test");
            CountryManager CM = new CountryManager(DA);
            AddFirstContinent(DA);
            //AddFirstCountry(DA);
            AddSecondCountry(CM);
            //AddFirstCity(DA);
            //AddSecondCity(CM);
            //AddCapital(CM);
            AddFirstRiverWithoutCountries(DA);
            AddSecondRiverWithCountries(DA);
            AddThirdRiverWithCountries(CM);
            GetThirdRiver(CM); 
        }

        private static void GetThirdRiver(CountryManager cM)
        {
            var x = cM.GetRiverForId(3);
            Console.WriteLine();
        }

        private static void AddThirdRiverWithCountries(CountryManager cM)
        {
            Country country = cM.GetCountryForId(1);
            cM.AddRiver("TestRiver1", 25, new List<Country> { country });
        }

        private static void AddSecondRiverWithCountries(DataAccess dA)
        {
            Country country = dA.Countries.GetCountryForId(1);
            River river = new River("TestRiver1", 25, new List<Country> { country });
            dA.Rivers.AddRiver(river);
        }

        private static void AddFirstRiverWithoutCountries(DataAccess dA)
        {
            River river = new River("TestRiver1", 25, new List<Country>());
            dA.Rivers.AddRiver(river);
        }

        private static void AddCapital(CountryManager cM)
        {
            Country country = cM.GetCountryForId(1);
            City capital = cM.AddCity("testCity3", 20, country,true);
        }

        private static void AddSecondCity(CountryManager cM)
        {
            Country country = cM.GetCountryForId(1);
            cM.AddCity("testCity2", 30, country);
        }

        private static void AddFirstCity(DataAccess dA)
        {
            Country country = dA.Countries.GetCountryForId(1);
            City city = new City("testCity1", 25, country,false);
            dA.Cities.AddCity(city);
        }

        private static void AddSecondCountry(CountryManager CM)
        {
            Continent continent = CM.GetContinentForId(1);
            CM.AddCountry("testCountry2",25000,200,continent);
        }

        private static void AddFirstCountry(DataAccess DA)
        {
            Continent continent = DA.Continents.GetContinentForId(1);
            Country country = new Country("testCountry1", 5000, 35000, continent);
            DA.Countries.AddCountry(country);
        }

        private static void AddFirstContinent(DataAccess DA)
        {

            Continent continent = new Continent("testContinent1");
            DA.Continents.AddContinent(continent);
        }
    }
}
