using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Gym
    {
        public int Id {get; set;}
        [Required]
        public string Name {get; set;}
        [Required]
        public string Address {get; set;}
        public string Location { get; set;}
        public double Latitude {get; set;}
        public double Longitude {get; set;}
        public string? Phone {get; set;}
        public TimeSpan? OpeningHours {get; set;}
        public TimeSpan? ClosingHours {get; set;}
        public string? Description {get; set;}
        public string? ImageUrl {get; set;}
        public string? SocialMedia {get; set;}
        public decimal? MounthlyFee {get; set;}

    }
}