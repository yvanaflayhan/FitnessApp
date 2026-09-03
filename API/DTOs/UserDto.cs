namespace API.DTOs
{
    public class UserDto
    {
        public string Username {get; set;}

        public string Token {get; set;}
        public string Role {get; set;}
        public double? Latitude {get; set;}
        public double? Longitude {get; set;}
        public string? Location {get; set;}
        

    }
}