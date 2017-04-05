using System.Collections.Generic;

namespace RosenCDK.DTO
{
    public class FullTraineeDetailDTO
    {
        public int TraineeID { get; set; }
        public string Name { get; set; }
        public string DefaultDepartment { get; set; }
        public List<ShortJobFunctionDTO> JobFunctions { get; set; }
        public List<CompetenceDTO> Competencies { get; set; }
        public List<TrainingDTO> TargetedForTrainings { get; set; }
        public List<TrainingDTO> TrainingsAttended { get; set; }
    }
}
