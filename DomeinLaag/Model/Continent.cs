using DomeinLaag.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Model
{
    public class Continent
    {
        public Continent(string name)
        {
            Naam = name;
        }
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                if (value < 1)
                    throw new ContinentException("De Id mag niet kleiner zijn dan 1.");
                else
                    _Id = value;
            }
        }
        private string _Naam;
        public string Naam
        {
            get { return _Naam; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ContinentException("De naam mag niet leeg of null zijn.");
                else _Naam = value;
            }
        }

        public int GetPopulation()
        {
                int aantal = 0;
                foreach (Country land in Countries)
                {
                    aantal += land.Population;
                }
                return aantal;
        }

        internal void RemoveCountryFromContinent(Country country)
        {
            if (Countries.Contains(country))
                Countries.Remove(country);
            else throw new ContinentException($"the given country with id {country.Id} was not part of the continent with id {Id}");
        }

        public void AddCountryToContinent(Country country)
        {
            foreach(Country l in Countries)
            {
                if (l.Equals(country))
                    throw new ContinentException("This Country is already part of this continent");
                else if (l.Name == country.Name)
                    throw new ContinentException("The name of a country must be unique within a continent");
            }
            Countries.Add(country);
        }
        public void SetCountries(List<Country> countries)
        {
            Countries = countries;
        }
        private List<Country> Countries { get; set; } = new List<Country>();
        public IReadOnlyList<Country> GetCountries()
        {
            return Countries.AsReadOnly();
        }
    }
}
