﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseService.DbEntities
{
    public partial class SearchModel
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public partial class SearchPageModel
    {
        public string Type { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public bool? IsFeatured { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set;}
        public string PrimaryEmail { get; set;}
        public string PrimaryWebsite { get; set;}
        public string PrimaryPhone { get; set;}
        public string CompanyName { get; set; }
        public string Warranty { get; set; }
        public string PartNumber { get; set; }
        public decimal? Price { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public bool? IsVerified { get; set; }
        public string Image { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }


    }
}
