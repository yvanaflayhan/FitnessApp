namespace API.Entities
{
    public class TrainerRequest
    {
        public int Id {get; set;}

        public int UserId {get; set;}
        public AppUser user {get; set;}

        //Personal Information
        public int? Age {get; set;}
        public string? Gender {get; set;}
        public string? Phone {get; set;}
        public double? Height {get; set;}
        public double? Weight {get; set;}
        public string? ImageUrl {get; set;}

        //Professional Information
        public string Specialization {get; set;} = string.Empty;
        public int? YearsOfExperience {get; set;}
        public string? Skills {get; set;}

        //Workplace 
        public int? GymId {get; set;}
        public Gym? Gym {get; set;}
        public string? OtherGymName {get; set;}
        public bool WorksIndependently {get; set;}

        //About the trainer
        public string? Description {get; set;}

        //Documents
        public string? CvUrl {get; set;}

        //Request Status 
        public string Status {get; set;} = "Pending";

    }
}