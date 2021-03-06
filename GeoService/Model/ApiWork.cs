﻿using DataLaag;
using DomeinLaag;
using DomeinLaag.Model;
using GeoService.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.Model
{
    public class ApiWork : IApiWork
    {
        private ICountryManager Domein;
        private DTOConverter converter;
        public ApiWork(ICountryManager countrymanager,IConfiguration config)
        {
            Domein = countrymanager;
            converter = new DTOConverter(config);
        }
        public ContinentDTOOut GetContinentForId(int id)
        {
            Continent result = Domein.GetContinentForId(id);
            return DTOConverter.ConvertContinentToDTOOut(result);
        }

        public CountryDTOOut GetCountryForId(int id)
        {
            Country result = Domein.GetCountryForId(id);
            return DTOConverter.ConvertCountryToDTOOut(result);
        }

        public RiverDTOOut GetRiverForId(int id)
        {
            River result = Domein.GetRiverForId(id);
            return DTOConverter.ConvertRiverToDTOOut(result);
        }

        public CityDTOOut GetCityForId(int id)
        {
            City result = Domein.GetCityForId(id);
            return DTOConverter.ConvertCityToDTOOut(result);
        }

        public CityDTOOut UpdateCity(CityDTOIn city)
        {
            City original = Domein.GetCityForId(city.CityId);
            original.Population = city.Population;
            original.Name = city.Name;
            original.Capital = city.Capital;
            
            City result = Domein.UpdateCity(original);
            return DTOConverter.ConvertCityToDTOOut(result);
        }

        public ContinentDTOOut UpdateContinent(ContinentDTOIn continent)
        {
            Continent original = Domein.GetContinentForId(continent.ContinentId);
            original.Name = continent.Name;
            Continent result = Domein.UpdateContinent(original);
            return DTOConverter.ConvertContinentToDTOOut(result);
        }

        public CountryDTOOut UpdateCountry(CountryDTOIn countryIn)
        {
            Country original = Domein.GetCountryForId(countryIn.CountryId);
            original.Population = countryIn.Population;
            Continent newContinent = Domein.GetContinentForId(countryIn.ContinentId);
            if(!original.Continent.Equals(newContinent))
                original.Continent = Domein.GetContinentForId(countryIn.ContinentId);
            original.Name = countryIn.Name;
            original.SurfaceArea = countryIn.SurfaceArea;

            Country result = Domein.UpdateCountry(original);
            return DTOConverter.ConvertCountryToDTOOut(result);
        }

        public RiverDTOOut UpdateRiver(RiverDTOIn riverIn)
        {
            River original = Domein.GetRiverForId(riverIn.RiverId);
            original.LengthInKm = riverIn.Length;
            original.Name = riverIn.Name;
            List<Country> countries = new List<Country>();
            foreach (int id in riverIn.CountryIds)
            {
                countries.Add(Domein.GetCountryForId(id));
            }
            original.SetCountries(countries);
            River result = Domein.UpdateRiver(original);
            return DTOConverter.ConvertRiverToDTOOut(result);
        }

        public void DeleteCity(int cityId)
        {
            Domein.DeleteCity(cityId);
        }

        public void DeleteContinent(int id)
        {
            Domein.DeleteContinent(id);
        }

        public void DeleteCountry(int countryId)
        {
            Domein.DeleteCountry(countryId);
        }

        public void DeleteRiver(int id)
        {
            Domein.DeleteRiver(id);
        }

        public ContinentDTOOut AddContinent(ContinentDTOIn continent)
        {
            Continent result = Domein.AddContinent(continent.Name);
            return DTOConverter.ConvertContinentToDTOOut(result);
        }

        public CountryDTOOut AddCountry(CountryDTOIn country)
        {
            Continent continent = Domein.GetContinentForId(country.ContinentId);
            Country result = Domein.AddCountry(country.Name, country.Population, country.SurfaceArea, continent);
            return DTOConverter.ConvertCountryToDTOOut(result);
        }

        public RiverDTOOut AddRiver(RiverDTOIn rivier)
        {
            List<Country> countries = new List<Country>();
            foreach(int id in rivier.CountryIds)
            {
                countries.Add(Domein.GetCountryForId(id));
            }
            River result = Domein.AddRiver(rivier.Name, rivier.Length,countries);
            return DTOConverter.ConvertRiverToDTOOut(result);
        }

        public CityDTOOut AddCity(CityDTOIn city)
        {
            Country country = Domein.GetCountryForId(city.CountryId);
            City result = Domein.AddCity(city.Name, city.Population, country,city.Capital);

            return DTOConverter.ConvertCityToDTOOut(result);
        }

        
    }
}
