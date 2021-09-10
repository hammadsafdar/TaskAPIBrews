using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APITaskCore.Models
{
    public class Ratings
    {
        [Key]
        public int RatingId { get; set; }
        public double AllRatings { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int BrewId { get; set; }
        public virtual Breweries Brewery { get; set; }

    }
}
