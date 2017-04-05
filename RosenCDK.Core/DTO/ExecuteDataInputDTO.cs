namespace RosenCDK.DTO
{
    public class ExecuteDataInputDTO
    {
        public ExecuteDataInputDTO()
        {
            
        }
        public ExecuteDataInputDTO(int jobFunctionId, int traineeId)
        {
            JobFunctionId = jobFunctionId;
            TraineeId = traineeId;
        }

        public int JobFunctionId { get; set; }
        public int TraineeId { get; set; }
    }
}
