using System;
using System.Collections.Generic;
using System.Text;

namespace DataLaag.DataModel
{
    public class DataRiver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public ICollection<DataCountryRiver> CountryLink { get; set; } = new HashSet<DataCountryRiver>();
    }
}
