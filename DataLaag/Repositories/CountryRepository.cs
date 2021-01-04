using DataLaag;
using DataLaag.DataModel;
using DomeinLaag.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomeinLaag.Interfaces
{
    public class CountryRepository : ICountryRepository
    {
        private CountryContext Context;
        public CountryRepository(CountryContext context)
        {
            Context = context;
        }

        public Country AddCountry(Country country)
        {
            DataCountry data = DataModelConverter.ConvertCountryToDataCountry(country);
            Context.Countries.Add(data);
            Context.SaveChanges();
            return GetCountryForId(data.Id);

        }

        public void DeleteCountry(int countryId)
        {
            DataCountry data = Context.Countries.Find(countryId);
            Context.Countries.Remove(data);
            Context.SaveChanges();
        }

        public Country GetCountryForId(int countryId)
        {
            DataCountry data = GetDataCountryForId(countryId);
            if (data == null)
                return null;
            else
                return DataModelConverter.ConvertCountryDataToCountry(data);
        }
        private DataCountry GetDataCountryForId(int countryId)
        {
            return Context.Countries.Where(x => x.Id == countryId).Include(x => x.Continent).Include(x => x.Cities).FirstOrDefault();
        }
        public Country UpdateCountry(Country country)
        {
            DataCountry data = DataModelConverter.ConvertCountryToDataCountry(country);
            DataCountry original = GetDataCountryForId(country.Id);
            original.Name = data.Name;
            original.Population = data.Population;
            original.Surface = data.Surface;
            original.ContinentId = data.ContinentId;
            Context.Countries.Update(original);
            Context.SaveChanges();
            return DataModelConverter.ConvertCountryDataToCountry(original);
        }
    }
}
