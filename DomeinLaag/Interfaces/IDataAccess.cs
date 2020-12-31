using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Interfaces
{
    public interface IDataAccess
    {
        public ICityRepository Cities { get; set; }
        public IContinentRepository Continents { get; set; }
        public ICountryRepository Countries { get; set; }
        public IRiverRepository Rivers { get; set; }
    }
}
