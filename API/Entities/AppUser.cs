
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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
    }
}