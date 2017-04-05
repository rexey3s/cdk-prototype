using System.Collections.Generic;

namespace RosenCDK.DTO
{
    public class TrainingDetailDTO
    {
        public int TrainingId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double TotalDuration { get; set; }
        public double MaxHoursPerDay { get; set; }
        public List<ShortTraineeDetailDTO> TraineesAssigned { get; set; }
        public List<ShortTrainerDetailDTO> TrainersAssigned { get; set; }
        public List<ShortModuleDetailDTO> ModulesArrangement { get; set; }
    }
}
