namespace RosenCDK.DTO
{
    public class ShortTrainerDetailDTO
    {
        public ShortTrainerDetailDTO()
        {
            
        }
        public ShortTrainerDetailDTO(int trainerId, string name)
        {
            TrainerId = trainerId;
            Name = name;
        }

        public int TrainerId { get; set; }
        public string Name { get; set; }
    }
}
