namespace API.Entities
{
    public class TrainerRequest
    {
        public int Id {get; set;}
        public int UserId {get; set;}
        public string Specialization {get; set;} = string.Empty;
        public string? Description {get; set;}
        public int? YearsOfExperience {get; set;}
        public string Status {get; set;} = "Pending";
    }
}