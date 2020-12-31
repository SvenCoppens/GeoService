using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.Model
{
    public class ContinentDTOOut
    {
        public string ContinentId { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public string[] Countries { get; set; }
    }
}
