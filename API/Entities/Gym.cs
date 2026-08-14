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
        public double Latitude {get; set;}
        public double Longitude {get; set;}
    }
}