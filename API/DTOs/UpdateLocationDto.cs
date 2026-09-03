namespace API.DTOs
{
    public class UpdateLocationDto
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
        public string? Location {get; set;}
    }
}