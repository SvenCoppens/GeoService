using DomeinLaag.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DomeinLaag.Model
{
    public class River
    {
        public River(string name, int lengthInKm, List<Country> countries)
        {
            Name = name;
            LengthInKm = lengthInKm;
            SetCountries(countries);
        }

        public void SetCountries(List<Country> countries)
        {
            if (countries == null || countries.Count < 1)
                throw new RiverException("Een rivier behoort minstens tot een land.");
            else
            {
                
                if (countries.Count == countries.Distinct().Count())
                {
                    foreach (Country c in Countries)
                    {
                        c.RemoveRiver(this);
                    }
                    Countries = new List<Country>();
                    foreach (Country c in countries)
                    {
                        Countries.Add(c);
                        c.AddRiver(this);
                    }
                }
                else throw new RiverException("The list of countries contained doubles.");
            }
        }
        public ReadOnlyCollection<Country> GetCountries()
        {
            return Countries.AsReadOnly();
        }
        private List<Country> Countries { get; set; } = new List<Country>();
        public int Id { get; set; }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new RiverException("A river's name can not be null or empty.");
                else _Name = value;
            }
        }
        private int _LengthInKm;
        public int LengthInKm
        {
            get { return _LengthInKm; }
            set
            {
                if (value < 1)
                    throw new RiverException("A river's length must be longer than 0");
                else _LengthInKm = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is River)
            {
                River o = obj as River;
                return o.Name == Name && o.LengthInKm == LengthInKm && o.GetCountries().SequenceEqual(GetCountries());
            }
            else return false;
        }
    }
}
