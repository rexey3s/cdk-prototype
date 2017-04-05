using System.Collections.Generic;

namespace RosenCDK.DTO
{
    public class ProgramDTO 
    {
        public ProgramDTO()
        {
            
        }
        public ProgramDTO(int programId, string programTitle, List<int> modulesIncluded, List<int> needByPotentialTrainees)
        {
            ProgramID = programId;
            ProgramTitle = programTitle;
            ModulesIncluded = modulesIncluded;
            NeedByPotentialTrainees = needByPotentialTrainees;
        }

        public int ProgramID { get; set; }

        public string ProgramTitle { get; set; }

        public List<int> ModulesIncluded { get; set; }

        public List<int> NeedByPotentialTrainees { get; set; }

    }
}
