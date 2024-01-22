using System.ComponentModel.DataAnnotations;

namespace BrewTask.DBCore.Entities
{
    public class BrewsEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double? AverageRating { get; set; }
    }
}
