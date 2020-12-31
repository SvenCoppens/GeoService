using System;
using System.Collections.Generic;
using System.Text;

namespace DataLaag.DataModel
{
    public class DataCountry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public int Surface { get; set; }
        public int ContinentId { get; set; }
        public DataContinent Continent { get; set; }
        public ICollection<DataCountryRiver> RiverLink { get; set; } //= new HashSet<DataCountryRiver>();
        public ICollection<DataCity> Cities { get; set; } //= new HashSet<DataCity>();
    }
}
