using System.ComponentModel.DataAnnotations;

namespace BrewTask.DBCore.Entities
{
    public class BrewRatingsEntity
    {
        [Key]
        public int Id { get; set; }
        public double? RatingCount { get; set; }
        public int BrewId { get; set; }
    }
}
