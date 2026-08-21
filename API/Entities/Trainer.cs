namespace API.Entities
{
    public class Trainer
    {
        public int Id {get; set;}
        public int userId {get; set;}
        public string Specialization {get; set;} = string.Empty;
        public string? Description {get; set;}
        public int? YearsOfExperience {get; set;}
        public string? Phone {get; set;}
        public string? ImageUrl {get; set;}
        public bool IsAproved {get; set;}
        public AppUser User {get; set;}
        public ICollection<TrainerGym> TrainerGyms {get; set;} = new List<TrainerGym>();

    }
}