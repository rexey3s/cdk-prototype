using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RosenCDK.Entities;

namespace RosenCDK.DTO
{
    public class TrainingDTO 
    {
        public TrainingDTO()
        {
            
        }


        public TrainingDTO(int trainingId, int programId, string programName, int maxHoursPerDay, TrainingStatus status, 
            DateTime startDate, DateTime endDate, double totalDuration, List<ShortTraineeDetailDTO> traineesAssigned, 
            List<ShortTrainerDetailDTO> trainersAssigned, List<FullModuleArrangementDTO> moduleArrangement)
        {
            TrainingId = trainingId;
            ProgramId = programId;
            ProgramName = programName;
            MaxHoursPerDay = maxHoursPerDay;
            Status = status;
            StartDate = startDate;
            EndDate = endDate;
            TotalDuration = totalDuration;
            TraineesAssigned = traineesAssigned;
            TrainersAssigned = trainersAssigned;
            ModuleArrangement = moduleArrangement;
        }

        public int TrainingId { get; set; }

        public int ProgramId { get; set; }

        public string ProgramName { get; set; }

        public int MaxHoursPerDay { get; set; }
        public TrainingStatus Status { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalDuration { get; set; }

        public List<ShortTraineeDetailDTO> TraineesAssigned { get; set; }

        public List<ShortTrainerDetailDTO> TrainersAssigned { get; set; }

        public List<FullModuleArrangementDTO> ModuleArrangement { get; set; }

        public bool CanRegister { get; set; }
    }
}
