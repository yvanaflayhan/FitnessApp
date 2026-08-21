namespace API.DTOs
{
    public class TrainerRequestDto
    {
        public string Specialization {get; set;} = string.Empty;
        public string? Description {get; set;}
        public int? YearsOfExperience {get; set;}
    }
}