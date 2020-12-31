using DataLaag;
using DomeinLaag;
using DomeinLaag.Model;
using GeoService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.Model
{
    public class ApiWork : IApiWork
    {
        private CountryManager Domein;
        public ApiWork(CountryManager countrymanager)
        {
            Domein = countrymanager;
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
            
            City result = Domein.UpdateCity(original);
            return DTOConverter.ConvertCityToDTOOut(result);
        }

        public ContinentDTOOut UpdateContinent(ContinentDTOIn continent)
        {
            Continent original = Domein.GetContinentForId(continent.ContinentId);
            original.Naam = continent.Name;
            Continent result = Domein.UpdateContinent(original);
            return DTOConverter.ConvertContinentToDTOOut(result);
        }

        public CountryDTOOut UpdateCountry(CountryDTOIn countryIn)
        {
            Country original = Domein.GetCountryForId(countryIn.CountryId);
            original.Population = countryIn.Population;
            original.Continent = Domein.GetContinentForId(countryIn.ContinentId);
            original.Name = countryIn.Name;
            original.SurfaceArea = countryIn.SurfaceArea;

            Country result = Domein.UpdateCountry(original);
            return DTOConverter.ConvertCountryToDTOOut(result);
        }

        public RiverDTOOut UpdateRivier(RiverDTOIn riverIn)
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

        public void VerwijderCity(int cityId)
        {
            Domein.DeleteCity(cityId);
        }

        public void VerwijderContinent(int id)
        {
            Domein.DeleteContinent(id);
        }

        public void VerwijderCountry(int countryId)
        {
            Domein.DeleteCountry(countryId);
        }

        public void VerwijderRivier(int id)
        {
            Domein.DeleteRiver(id);
        }

        public ContinentDTOOut VoegContinentToe(ContinentDTOIn continent)
        {
            Continent result = Domein.AddContinent(continent.Name);
            return DTOConverter.ConvertContinentToDTOOut(result);
        }

        public CountryDTOOut VoegLandToe(CountryDTOIn country)
        {
            Continent continent = Domein.GetContinentForId(country.ContinentId);
            Country result = Domein.AddCountry(country.Name, country.Population, country.SurfaceArea, continent);
            return DTOConverter.ConvertCountryToDTOOut(result);
        }

        public RiverDTOOut VoegRivierToe(RiverDTOIn rivier)
        {
            List<Country> countries = new List<Country>();
            foreach(int id in rivier.CountryIds)
            {
                countries.Add(Domein.GetCountryForId(id));
            }
            River result = Domein.AddRiver(rivier.Name, rivier.Length,countries);
            return DTOConverter.ConvertRiverToDTOOut(result);
        }

        public CityDTOOut VoegStadToe(CityDTOIn city)
        {
            Country country = Domein.GetCountryForId(city.CountryId);
            City result = Domein.AddCity(city.Name, city.Population, country);

            return DTOConverter.ConvertCityToDTOOut(result);
        }
    }
}
