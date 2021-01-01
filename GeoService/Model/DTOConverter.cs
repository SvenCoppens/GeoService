using DomeinLaag.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.Model
{
    public class DTOConverter
    {
        //hier iets vinden om dit static te regelen.
        public DTOConverter(IConfiguration config)
        {
            CreateHostString(config);
        }
        private static string HostString;
        //out
        public static ContinentDTOOut ConvertContinentToDTOOut(Continent continent)
        {
            ContinentDTOOut result = new ContinentDTOOut();
            result.Name = continent.Naam;
            result.Population = continent.GetPopulation();
            result.ContinentId = CreateContinentIdString(continent.Id);
            var countries = continent.GetCountries();
            string[] countryStrings = new string[countries.Count];
            for(int i=0;i<countries.Count;i++)
            {
                countryStrings[i] = CreateCountryIdString(continent.Id,countries[i].Id);
            }

            result.Countries = countryStrings;
            return result;
        }
        public static CityDTOOut ConvertCityToDTOOut(City city)
        {
            CityDTOOut result = new CityDTOOut();
            result.Name = city.Name;
            result.CityId = CreateCityIdString(city.Country.Continent.Id,city.Country.Id,city.Id);
            result.Population = city.Population;

            result.Country = CreateCountryIdString(city.Country.Continent.Id, city.Country.Id);
            return result;
        }
        public static CountryDTOOut ConvertCountryToDTOOut(Country country)
        {
            CountryDTOOut result = new CountryDTOOut();
            result.Continent = CreateContinentIdString(country.Continent.Id);
            result.Name = country.Name;
            result.Population = country.Population;
            result.Surface = country.SurfaceArea;
            result.CountryId = CreateCountryIdString(country.Continent.Id, country.Id);

            var capitals = country.Capitals;
            string[] capitalStrings = new string[capitals.Count];
            for (int i = 0; i < capitals.Count; i++)
            {
                capitalStrings[i] = CreateCityIdString(country.Continent.Id, country.Id, capitals[i].Id);
            }
            result.Cities = capitalStrings;
            return result;
        }
        public static RiverDTOOut ConvertRiverToDTOOut(River river)
        {
            RiverDTOOut result = new RiverDTOOut();
            result.RiverId = CreateRiverIdString(river.Id);
            result.Name = river.Name;
            result.Length = river.LengthInKm;


            var countries = river.GetCountries();
            string[] countryStrings = new string[countries.Count];
            for (int i = 0; i < countries.Count; i++)
            {
                countryStrings[i] = CreateCountryIdString(countries[i].Continent.Id,countries[i].Id);
            }
            result.Countries = countryStrings;
            return result;
        }

        private static void CreateHostString(IConfiguration iConfiguration)
        { 
            HostString = iConfiguration.GetValue<string>("iisSettings:IISExpress:applicationUrl");
        }
        private static string CreateContinentIdString(int continentId)
        {
            return HostString + @"/api/continent/" + continentId;
        }
        private static string CreateCityIdString(int continentId, int countryId,int cityId)
        {
            return HostString + @"/api/continent/" + continentId + "/Country/" + countryId+"/City/"+cityId;
        }
        private static string CreateCountryIdString(int continentId,int countryId)
        {
            return HostString + @"/api/continent/" + continentId + "/Country/"+countryId;
        }
        private static string CreateRiverIdString(int riverId)
        {
            return HostString + @"/api/continent/" + riverId;
        }
    }
}
