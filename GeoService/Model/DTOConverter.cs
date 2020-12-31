using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.Model
{
    public static class DTOConverter
    {
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
                countryStrings[i] = CreateCountryIdString(countries[i].Id);
            }

            result.Countries = countryStrings;
            return result;
        }
        public static CityDTOOut ConvertCityToDTOOut(City city)
        {
            CityDTOOut result = new CityDTOOut();
            result.Name = city.Name;
            result.CityId = CreateCityIdString(city.Id);
            result.Population = city.Population;

            result.Country = CreateCountryIdString(city.Country.Id);
            return result;
        }
        public static CountryDTOOut ConvertCountryToDTOOut(Country country)
        {
            CountryDTOOut result = new CountryDTOOut();
            result.Continent = CreateCountryIdString(country.Continent.Id);
            result.Name = country.Name;
            result.Population = country.Population;
            result.Surface = country.SurfaceArea;
            result.CountryId = CreateCountryIdString(country.Id);

            var capitals = country.Capitals;
            string[] capitalStrings = new string[capitals.Count];
            for (int i = 0; i < capitals.Count; i++)
            {
                capitalStrings[i] = CreateCountryIdString(capitals[i].Id);
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
                countryStrings[i] = CreateCountryIdString(countries[i].Id);
            }
            result.Countries = countryStrings;
            return result;
        }


        private static string CreateContinentIdString(int ContinentId)
        {

        }
        private static string CreateCityIdString(int cityId)
        {

        }
        private static string CreateCountryIdString(int countryId)
        {

        }
        private static string CreateRiverIdString(int riverId)
        {

        }
    }
}
