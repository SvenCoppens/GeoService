using System;
using System.Collections.Generic;
using System.Text;

namespace DataLaag.DataModel
{
    public class DataContinent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DataCountry> Countries { get; set; } = new HashSet<DataCountry>();
    }
}
