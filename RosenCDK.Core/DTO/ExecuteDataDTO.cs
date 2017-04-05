using System.Collections.Generic;

namespace RosenCDK.DTO
{
    public class ExecuteDataDTO
    {
        public ExecuteDataDTO()
        {
            
        }
        public ExecuteDataDTO(int programId, string programTitle, List<TrainingDTO> availableTrainings)
        {
            ProgramID = programId;
            ProgramTitle = programTitle;
            AvailableTrainings = availableTrainings;
        }

        public int ProgramID { get; set; }
        public string ProgramTitle { get; set; }
        public List<TrainingDTO> AvailableTrainings { get; set; }
    }
}
