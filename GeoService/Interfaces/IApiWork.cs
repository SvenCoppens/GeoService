using GeoService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.Interfaces
{
    public interface IApiWork
    {
        ContinentDTOOut VoegContinentToe(ContinentDTOIn continent);
        ContinentDTOOut GetContinentForId(int id);
        CountryDTOOut GetCountryForId(int id);
        ContinentDTOOut UpdateContinent(ContinentDTOIn continent);
        void VerwijderContinent(int id);
        CountryDTOOut VoegLandToe(CountryDTOIn country);
        void VerwijderCountry(int countryId);
        CountryDTOOut UpdateCountry(CountryDTOIn countryIn);
        CityDTOOut VoegStadToe(CityDTOIn city);
        CityDTOOut GetCityForId(int id);
        void VerwijderCity(int cityId);
        CityDTOOut UpdateCity(CityDTOIn city);


        RiverDTOOut VoegRivierToe(RiverDTOIn rivier);

        RiverDTOOut GetRiverForId(int id);
        void VerwijderRivier(int id);
        RiverDTOOut UpdateRivier(RiverDTOIn rivier);
        
    }
}
