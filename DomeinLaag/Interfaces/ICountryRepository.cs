using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Interfaces
{
    public interface ICountryRepository
    {
        Country AddCountry(Country country);
        Country GetCountryForId(int id);
        Country UpdateCountry(Country country);
        void DeleteCountry(int countryId);
    }
}
