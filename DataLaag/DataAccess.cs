using DomeinLaag.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLaag
{
    public class DataAccess : IDataAccess
    {
        private CountryContext Context;
        public DataAccess(string db="Production")
        {
            Context = new CountryContext(db);
            Cities = new CityRepository(Context);
            Continents = new ContinentRepository(Context);
            Countries = new CountryRepository(Context);
            Rivers = new RiverRepository(Context);
        }
        public ICityRepository Cities { get; set ; }
        public IContinentRepository Continents { get ; set; }
        public ICountryRepository Countries { get; set; }
        public IRiverRepository Rivers { get; set; }
    }
}
