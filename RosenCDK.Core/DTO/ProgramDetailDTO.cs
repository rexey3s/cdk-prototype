using System.Collections.Generic;

namespace RosenCDK.DTO
{
    public class ProgramDetailDTO
    {
        public int ProgramId { get; set; }
        public string ProgramTitle { get; set; }
        public double TotalDuration { get; set; }
        public double MaxHoursPerDay { get; set; }
        public List<ShortModuleDetailDTO> ModulesIncluded { get; set; }
        public List<ShortTraineeDetailDTO> NeedByPotentialTrainees { get; set; }
    }
}
