using System;
using System.Collections.Generic;
using System.Text;

namespace DataLaag.DataModel
{
    public class DataCountryRiver
    {
        public int CountryId { get; set; }
        public DataCountry Country { get; set; }
        public int RiverId { get; set; }
        public DataRiver River { get; set; }
    }
}
