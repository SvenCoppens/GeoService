using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.Model
{
    public class CityDTOIn
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public bool Capital { get; set; }
        public int Population { get; set; }
        public int CountryId { get; set; }
        public int ContinentId { get; set; }
    }
}
