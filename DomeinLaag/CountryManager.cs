using DomeinLaag.Exceptions;
using DomeinLaag.Interfaces;
using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag
{
    public class CountryManager
    {
        public IDataAccess Data { get; set; }
        public CountryManager(IDataAccess dataAccess)
        {
            Data = dataAccess;
        }
        public Continent AddContinent(string name)
        {
            Continent continent = new Continent(name);

            Continent result = Data.Continents.AddContinent(continent);
            return result;
        }
        
        public Country AddCountry(string name, int population, int surfaceArea,Continent continent)
        {
            Country country = new Country(name, population, surfaceArea, continent);
            Country result = Data.Countries.AddCountry(country);
            return result;
        }
        public River AddRiver(string name, int length, List<Country> countries)
        {
            River river = new River(name, length, countries);

            River result = Data.Rivers.AddRiver(river);
            return result;
        }
        public City AddCity(string name, int population,Country country,bool capital=false)
        {
            City city = new City(name, population, country,capital);

            City result = Data.Cities.AddCity(city);
            return result;
        }

        public Continent GetContinentForId(int id)
        {
            Continent continent = Data.Continents.GetContinentForId(id);
            return continent;
        }
        public Country GetCountryForId(int id)
        {
            Country country = Data.Countries.GetCountryForId(id);
            return country;
        }

        public River GetRiverForId(int id)
        {
            River river = Data.Rivers.GetRiverForId(id);
            return river;
        }
        public City GetCityForId(int id)
        {
            City city = Data.Cities.GetCityForId(id);
            return city;
        }
        public City UpdateCity(City city)
        {
            City updated = Data.Cities.UpdateCity(city);
            return updated;
        }
        public Continent UpdateContinent(Continent continent)
        {
            Continent updated = Data.Continents.UpdateContinent(continent);
            return updated;
        }
        public Country UpdateCountry(Country country)
        {
            Country updated = Data.Countries.UpdateCountry(country);
            return updated;
        }
        public River UpdateRiver(River river)
        {
            River updated = Data.Rivers.UpdateRiver(river);
            return updated;
        }
        public void DeleteRiver(int riverId)
        {
            Data.Rivers.DeleteRiver(riverId);
        }
        public void DeleteCity(int cityId)
        {
            Data.Cities.DeleteCity(cityId);
        }
        public void DeleteContinent(int continentId)
        {
            Continent continent = GetContinentForId(continentId);
            if (continent.GetCountries().Count != 0)
                throw new DomainException("All countries within a continent must be deleted before the continent can be deleted");
            Data.Continents.DeleteContinent(continentId);
        }
        public void DeleteCountry(int countryId)
        {
            Country country = GetCountryForId(countryId);
            if (country.GetCities().Count != 0)
                throw new DomainException("All cities within a country must be deleted before the country can be deleted");
            Data.Countries.DeleteCountry(countryId);
        }
    }
}
