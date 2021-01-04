using GeoService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.Interfaces
{
    public interface IApiWork
    {
        ContinentDTOOut AddContinent(ContinentDTOIn continent);
        ContinentDTOOut GetContinentForId(int id);
        CountryDTOOut GetCountryForId(int id);
        ContinentDTOOut UpdateContinent(ContinentDTOIn continent);
        void DeleteContinent(int id);
        CountryDTOOut AddCountry(CountryDTOIn country);
        void DeleteCountry(int countryId);
        CountryDTOOut UpdateCountry(CountryDTOIn countryIn);
        CityDTOOut AddCity(CityDTOIn city);
        CityDTOOut GetCityForId(int id);
        void DeleteCity(int cityId);
        CityDTOOut UpdateCity(CityDTOIn city);


        RiverDTOOut AddRiver(RiverDTOIn rivier);

        RiverDTOOut GetRiverForId(int id);
        void DeleteRiver(int id);
        RiverDTOOut UpdateRiver(RiverDTOIn rivier);
        
    }
}
