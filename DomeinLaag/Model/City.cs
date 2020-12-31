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
            Capital = capital;
            Country = country;
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
        public int Population { get { return _Population; } set { _Population = value; } }
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
                    _Country = value;
                    if (Capital)
                        _Country.AddCapital(this);
                    else
                        _Country.AddCity(this);
                }
            }
        }
        public bool Capital { get; private set; }
    }
}
