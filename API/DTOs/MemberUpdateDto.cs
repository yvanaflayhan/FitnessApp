using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class MemberUpdateDto
    {
        [Required]
        public string Username {get; set;}
        [Required]
        [RegularExpression("^(User|Admin)$")]
        public string Role {get; set;}
    }
}