using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.Model
{
    public class RiverDTOIn
    {
        public int RiverId { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public int[] CountryIds { get; set; }
    }
}
