using System;
using System.Collections.Generic;

namespace RosenCDK.DTO
{
    public class ModuleDTO 
    {
        public ModuleDTO()
        {
            
        }

        public ModuleDTO(int moduleId, List<int> competenciesTrained, string areaOfObjective, 
            int typeId, string title, string objectives, string topicsCovered, string exercises,
            double theory, double pratical, string methods, string referencesDoc, bool examInclude, 
            string roomOrEquipment, string learningTransfer, DateTime? expirationDate, string targetGroup, int personId)
        {
            ModuleID = moduleId;
            this.competenciesTrained = competenciesTrained;
            AreaOfObjective = areaOfObjective;
            TypeId = typeId;
            Title = title;
            Objectives = objectives;
            TopicsCovered = topicsCovered;
            Exercises = exercises;
            Theory = theory;
            Pratical = pratical;
            Methods = methods;
            ReferencesDoc = referencesDoc;
            ExamInclude = examInclude;
            RoomOrEquipment = roomOrEquipment;
            LearningTransfer = learningTransfer;
            ExpirationDate = expirationDate;
            TargetGroup = targetGroup;
            PersonId = personId;
        }

        public int ModuleID { get; set; }

        public List<int> competenciesTrained { get; set; }

        public string AreaOfObjective { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Objectives { get; set; }
        public string TopicsCovered { get; set; }
        public string Exercises { get; set; }
        public double Theory { get; set; }
        public double Pratical { get; set; }
        public string Methods { get; set; }
        public string ReferencesDoc { get; set; }
        public bool ExamInclude { get; set; }
        public string RoomOrEquipment { get; set; }
        public string LearningTransfer { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string TargetGroup { get; set; }
        public int PersonId { get; set; }
    }
}
