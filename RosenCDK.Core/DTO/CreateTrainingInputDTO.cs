namespace RosenCDK.DTO
{
    public class CreateTrainingInputDTO
    {
        public CreateTrainingInputDTO()
        {
            
        }
        public CreateTrainingInputDTO(int programId, string startDate, string endDate, double totalDuration, string assignedTrainees, string assignedTrainers, string modulesArrangement)
        {
            ProgramID = programId;
            StartDate = startDate;
            EndDate = endDate;
            TotalDuration = totalDuration;
            AssignedTrainees = assignedTrainees;
            AssignedTrainers = assignedTrainers;
            this.ModulesArrangement = modulesArrangement;
        }

        public int ProgramID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double TotalDuration { get; set; }   
        public string AssignedTrainees { get; set; }
        public string AssignedTrainers { get; set; }
        public string ModulesArrangement { get; set; }

    }
}
