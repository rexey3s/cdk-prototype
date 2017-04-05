using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.DTO
{
    public class FullModuleArrangementDTO
    {
        public FullModuleArrangementDTO(int moduleId, double trainTime, string moduleTitle, double totalDate, string duration, List<ShortCompetenceDTO> competences)
        {
            ModuleId = moduleId;
            TrainTime = trainTime;
            ModuleTitle = moduleTitle;
            TotalDate = totalDate;
            Duration = duration;
            Competences = competences;
        }

        public int ModuleId { get; set; }
        public double TrainTime { get; set; }

        public string ModuleTitle { get; set; }

        public double TotalDate { get; set; }

        public string Duration { get; set; }
        public List<ShortCompetenceDTO> Competences { get; set; }
    }
}
