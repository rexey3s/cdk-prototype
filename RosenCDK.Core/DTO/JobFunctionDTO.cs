using System.Collections.Generic;

namespace RosenCDK.DTO
{
    public class JobFunctionDTO 
    {
        public JobFunctionDTO()
        {
        }

        public JobFunctionDTO(int jobFunctionId, string jobFunctionTitle, List<int> requiredCompetencies)
        {
            JobFunctionID = jobFunctionId;
            JobFunctionTitle = jobFunctionTitle;
            RequiredCompetencies = requiredCompetencies;
        }

        public int JobFunctionID { get; set; }

        public string JobFunctionTitle { get; set; }

        public List<int> RequiredCompetencies { get; set; }

    }
}
