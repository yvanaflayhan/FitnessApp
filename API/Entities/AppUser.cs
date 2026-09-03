
using System.ComponentModel.DataAnnotations;
namespace API.Entities
{
    public class AppUser
    {
        public int Id {get; set;}

        [Required]

        public string UserName {get; set;}

        public string FullName {get; set;} =string.Empty;

        public byte[] PasswordHash {get; set;}

        public byte[] PasswordSalt {get; set;}

        public string Role {get; set;} = "User";
        public double? Latitude {get; set;}
        public double? Longitude {get; set;}
        public string? Location {get; set;}
    }
}