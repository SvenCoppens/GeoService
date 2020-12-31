using DomeinLaag.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Model
{
    public class Country
    {
        public Country(string name,int population,int surfaceArea,Continent continent)
        {
            Continent = continent;
            Name = name;
            Population = population;
            SurfaceArea = surfaceArea;
        }
        private int _Id;
        public int Id { 
            get { return _Id; } 
            set { if (value < 1) 
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
                else {
                    if (_Continent != null)
                    {
                        _Continent.RemoveCountryFromContinent(this);
                        _Continent = value;
                    }
                    else
                    {
                        _Continent = value;
                        _Continent.AddCountryToContinent(this);
                    }

                };
            }
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
                if (value<1)
                    throw new CountryException("The population of a country must be bigger than 0.");
                else _Population = value;
            }
        }
        private int _SurfaceArea;
        public int SurfaceArea {
            get { return _SurfaceArea; }
            set { if (value < 1)
                    throw new CountryException("The surface Area of a Country must be bigger than 0.");
                else
                {
                    _SurfaceArea = value;
                }
            } }


        public List<City> Capitals { get; set; } = new List<City>();
        public List<City> Cities { get; set; } = new List<City>();
        public IReadOnlyCollection<City> GetCities()
        {
            return Cities.AsReadOnly();
        }
        public IReadOnlyCollection<City> GetCapitals()
        {
            return Capitals.AsReadOnly();
        }
        public void AddCapital(City city)
        {
            if (Capitals.Contains(city))
                throw new CountryException("This capital is already part of this country");
            else
            {
                if (!Cities.Contains(city))
                    AddCity(city);
                Capitals.Add(city);
            }         
        }
        public void AddCity(City city)
        {
            if (Cities.Contains(city))
                throw new CountryException("This city is already part of this country");
            else
            {
                int total = 0;
                foreach(City c in Cities)
                {
                    total += c.Population;
                }
                total += city.Population;
                if (total > Population)
                    throw new CountryException("The population of the cities in a country can not be bigger than the Population of that country.");
                    Cities.Add(city);
            }
        }
        public void RemoveAsCapital(City city)
        {
            if (Capitals.Contains(city))
            {
                if (Capitals.Count > 1)
                {
                    Capitals.Remove(city);
                    Cities.Remove(city);
                }
                else throw new CountryException("A Country must always have at least 1 capital.");
            }
            else
            {
                throw new CountryException("This city was not a capital of this country");
            }
        }
        public void SetRivers(List<River> rivers)
        {
            Rivers = rivers;
        }
        
        public List<River> Rivers { get; set; } = new List<River>();
    }
}
