namespace API.Entities
{
    public class TrainerGym
    {
        public int TrainerId { get; set; }
        public int GymId { get; set; }
        public Trainer Trainer { get; set; } = null!;
        public Gym Gym { get; set; } = null!;
    }
}
