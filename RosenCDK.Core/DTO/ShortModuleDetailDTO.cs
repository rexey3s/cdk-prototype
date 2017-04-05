using System.Collections.Generic;

namespace RosenCDK.DTO
{
    public class ShortModuleDetailDTO
    {
        public int ModuleId { get; set; }
        public string ModuleTitle { get; set; }
        public double Duration { get; set; }
        public double TrainTime { get; set; }
        public double TotalDate { get; set; }
        public List<CompetenceDTO> Competencies { get; set; }
    }
}
