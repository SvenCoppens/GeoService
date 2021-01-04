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
    public class ContinentRepository : IContinentRepository
    {
        private CountryContext Context;
        public ContinentRepository(CountryContext context)
        {
            Context = context;
        }

        public Continent AddContinent(Continent continent)
        {
            DataContinent data = DataModelConverter.ConvertContinentToContinentData(continent);
            Context.Continents.Add(data);
            Context.SaveChanges();
            return DataModelConverter.ConvertContinentDataToContinent(data);
        }

        public void DeleteContinent(int continentId)
        {
            DataContinent data = GetContinentDataForId(continentId);
            Context.Remove(data);
            Context.SaveChanges();
        }
        private DataContinent GetContinentDataForId(int continentId)
        {
            return Context.Continents.Where(x => x.Id == continentId).Include(x => x.Countries).ThenInclude(x => x.Cities).FirstOrDefault();
        }
        public Continent GetContinentForId(int continentId)
        {
            DataContinent data = GetContinentDataForId(continentId);
            if (data == null)
                return null;
            else
            return DataModelConverter.ConvertContinentDataToContinent(data);
        }

        public Continent UpdateContinent(Continent continent)
        {
            DataContinent data = DataModelConverter.ConvertContinentToContinentData(continent);
            DataContinent original = Context.Continents.Find(data.Id);
            original.Countries = data.Countries;
            original.Name = data.Name;
            Context.Continents.Update(original);
            Context.SaveChanges();
            return DataModelConverter.ConvertContinentDataToContinent(original);
        }

        public bool IsNameAvailable(string name)
        {
            return !Context.Continents.Any(x => x.Name == name);
        }
    }
}
