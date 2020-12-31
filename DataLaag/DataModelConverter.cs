﻿using DataLaag.DataModel;
using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLaag
{
    public static class DataModelConverter
    {
        internal static DataCity ConvertCityToCityData(City city)
        {
            DataCity result = new DataCity();
            result.CountryId = city.Country.Id;
            result.Name = city.Name;
            result.Population = city.Population;
            result.Capital = city.Capital;
            return result;
        }

        internal static City ConvertCityDataToCity(DataCity data)
        {
            Country country = ConvertCountryDataToCountry(data.Country);
            City result = new City(data.Name, data.Population, country,data.Capital);
            result.Id = data.Id;
            return result;
        }

        internal static DataContinent ConvertContinentToContinentData(Continent continent)
        {
            DataContinent result = new DataContinent();
            result.Id = continent.Id;
            result.Name = continent.Naam;
            var collection = continent.GetCountries();
            if(collection != null)
            {
                foreach (Country c in collection)
                {
                    result.Countries.Add(ConvertCountryToDataCountry(c));
                }
            }

            return result;
        }

        internal static River ConvertRiverDataToRiver(DataRiver data)
        {
            List<Country> countries = new List<Country>();
            if (data.CountryLink != null)
            {
                foreach (DataCountryRiver countryRiver in data.CountryLink)
                {
                    countries.Add(ConvertCountryDataToCountry(countryRiver.Country));
                }
            }
            River result = new River(data.Name,data.Length,countries);
            result.Id = data.Id;
            return result;
        }

        internal static DataRiver ConvertRiverToRiverData(River river)
        {
            DataRiver data = new DataRiver();
            data.Id = river.Id;
            data.Length = river.LengthInKm;
            data.Name = river.Name;
            foreach(Country country in river.GetCountries())
            {
                DataCountryRiver temp = new DataCountryRiver();
                temp.RiverId = data.Id;
                temp.CountryId = country.Id;
                data.CountryLink.Add(temp);
            }
            return data;
        }

        internal static DataCountry ConvertCountryToDataCountry(Country country)
        {
            DataCountry data = new DataCountry();
            data.ContinentId = country.Continent.Id;
            data.Id = country.Id;
            data.Name = country.Name;
            data.Population = country.Population;
            data.Surface = country.SurfaceArea;

            return data;
        }

        internal static Country ConvertCountryDataToCountry(DataCountry data)
        {
            Continent continent = ConvertContinentDataToContinent(data.Continent);
            Country country = new Country(data.Name, data.Population, data.Surface, continent);
            country.Id = data.Id;
            return country;
        }

        internal static Continent ConvertContinentDataToContinent(DataContinent data)
        {
            Continent result = new Continent(data.Name);
            result.Id = data.Id;
            if (data.Countries != null)
            {
                foreach (DataCountry c in data.Countries)
                {
                    CreateCountryToAddToContinent(c,result);
                }
            }
            return result;
        }
        internal static Country CreateCountryToAddToContinent(DataCountry country,Continent continent)
        {
            Country countryResult = new Country(country.Name, country.Population, country.Surface, continent);
            country.Id = country.Id;
            return countryResult;
        }
    }
}