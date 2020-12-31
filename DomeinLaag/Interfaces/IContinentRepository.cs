using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Interfaces
{
    public interface IContinentRepository
    {
        Continent AddContinent(Continent continent);
        Continent GetContinentForId(int id);
        Continent UpdateContinent(Continent continent);
        void DeleteContinent(int continentId);
    }
}
