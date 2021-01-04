using DomeinLaag.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DomeinLaag.Model
{
    public class Continent
    {
        public Continent(string name)
        {
            Name = name;
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
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ContinentException("De naam mag niet leeg of null zijn.");
                else _Name = value;
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

        public void RemoveCountryFromContinent(Country country)
        {
            if (Countries.Contains(country))
                Countries.Remove(country);
            else throw new ContinentException($"the given country with id {country.Id} was not part of the continent with id {Id}");
        }
        /// <summary>
        /// this method should only be called from within the country object when it's Continent gets changed. It will do this automaticly.
        /// </summary>
        /// <param name="country"></param>
        public void AddCountryToContinent(Country country)
        {
            if (!country.Continent.Equals(this))
                throw new ContinentException("The continent of the country did not equal this continent");
            foreach(Country c in Countries)
            {
                if (c.Equals(country))
                    throw new ContinentException("This Country is already part of this continent");
                else if (c.Name == country.Name)
                    throw new ContinentException("The name of a country must be unique within a continent");
            }          
            Countries.Add(country);
        }
        public void SetCountries(List<Country> countries)
        {
            List<Country> newCountries = new List<Country>();
            foreach(Country c in countries)
            {
                newCountries.Add(c);
            }
            Countries = newCountries;
        }
        private List<Country> Countries { get; set; } = new List<Country>();
        public ReadOnlyCollection<Country> GetCountries()
        {
            return Countries.AsReadOnly();
        }
        public override bool Equals(object obj)
        {
            if (obj is Continent)
            {
                Continent o = obj as Continent;
                return o.Name == Name &&
                    o.Countries.SequenceEqual(Countries);
            }
            else return false;
        }
    }
}
