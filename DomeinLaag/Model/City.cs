using DomeinLaag.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Model
{
    public class City
    {



        public City(string name, int population, Country country, bool capital)
        {
            Name = name;
            Population = population;
            Country = country;
            Capital = capital;
        }

        public int Id { get; set; }
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        private int _Population;
        public int Population
        {
            get { return _Population; }
            set
            {
                if (value < 1)
                    throw new CityException("The Population must be bigger than 0.");
                _Population = value;
            }
        }
        private Country _Country;
        public Country Country
        {
            get { return _Country; }
            set
            {
                if (value == null)
                    throw new CityException("A City has to have a Country");
                else
                {
                    Country oldCountry = Country;
                    _Country = value;
                    if (oldCountry != null)
                    {
                        oldCountry.RemoveCity(this);
                    }
                    Country.AddCity(this);
                    if (Capital)
                        Country.AddCapital(this);
                }
            }
        }
        private bool _Capital;
        public bool Capital
        {
            get { return _Capital; }
            set
            {
                bool oldValue = _Capital;
                _Capital = value;
                if (value == true&&oldValue==false)
                {
                    Country.AddCapital(this);
                }
                else if (value==false&&oldValue==true)
                {
                    Country.RemoveAsCapital(this);
                }
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is City)
            {
                City o = obj as City;
                return o.Name == Name && o.Population == Population && o.Capital == Capital;
            }
            else return false;
        }
    }
}
