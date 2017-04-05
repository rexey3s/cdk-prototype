using System.Collections.Generic;

namespace RosenCDK.DTO
{
    public class TraineeDTO 
    {
        public TraineeDTO()
        {
            
        }

        public TraineeDTO(int id, int personId, string name, string defaultDepartment, List<ShortJobFunctionDTO> jobFunctions, List<ShortCompetenceDTO> competencies, List<ShortTrainingDTO> targetedForProgram, List<ShortTrainingDTO> trainingAttended)
        {
            Id = id;
            PersonId = personId;
            Name = name;
            DefaultDepartment = defaultDepartment;
            JobFunctions = jobFunctions;
            Competencies = competencies;
            TargetedForProgram = targetedForProgram;
            TrainingAttended = trainingAttended;
        }

        public int Id { get; set; }

        public int PersonId { get; set; }
        public string Name { get; set; }
        public string DefaultDepartment { get; set; }

        public List<ShortJobFunctionDTO> JobFunctions { get; set; }

        public List<ShortCompetenceDTO> Competencies { get; set; }

        public List<ShortTrainingDTO> TargetedForProgram { get; set; }

        public List<ShortTrainingDTO> TrainingAttended { get; set; }
    }
    
}
