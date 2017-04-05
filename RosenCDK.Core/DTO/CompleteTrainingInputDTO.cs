namespace RosenCDK.DTO
{
    public class CompleteTrainingInputDTO
    {
        public CompleteTrainingInputDTO(int trainingId, string traineeTrainingOutcome)
        {
            TrainingId = trainingId;
            TraineeTrainingOutcome = traineeTrainingOutcome;
        }

        public int TrainingId { get; set; }

        public string TraineeTrainingOutcome { get; set; }
    }
}
