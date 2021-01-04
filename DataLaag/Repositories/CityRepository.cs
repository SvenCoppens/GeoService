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
    public class CityRepository : ICityRepository
    {
        private CountryContext Context;
        public CityRepository(CountryContext context)
        {
            Context = context;
        }

        public City AddCity(City city)
        {
            DataCity data = DataModelConverter.ConvertCityToCityData(city);
            Context.Cities.Add(data);
            Context.SaveChanges();
            return DataModelConverter.ConvertCityDataToCity(data);
        }

        public void DeleteCity(int cityId)
        {
            DataCity result = Context.Cities.Find(cityId);
            Context.Remove(result);
            Context.SaveChanges();
        }

        public City GetCityForId(int cityId)
        {
            DataCity result = GetDataCityForId(cityId);
            if (result == null)
                return null;
            else
            return DataModelConverter.ConvertCityDataToCity(result);
        }
        private DataCity GetDataCityForId(int cityId)
        {
            return Context.Cities.Where(x => x.Id == cityId).Include(x => x.Country).ThenInclude(x => x.Continent).FirstOrDefault();
        }
        public City UpdateCity(City city)
        {
            DataCity newCity = DataModelConverter.ConvertCityToCityData(city);
            DataCity original = GetDataCityForId(city.Id);
            original.CountryId = newCity.CountryId;
            original.Name = newCity.Name;
            original.Population = newCity.Population;
            original.Capital = newCity.Capital;
            Context.Cities.Update(original);
            Context.SaveChanges();
            return DataModelConverter.ConvertCityDataToCity(original);
        }
    }
}
