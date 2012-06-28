﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateMyAmenity.Models
{
    public class Amenity
    {
        public int AmenityID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
        public string DefaultImage { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}