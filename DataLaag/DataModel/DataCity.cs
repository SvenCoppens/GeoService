using System;
using System.Collections.Generic;
using System.Text;

namespace DataLaag.DataModel
{
    public class DataCity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public int CountryId { get; set; }
        public DataCountry Country { get; set; }
        public Boolean Capital { get; set; }
    }
}
