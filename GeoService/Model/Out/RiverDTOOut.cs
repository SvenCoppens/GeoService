using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.Model
{
    public class RiverDTOOut
    {
        public string RiverId { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public string[] Countries { get; set; }
    }
}
