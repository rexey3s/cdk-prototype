namespace RosenCDK.DTO
{
    public class ShortTraineeDetailDTO
    {
        public ShortTraineeDetailDTO()
        {
            
        }

        public ShortTraineeDetailDTO(int traineeId, string name, string defaultDepartment)
        {
            TraineeId = traineeId;
            Name = name;
            DefaultDepartment = defaultDepartment;
        }

        public int TraineeId { get; set; }
        public string Name { get; set; }
        public string DefaultDepartment { get; set; }
    }
}
