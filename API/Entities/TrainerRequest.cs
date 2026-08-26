namespace API.Entities
{
    public class TrainerRequest
    {
        public int Id {get; set;}
        public int UserId {get; set;}
        public AppUser user {get; set;}
        public string Specialization {get; set;} = string.Empty;
        public string? Description {get; set;}
        public int? YearsOfExperience {get; set;}
        public int? GymId {get; set;}
        public Gym? Gym {get; set;}
        public string? OtherGymName {get; set;}
        public bool WorksIndependently {get; set;}
        public string Status {get; set;} = "Pending";

    }
}