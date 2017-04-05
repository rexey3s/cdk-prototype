namespace RosenCDK.DTO
{
    public class UpdateTrainingInputDTO
    {
        public UpdateTrainingInputDTO()
        {
            
        }
       
        public int TrainingID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double TotalDuration { get; set; }
        public string AssignedTrainees { get; set; }
        public string AssignedTrainers { get; set; }
        public string ModulesArrangement { get; set; }
    }
}
