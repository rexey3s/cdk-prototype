using RosenCDK.Entities;
using RosenCDK.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Migrations.SeedData
{
    class RosenCDKTrainingProgramCreator
    {
        private RosenCDKDbContext _context;
        private ModuleType classRoomType;
        private ModuleType OnlineType;
        private TrainingStatus PlannedStatus;
        private TrainingStatus OngoingStatus;
        private TrainingStatus CompletedStatus;

        public RosenCDKTrainingProgramCreator(RosenCDKDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateModuleTypesAndTrainingStatus();
            CreateModulesAndTrainingProgram();
        }

        private void CreateModuleTypesAndTrainingStatus()
        {
            classRoomType = _context.ModuleTypes.Add(new ModuleType()
            {
                TypeName = "Classroom"
            });
            OnlineType = _context.ModuleTypes.Add(new ModuleType()
            {
                TypeName = "Online"
            });
            PlannedStatus = _context.TrainingStatuses.Add(new TrainingStatus()
            {
                StatusName = "Planned"
            });
            OngoingStatus = _context.TrainingStatuses.Add(new TrainingStatus()
            {
                StatusName = "Canceled"
            });
            OngoingStatus = _context.TrainingStatuses.Add(new TrainingStatus()
            {
                StatusName = "Ongoing"
            });
            CompletedStatus = _context.TrainingStatuses.Add(new TrainingStatus()
            {
                StatusName = "Completed"
            });
            _context.SaveChanges();
        }

        private void CreateModulesAndTrainingProgram()
        {
            Person kathleen = _context.People.Where(user => user.Username == "kathleen.kennedy").FirstOrDefault();
            Person hubert = _context.People.Where(user => user.Username == "hubert.farnsworth").FirstOrDefault();
            Person simpson = _context.People.Where(user => user.Username == "simpson.lizzie").FirstOrDefault();
            Person doug = _context.People.Where(user => user.Username == "doug.chiang").FirstOrDefault();
            Person david = _context.People.Where(user => user.Username == "david.evans").FirstOrDefault();

            Module basicHtml = _context.Modules.Add(new Module()
            {
                TypeId = OnlineType.Id,
                AreaOfObjective = "Frontend Web Development",
                ArrayOfTrainingCompetencies = new int[] { 6 },
                Title = "Basic knowledge HTML5/CCS3",
                Objectives = "Able to setup a web template using HTML5/CCS3",
                TopicsCovered = "HTTP Request, HyperText Markup Lang., Sketch UI",
                Exercises = "View url",
                Theory = 3.0,
                Pratical = 10.0,
                ReferencesDoc = "View url",
                Methods = "Lecture, exercises, Learn by examples",
                RoomOrEquipment = "Everyone use their own laptop/computer",
                ExamInclude = true,
                LearningTransfer = "Teamwork, discussion board, attend the course, reading references",
                TargetGroup = "FrontEnd Developer, Fullstack Developer",
                PersonId = david.Id
            });
            _context.SaveChanges();

            Module basicJS = _context.Modules.Add(new Module()
            {
                TypeId = classRoomType.Id,
                AreaOfObjective = "Frontend Web Development",
                ArrayOfTrainingCompetencies = new int[] { 1,18,6},
                Title = "Basic JavaScript",
                Objectives = "Basic Knowledge in JavaScript",
                TopicsCovered = "JavaScript Interperter, JSON, Jquery",
                Exercises = "View url",
                Theory = 5.0,
                Pratical = 15.0,
                ReferencesDoc = "View url",
                Methods = "Lecture, Learn by examples",
                RoomOrEquipment = "Everyone brings their own laptop",
                ExamInclude = true,
                LearningTransfer = "Teamwork, discussion board, attend the course, reading references",
                TargetGroup = "FrontEnd Developer, Fullstack Developer",
                PersonId = kathleen.Id
            });
            _context.SaveChanges();

            Module angularJS = _context.Modules.Add(new Module()
            {
                TypeId = classRoomType.Id,
                AreaOfObjective = "Frontend Web Development",
                ArrayOfTrainingCompetencies = new int[] {18, 2,7},
                Title = "AngularJS",
                Objectives = "Can design a client Web UI using AngularJS",
                TopicsCovered = "MVC/MVVM models, Dependency Injection, ",
                Exercises = "View url",
                Theory = 10.0,
                Pratical = 20.0,
                ReferencesDoc = "View url",
                Methods = "Lecture, Team Project, Learn by examples",
                RoomOrEquipment = "Everyone brings their own laptop or provided",
                ExamInclude = true,
                LearningTransfer = "Teamwork, discussion board, attend the course, reading references",
                TargetGroup = "FrontEnd Developer, Fullstack Developer",
                PersonId = david.Id
            });
            _context.SaveChanges();

            Module reactJS = _context.Modules.Add(new Module()
            {
                TypeId = classRoomType.Id,
                AreaOfObjective = "Cross-platform/Web Development",
                ArrayOfTrainingCompetencies = new int[] {18, 2, 6 },
                Title = "ReactJS",
                Objectives = "Building mordern UI using ReactJS",
                TopicsCovered = "React Pattern, Dependency Injection, Data Flow",
                Exercises = "View url",
                Theory = 10.0,
                Pratical = 20.0,
                ReferencesDoc = "View url",
                Methods = "Lecture, TeamProject Learn by examples",
                RoomOrEquipment = "Everyone brings their own laptop",
                ExamInclude = true,
                LearningTransfer = "Teamwork, discussion board, attend the course, reading references",
                TargetGroup = "FrontEnd/Mobile Developer, Fullstack Developer",
                PersonId = hubert.Id
            });
            _context.SaveChanges();

            Module javaEE = _context.Modules.Add(new Module()
            {
                TypeId = classRoomType.Id,
                AreaOfObjective = "Backend/Enteprise Development",
                ArrayOfTrainingCompetencies = new int[] { 3,9, 10},
                Title = "Java Enteprise Environment",
                Objectives = "Be able to build an application at enteprise level using JavaEE Technology",
                TopicsCovered = "Design Patterns,ORM, Java EE, NLayered Architecture",
                Exercises = "View url",
                Theory = 20.0,
                Pratical = 30.0,
                ReferencesDoc = "View url",
                Methods = "Lectures, TeamProject Learn by examples",
                RoomOrEquipment = "Everyone brings their own laptop",
                ExamInclude = true,
                LearningTransfer = "Teamwork, discussion board, attend the course, reading references",
                TargetGroup = "Backend/Enteprise Developer",
                PersonId = hubert.Id
            });
            _context.SaveChanges();

            Module aspNet = _context.Modules.Add(new Module()
            {
                TypeId = classRoomType.Id,
                AreaOfObjective = "Backend/Enteprise Development",
                ArrayOfTrainingCompetencies = new int[] { 3, 8, 11 },
                Title = "Java Enteprise Environment",
                Objectives = "Be able to build an application at enteprise level using ASP.NET Technology",
                TopicsCovered = "Design Patterns,ORM, ASP.NET, NLayered Architecture ",
                Exercises = "View url",
                Theory = 20.0,
                Pratical = 30.0,
                ReferencesDoc = "View url",
                Methods = "Lectures, TeamProject Learn by examples",
                RoomOrEquipment = "Everyone brings their own laptop",
                ExamInclude = true,
                LearningTransfer = "Teamwork, discussion board, attend the course, reading references",
                TargetGroup = "Backend/Enteprise Developer",
                PersonId = hubert.Id
            });
            _context.SaveChanges();

            Module basicProgramming = _context.Modules.Add(new Module()
            {
                TypeId = OnlineType.Id,
                AreaOfObjective = "Basic programming knowledges",
                ArrayOfTrainingCompetencies = new int[] { 1,18,16 },
                Title = "Programming 101",
                Objectives = "Be able to ",
                TopicsCovered = "Problem solving using programming paradigm, Object-Orient Programming",
                Exercises = "View url",
                Theory = 10.0,
                Pratical = 10.0,
                ReferencesDoc = "View url",
                Methods = "Lectures, Exam, Learn by examples",
                RoomOrEquipment = "Everyone brings their own laptop",
                ExamInclude = true,
                LearningTransfer = "Teamwork, discussion board, attend the course, reading references",
                TargetGroup = "Fresher,Beginner ",
                PersonId = kathleen.Id
            });
            Module agileModule = _context.Modules.Add(new Module()
            {
                TypeId = classRoomType.Id,
                AreaOfObjective = "Software Development Process",
                ArrayOfTrainingCompetencies = new int[] { 5, 17,16,19 },
                Title = "Agile Software Developement",
                Objectives = "Be able to ",
                TopicsCovered = "Requirement analysis, team management, agile, Scrum",
                Exercises = "View url",
                Theory = 10.0,
                Pratical = 5.0,
                ReferencesDoc = "View url",
                Methods = "Lectures, Presentation/Teamwork , Learn by examples",
                RoomOrEquipment = "Teamwork",
                ExamInclude = true,
                LearningTransfer = "Teamwork, discussion board, attend the course, reading references",
                TargetGroup = "Backend/Enteprise Developer, Software Architect",
                PersonId = doug.Id
            });
            _context.SaveChanges();

            Module uxDev = _context.Modules.Add(new Module()
            {
                TypeId = classRoomType.Id,
                AreaOfObjective = "UI/UX Design",
                ArrayOfTrainingCompetencies = new int[] { 18, 17, 19 },
                Title = "Agile Software Developement",
                Objectives = "Be able to ",
                TopicsCovered = "Requirement analysis, team management, agile, Scrum",
                Exercises = "View url",
                Theory = 10.0,
                Pratical = 5.0,
                ReferencesDoc = "View url",
                Methods = "Lectures, Presentation/Teamwork , Learn by examples",
                RoomOrEquipment = "Teamwork",
                ExamInclude = true,
                LearningTransfer = "Teamwork, discussion board, attend the course, reading references",
                TargetGroup = "Backend/Enteprise Developer, Software Architect",
                PersonId = doug.Id
            });
            _context.SaveChanges();

            Program basicFrontEnd = _context.Programs.Add(new Program() {
                ProgramTitle = "Basic Frontend Web Dev",
                ArrayOfIncludedModules = new int[] { basicHtml.Id, basicJS.Id},
                NeedByPotentialTrainees = "4"

            });
            _context.SaveChanges();
            Program advanceFrontEnd1 = _context.Programs.Add(new Program()
            {
                ProgramTitle = "Advanced Frontend Web Dev",
                ArrayOfIncludedModules = new int[] { basicProgramming.Id, angularJS.Id },
                NeedByPotentialTrainees = ""

            });
            Program advanceFrontEnd2 = _context.Programs.Add(new Program()
            {
                ProgramTitle = "Advanced Frontend Web Dev",
                ArrayOfIncludedModules = new int[] { basicProgramming.Id, reactJS.Id },
                NeedByPotentialTrainees = ""

            });
            Program eSoftwareDevArchitect = _context.Programs.Add(new Program()
            {
                ProgramTitle = "Enteprise Software Architecture",
                ArrayOfIncludedModules = new int[] { javaEE.Id, agileModule.Id },
                NeedByPotentialTrainees = ""

            });
            Program entepriseDev = _context.Programs.Add(new Program()
            {
                ProgramTitle = "Enteprise Software Dev Technologies",
                ArrayOfIncludedModules = new int[] { javaEE.Id, aspNet.Id },
                NeedByPotentialTrainees = "1,3"

            });

            Program uxUi = _context.Programs.Add(new Program()
            {
                ProgramTitle = "Desgin UI by User Experience Approach",
                ArrayOfIncludedModules = new int[] { basicHtml.Id, uxDev.Id },
                NeedByPotentialTrainees = "1,3"

            });
            _context.SaveChanges();

            Training basicFrontEndTraining = _context.Trainings.Add(new Training()
            {
                ProgramId = basicFrontEnd.Id,
                StatusId = PlannedStatus.Id,
                StartDate = new DateTime(2017,04,01),
                EndDate = new DateTime(2017,06,01),
                TotalDuration = 50, 
                ArrayOfAssignedTrainees = new int[] {1},
                ArrayOfAssignedTrainers = new int[] {1},
                ModuleArrangement = "[{moduleID: "+basicHtml.Id+", trainTime: 5},{moduleID: "+basicJS.Id+", trainTime: 8}]"
            });
            _context.SaveChanges();

            Training advanceFrontEndTraining = _context.Trainings.Add(new Training()
            {
                ProgramId = advanceFrontEnd1.Id,
                StatusId = PlannedStatus.Id,
                StartDate = new DateTime(2017, 04, 01),
                EndDate = new DateTime(2017, 06, 01),
                TotalDuration = 100,
                ArrayOfAssignedTrainees = new int[] {4},
                ArrayOfAssignedTrainers = new int[] {1},
                ModuleArrangement = "[{moduleID: " + basicProgramming.Id + ", trainTime: 5},{moduleID: " + angularJS.Id + ", trainTime: 8}]"
            });
            _context.SaveChanges();


            Training advanceFrontEndTraining2 = _context.Trainings.Add(new Training()
            {
                ProgramId = advanceFrontEnd2.Id,
                StatusId = OngoingStatus.Id,
                StartDate = new DateTime(2017, 03, 01),
                EndDate = new DateTime(2017, 05, 01),
                TotalDuration = 100,
                ArrayOfAssignedTrainees = new int[] {1},
                ArrayOfAssignedTrainers = new int[] {2},
                ModuleArrangement = "[{moduleID: " + basicProgramming.Id + ", trainTime: 5},{moduleID: " + reactJS.Id + ", trainTime: 8}]"
            });
            _context.SaveChanges();

            Training eSoftwareDevArchitectTraining = _context.Trainings.Add(new Training()
            {
                ProgramId = eSoftwareDevArchitect.Id,
                StatusId = PlannedStatus.Id,
                StartDate = new DateTime(2017, 04, 01),
                EndDate = new DateTime(2017, 10, 15),
                TotalDuration = 150,
                ArrayOfAssignedTrainees = new int[] {6},
                ArrayOfAssignedTrainers = new int[] {3},
                ModuleArrangement = "[{moduleID: " + javaEE.Id + ", trainTime: 8},{moduleID: " + agileModule.Id + ", trainTime: 5}]"
            });
            _context.SaveChanges();

            Training entepriseDevTraining = _context.Trainings.Add(new Training()
            {
                ProgramId = entepriseDev.Id,
                StatusId = OngoingStatus.Id,
                StartDate = new DateTime(2017, 03, 01),
                EndDate = new DateTime(2017, 10, 15),
                TotalDuration = 70,
                ArrayOfAssignedTrainees = new int[] {1},
                ArrayOfAssignedTrainers = new int[] {2},
                ModuleArrangement = "[{moduleID: " + javaEE.Id + ", trainTime: 8},{moduleID: " + aspNet.Id + ", trainTime: 8}]"
            });
            _context.SaveChanges();

            Training uxTraining = _context.Trainings.Add(new Training()
            {
                ProgramId = uxUi.Id,
                StatusId = PlannedStatus.Id,
                StartDate = new DateTime(2017, 04, 01),
                EndDate = new DateTime(2017, 08, 15),
                TotalDuration = 90,
                ArrayOfAssignedTrainees = new int[] {5},
                ArrayOfAssignedTrainers = new int[] {3},
                ModuleArrangement = "[{moduleID: " + basicHtml.Id + ", trainTime: 5},{moduleID: " + uxDev.Id + ", trainTime: 8}]"
            });
            _context.SaveChanges();

            Entities.Configuration MaximumHoursPerDay = _context.Configurations.Add(new Entities.Configuration()
            {
                Name = "MaximumHoursPerDay", Value = "8"
            });
            Entities.Configuration DaysOff = _context.Configurations.Add(new Entities.Configuration()
            {
                Name = "DaysOff",
                Value = "[0,6]"
            });
            Entities.Configuration MaintenanceTime = _context.Configurations.Add(new Entities.Configuration()
            {
                Name = "MaintenanceTime",
                Value = "01"
            });
            _context.SaveChanges();
        }
    }
}
