namespace API.DTOs
{
    public class TrainerDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? Skills { get; set; }
        public string? Phone { get; set; }
        public string? ImageUrl { get; set; }

        public bool IsApproved { get; set; }
    }
}
