using DomeinLaag.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DomeinLaag.Model
{
    public class Country
    {
        public Country(string name, int population, int surfaceArea, Continent continent)
        {
            Name = name;
            Population = population;
            SurfaceArea = surfaceArea;
            Continent = continent;
        }
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                if (value < 1)
                    throw new CountryException("De id moet groter zijn dan 0.");
                else { _Id = value; }
            }
        }
        //mag niet null zijn
        private Continent _Continent;
        public Continent Continent
        {
            get { return _Continent; }
            set
            {
                if (value == null)
                    throw new CountryException("Het continent van een land mag niet null zijn.");
                else
                {
                    if (_Continent != null)
                    {
                        _Continent.RemoveCountryFromContinent(this);
                    }
                    _Continent = value;
                    _Continent.AddCountryToContinent(this);
                }

            }
        }

        internal void AddRiver(River river)
        {
            Rivers.Add(river);
        }

        internal void RemoveRiver(River river)
        {
            Rivers.Remove(river);
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new CountryException("The name of a country can not be null or empty.");
                else _Name = value;
            }
        }
        private int _Population;
        public int Population
        {
            get { return _Population; }
            set
            {
                if (value < 1)
                    throw new CountryException("The population of a country must be bigger than 0.");
                else _Population = value;
            }
        }

        private int _SurfaceArea;
        public int SurfaceArea
        {
            get { return _SurfaceArea; }
            set
            {
                if (value < 1)
                    throw new CountryException("The surface Area of a Country must be bigger than 0.");
                else
                {
                    _SurfaceArea = value;
                }
            }
        }


        private List<City> Capitals { get; set; } = new List<City>();
        private List<City> Cities { get; set; } = new List<City>();
        public ReadOnlyCollection<City> GetCities()
        {
            return Cities.AsReadOnly();
        }
        public ReadOnlyCollection<City> GetCapitals()
        {
            return Capitals.AsReadOnly();
        }
        public void AddCapital(City city)
        {
            if (!city.Capital)
                throw new CountryException("The city was not a capital.");
            else if (Capitals.Contains(city))
                throw new CountryException("This city is already a capital of this country");
            else if (!Cities.Contains(city))
            {
                throw new CountryException("This city is not part of this Country");
            }
            
            Capitals.Add(city);
        }
        public void AddCity(City city)
        {
            if (Cities.Contains(city))
                throw new CountryException("This city is already part of this country");
            else if (city.Country != this)
                throw new CountryException("The country of the city did not equal this country.");
            else
            {
                //check on domainrule of the population of a country
                int total = 0;
                foreach (City c in Cities)
                {
                    total += c.Population;
                }
                total += city.Population;
                if (total > Population)
                    throw new CountryException("The population of the cities in a country can not be bigger than the Population of that country.");

                //manipulate the actual elements.
                Cities.Add(city);
            }
        }
        public void RemoveCity(City city)
        {
            Cities.Remove(city);
            Capitals.Remove(city);
        }
        public void RemoveAsCapital(City city)
        {
            if (Capitals.Contains(city))
            {
                    Capitals.Remove(city);
                    city.Capital = false;
            }
        }
        private List<River> Rivers { get; set; } = new List<River>();
        public ReadOnlyCollection<River> GetRivers()
        {
            return Rivers.AsReadOnly();
        }
        public override bool Equals(object obj)
        {
            if (obj is Country)
            {
                Country o = obj as Country;

                return Name == o.Name &&
                    Population == o.Population &&
                    SurfaceArea == o.SurfaceArea &&
                    Capitals.SequenceEqual(o.Capitals) &&
                    Cities.SequenceEqual(o.Cities);
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Population, SurfaceArea);
        }
    }
}
