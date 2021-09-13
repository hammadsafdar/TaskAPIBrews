using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APITaskCore.Models
{
    public class Breweries
    {
        [Key]
        public int BrewId { get; set; }
        public string BrewName { get; set; }
        public string BrewType { get; set; }
        public string WebsiteURL { get; set; }
        public double AverageRating { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<Ratings> Ratings { get; set; } = new List<Ratings>();
    }
}
