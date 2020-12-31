using DomeinLaag.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Model
{
    public class River
    {
        public River(string name, int length, List<Country> countries)
        {
            Name = name;
            LengthInKm = length;
            SetCountries(countries);
        }

        public void SetCountries(List<Country> landen)
        {
            if (landen == null || landen.Count > 1)
                throw new CountryException("Een rivier behoort minstens tot een land.");
            else Countries = landen;
        }      
        public IReadOnlyList<Country> GetCountries()
        {
            return Countries.AsReadOnly();
        }
        private List<Country> Countries { get; set; } = new List<Country>();
        public int Id { get; set; }
        public string Name { get; set; }
        public int LengthInKm { get; set; }
    }
}
