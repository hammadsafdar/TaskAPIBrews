namespace BrewTask.Models.Dtos
{
    public class BrewsCreateRequestDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double? Rating { get; set; }
    }
}
