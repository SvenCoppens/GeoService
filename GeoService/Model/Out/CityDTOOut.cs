﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.Model
{
    public class CityDTOOut
    {
        public string CityId { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public string Country { get; set; }
    }
}