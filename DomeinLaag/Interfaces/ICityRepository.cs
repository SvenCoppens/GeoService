using DomeinLaag.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomeinLaag.Interfaces
{
    public interface ICityRepository
    {
        City AddCity(City city);
        City GetCityForId(int id);
        City UpdateCity(City city);
        void DeleteCity(int cityId);
    }
}
