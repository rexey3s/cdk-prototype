using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using RosenCDK.BussinessLogics;
using RosenCDK.DTO;
using RosenCDK.Entities;
using RosenCDK.Repositories;

namespace RosenCDK.UnitTests.BusinessLogicLayerTests
{

    [TestFixture]
    class TrainingAppService_Tests
    {
     /*   
        ITrainingAppService trainingAppService;

        Mock<ITrainingRepository> mockTrainingRepository;
        Mock<IProgramRepository> mockProgramRepository;
        Mock<IModuleRepository> mockModuleRepository;
        Mock<IConfigurationRepository> mockConfigurationRepository;
        Mock<ICompetenceRepository> mockCompetenceRepository;

//        ICompareAppService sutCompareAppService;

        Mock<ICompareAppService> mockCompareAppService;

        Mock<ITraineeRepository> mockTraineeRepository;
        Mock<IPersonRepository> mockPersonRepository;
        Mock<ITrainerRepository> mockTrainerRepository;
        Mock<ITrainingStatusRepository> mockTrainingStatusRepository;

        [OneTimeSetUp]
        public void SetUp()
        {

            mockTrainingRepository = new Mock<ITrainingRepository>();
            mockProgramRepository = new Mock<IProgramRepository>();
            mockModuleRepository = new Mock<IModuleRepository>();
            mockConfigurationRepository = new Mock<IConfigurationRepository>();
            mockCompetenceRepository = new Mock<ICompetenceRepository>();

//            sutCompareAppService = new CompareAppService();
            mockTraineeRepository = new Mock<ITraineeRepository>();
            mockPersonRepository = new Mock<IPersonRepository>();
            mockTrainerRepository = new Mock<ITrainerRepository>();
            mockTrainingStatusRepository = new Mock<ITrainingStatusRepository>();
            mockCompareAppService = new Mock<ICompareAppService>();

            trainingAppService = new TrainingAppService(
                mockTrainingRepository.Object,
                mockProgramRepository.Object,
                mockModuleRepository.Object,
                mockConfigurationRepository.Object,
                mockCompetenceRepository.Object,
                mockCompareAppService.Object,
                mockTraineeRepository.Object,
                mockPersonRepository.Object,
                mockTrainerRepository.Object,
                mockTrainingStatusRepository.Object
                );
        }

        [Test]
        public void TestCalculateEndDateReturnTrueEndDate()
        {
            // Arrange
            Program prg = new Program()
            {
                Id = 1,
                ProgramTitle = "test",
                IncludedModules = "1",
                NeedByPotentialTrainees = "1,2,3"
            };

            Module module = new Module()
            {
                Id = 1,
                CompetenciesTrained = "1",
                AreaOfObjective = "Game design",
                TypeId = 1,
                Title = "Game design basic",
                Objectives = "Learn 3Dmax and basic design skill",
                TopicsCovered = "Step 1, step 2, step 3",
                Exercises = "Capture the event object, pass $event as a parameter in the event callback from the template",
                Theory = 10.0,
                Pratical = 20.0,
                Methods = "instructor-led training, supported by multimedia presentation",
                ReferencesDoc = "urlXYZ",
                ExamInclude = false,
                RoomOrEquipment = "beamer, access to QAS, pc / laptop",
                LearningTransfer = "training system, daily work, discussion board",
                ExpirationDate = System.DateTime.Parse("02/01/2017"),
                TargetGroup = "Software Developer",
                PersonId = 1,
            };

            Competence competence = new Competence()
            {
                Id = 1,
                Name = "competence test",
                Description = "test"
            };


            //mock
            mockProgramRepository.Setup(repo => repo.Get(1)).Returns(prg);
            mockModuleRepository.Setup(repo => repo.GetModulesByArrayID(new int[1] { 1 })).Returns(new List<Module> { module });
            mockConfigurationRepository.Setup(repo => repo.GetMaximumHoursPerDay()).Returns(8);
            mockCompetenceRepository.Setup(repo => repo.GetCompetenciesByCompetenceID(new int[1] { 1 })).Returns(new List<Competence>() { competence });
            mockConfigurationRepository.Setup(repo => repo.GetDaysOff()).Returns("[0,4]"); //fix this. Pls! when i return 0,4 it throw an exception
            mockCompareAppService
                .Setup(
                    service => service.CalculateEndDate(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<List<int>>())
                )
                .Returns("06/01/2017");

            EndDateMessageDTO expectedEndDateMessageDto =
                new EndDateMessageDTO()
                {
                    status = true,
                    message = "Calculated!",
                    endDate = "06/01/2017"

                };

            // Act
            EndDateMessageDTO actualEndDateMessageDto = trainingAppService.CalculaterEndDate(new CalculateEndDateInputDTO() { ProgramID = 1, StartDate = "02/01/2017" });

            // Assert
            Assert.AreEqual(expectedEndDateMessageDto.endDate, actualEndDateMessageDto.endDate);

        }

        [Test]
        public void TestCalculaterEndDateReturnFalse1()
        {
            // Arrange


            Program prg = new Program()
            {
                Id = 1,
                ProgramTitle = "test",
                IncludedModules = "1",
                NeedByPotentialTrainees = "1,2,3"
            };

            Module module = new Module()
            {
                Id = 1,
                CompetenciesTrained = "1",
                AreaOfObjective = "Game design",
                TypeId = 1,
                Title = "Game design basic",
                Objectives = "Learn 3Dmax and basic design skill",
                TopicsCovered = "Step 1, step 2, step 3",
                Exercises =
                    "Capture the event object, pass $event as a parameter in the event callback from the template",
                Theory = 10.0,
                Pratical = 20.0,
                Methods = "instructor-led training, supported by multimedia presentation",
                ReferencesDoc = "urlXYZ",
                ExamInclude = false,
                RoomOrEquipment = "beamer, access to QAS, pc / laptop",
                LearningTransfer = "training system, daily work, discussion board",
                ExpirationDate = System.DateTime.Parse("02/01/2017"),
                TargetGroup = "Software Developer",
                PersonId = 1,
            };

            Competence competence = new Competence()
            {
                Id = 1,
                Name = "competence test",
                Description = "test"
            };

            //mock
            mockProgramRepository.Setup(repo => repo.Get(1)).Returns(prg);
            mockModuleRepository.Setup(repo => repo.GetModulesByArrayID(new int[1] {1}))
                .Returns(new List<Module>() {module});
            mockConfigurationRepository.Setup(repo => repo.GetMaximumHoursPerDay()).Returns(8);
            mockCompetenceRepository.Setup(repo => repo.GetCompetenciesByCompetenceID(new int[1] {1}))
                .Returns(new List<Competence>() {competence});
            mockConfigurationRepository.Setup(repo => repo.GetDaysOff()).Returns("[0,4]");
                //fix this. Pls! when i return 0,4 it throw an exception

            mockCompareAppService
                .Setup(
                    service => service.CalculateEndDate(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<List<int>>())
                )
                .Returns("06/01/2017");
            //EndDateMessageDTO expectedEndDateMessageObject =
            //    new EndDateMessageDTO()
            //    {
            //        status = true,
            //        message = "Calculated!",
            //        endDate = "06/01/2017"

            //    };


            // Act
            EndDateMessageDTO EndDateMessageObjectOutput =
                trainingAppService.CalculaterEndDate(new CalculateEndDateInputDTO()
                {
                    ProgramID = 1,
                    StartDate = "35/01/2017"
                }); //use wrong date

            // Assert
            Assert.AreEqual(false, EndDateMessageObjectOutput.status);
        }

        [Test]
        public void TestCalculaterEndDateReturnFalse2()
        {
            // Arrange


            Program prg = new Program()
            {
                Id = 1,
                ProgramTitle = "test",
                IncludedModules = "1",
                NeedByPotentialTrainees = "1,2,3"
            };

            Module module = new Module()
            {
                Id = 1,
                CompetenciesTrained = "1",
                AreaOfObjective = "Game design",
                TypeId = 1,
                Title = "Game design basic",
                Objectives = "Learn 3Dmax and basic design skill",
                TopicsCovered = "Step 1, step 2, step 3",
                Exercises = "Capture the event object, pass $event as a parameter in the event callback from the template",
                Theory = 10.0,
                Pratical = 20.0,
                Methods = "instructor-led training, supported by multimedia presentation",
                ReferencesDoc = "urlXYZ",
                ExamInclude = false,
                RoomOrEquipment = "beamer, access to QAS, pc / laptop",
                LearningTransfer = "training system, daily work, discussion board",
                ExpirationDate = System.DateTime.Parse("02/01/2017"),
                TargetGroup = "Software Developer",
                PersonId = 1,
            };

            Competence competence = new Competence()
            {
                Id = 1,
                Name = "competence test",
                Description = "test"
            };

            //mock
            mockProgramRepository.Setup(repo => repo.Get(1)).Returns(prg);
            mockModuleRepository.Setup(repo => repo.GetModulesByArrayID(new int[1] { 1 })).Returns(new List<Module>() { module });
            mockConfigurationRepository.Setup(repo => repo.GetMaximumHoursPerDay()).Returns(8);
            mockCompetenceRepository.Setup(repo => repo.GetCompetenciesByCompetenceID(new int[1] { 1 })).Returns(new List<Competence>() { competence });
            mockConfigurationRepository.Setup(repo => repo.GetDaysOff()).Returns("[0,4]"); //fix this. Pls! when i return 0,4 it throw an exception


            //EndDateMessageDTO expectedEndDateMessageObject =
            //    new EndDateMessageDTO()
            //    {
            //        status = true,
            //        message = "Calculated!",
            //        endDate = "06/01/2017"

            //    };


            // Act
            EndDateMessageDTO EndDateMessageObjectOutput = trainingAppService.CalculaterEndDate(new CalculateEndDateInputDTO() { ProgramID = 999, StartDate = "35/01/2017" }); //use wrong ProgramID

            // Assert
            Assert.AreEqual(false, EndDateMessageObjectOutput.status);

        }

        [Test]
        public void TestCustomEndDateReturnTrue()
        {
            Program prg = new Program()
            {
                Id = 1,
                ProgramTitle = "test",
                IncludedModules = "1",
                NeedByPotentialTrainees = "1,2,3"
            };

            Module module = new Module()
            {
                Id = 1,
                CompetenciesTrained = "1,2,11,12",
                AreaOfObjective = "Game design",
                TypeId = 1,
                Title = "Game design basic",
                Objectives = "Learn 3Dmax and basic design skill",
                TopicsCovered = "Step 1, step 2, step 3",
                Exercises = "Capture the event object, pass $event as a parameter in the event callback from the template",
                Theory = 6.0,
                Pratical = 3.0,
                Methods = "instructor-led training, supported by multimedia presentation",
                ReferencesDoc = "urlXYZ",
                ExamInclude = false,
                RoomOrEquipment = "beamer, access to QAS, pc / laptop",
                LearningTransfer = "training system, daily work, discussion board",
                ExpirationDate = System.DateTime.Parse("01/01/2017"),
                TargetGroup = "Software Developer",
                PersonId = 1,
            };

            Competence competence = new Competence()
            {
                Id = 1,
                Name = "competence test",
                Description = "test"
            };

            Training b = new Training()
            {
                Id = 1,
                ProgramId = 1,
                StatusId = 1,
                StartDate = System.DateTime.Parse("1/1/2010"),
                EndDate = System.DateTime.Parse("25/1/2010"),
                TotalDuration = 250.0f,
                AssignedTrainees = "1,2,3",
                AssignedTrainers = "1,2",
                ModuleArrangement = "{moduleID: 1, trainTime: 8}", //error, i dont know how to fit it so that JsonConvert.DeserializeObject<ModuleArrangement[]>(customEndDateInput.ModulesArrangement).ToList()  work!
            };
            //mock
            mockProgramRepository.Setup(repo => repo.Get(1)).Returns(prg);
            mockModuleRepository.Setup(repo => repo.Get(1)).Returns(module);
            mockConfigurationRepository.Setup(repo => repo.GetMaximumHoursPerDay()).Returns(8);
            mockConfigurationRepository.Setup(repo => repo.GetDaysOff()).Returns("[0,4]");
            mockCompetenceRepository.Setup(repo => repo.GetCompetenciesByCompetenceID(new int[1] { 1 })).Returns(new List<Competence>() { competence });

            EndDateMessageDTO expected =
                new EndDateMessageDTO()
                {
                    status = true,
                    message = "Calculated!",
                    endDate = "06/01/2017"
                };


            // Act
            EndDateMessageDTO Output = trainingAppService.CustomEndDate(new CustomEndDateInputDTO() { ModulesArrangement = "[1]", StartDate = "2/1/2017" });

            // Assert
            Assert.AreEqual(expected.endDate, Output.endDate);
        }
        // Disable because the TrainingDTO structure was changed 
        /*
        [Test]
        public void GetAllTrainingShouldReturn()
        {
            //Arrange
            Program prg1 = new Program()
            {
                Id = 1,
                ProgramTitle = "test1",
                IncludedModules = "1",
                NeedByPotentialTrainees = "1,2,3"
            };
            Program prg2 = new Program()
            {
                Id = 2,
                ProgramTitle = "test2",
                IncludedModules = "1",
                NeedByPotentialTrainees = "1,2,3"
            };
            Training a = new Training()
            {
                Id = 1,
                ProgramId = 1,
                Programs = prg1,
                StatusId = 1,
                TrainingStatus = new TrainingStatus() { Id = 1, StatusName = "Planned" },
                StartDate = System.DateTime.Parse("1/1/2010"),
                EndDate = System.DateTime.Parse("25/1/2010"),
                TotalDuration = 250.0f,
                AssignedTrainees = "1,2,3",
                AssignedTrainers = "1,2",
                ModuleArrangement = "[{moduleID: 1, trainTime: 8}]", 
                //error, i dont know how to fit it so that JsonConvert.DeserializeObject<ModuleArrangement[]>(customEndDateInput.ModulesArrangement).ToList()  work!
            };
            Training b = new Training()
            {
                Id = 2,
                ProgramId = 2,
                Programs = prg2,
                StatusId = 1,
                TrainingStatus = new TrainingStatus() { Id = 1, StatusName = "Planned" },
                StartDate = System.DateTime.Parse("2/1/2010"),
                EndDate = System.DateTime.Parse("26/1/2010"),
                TotalDuration = 250.0f,
                AssignedTrainees = "1,2,3",
                AssignedTrainers = "1,2",
                ModuleArrangement = "{moduleID: 1, trainTime: 8}", //error, i dont know how to fit it so that JsonConvert.DeserializeObject<ModuleArrangement[]>(customEndDateInput.ModulesArrangement).ToList()  work!
            };


            List<Training> rtn = new List<Training> { a, b };
            ListTrainingDTO expected = new ListTrainingDTO()
            {
                TrainingList = new List<TrainingDTO>()
                {
                    new TrainingDTO
                    (
                        1,

                        1,
                        "test1",
                        new TrainingStatus() { Id = 1, StatusName = "Planned" },
                        System.DateTime.Parse("1/1/2010"),
                        System.DateTime.Parse("26/1/2010"),
                        250.0f,new List<int>() {1,2,3 },
                        new List<int>{1,2 },
                        new List<ModuleArrangementDTO>(){ }


                    ),
                    new TrainingDTO
                    (
                        1,

                        1,
                        "test2",
                        new TrainingStatus() { Id = 1, StatusName = "Planned" },
                        System.DateTime.Parse("1/1/2010"),
                        System.DateTime.Parse("26/1/2010"),
                        250.0f,new List<int>() {1,2,3 },
                        new List<int>{1,2 },
                        new List<ModuleArrangementDTO>(){ }


                    )
                }
            };
            //mock
            mockTrainingRepository.Setup(repo => repo.GetAllList()).Returns(rtn);

            //Act
            ListTrainingDTO output = trainingAppService.GetAllTrainings();



            bool check = true;



            for (int i = 0; i < output.TrainingList.Count; i++)
            {
                if (output.TrainingList[i].ProgramId != expected.TrainingList[i].ProgramId)
                {
                    check = false;
                    break;
                }
            }

            //Assert
            Assert.AreEqual(true, check);
        }
        

        [Test]
        public void GetTrainingDetailByIDShouldReturn()
        {
            //Arrange
            Program prg1 = new Program()
            {
                Id = 1,
                ProgramTitle = "test1",
                IncludedModules = "1",
                NeedByPotentialTrainees = "1,2,3"
            };

            Training a = new Training()
            {
                Id = 1,
                ProgramId = 1,
                Programs = prg1,
                StatusId = 1,
                TrainingStatus = new TrainingStatus() { Id = 1, StatusName = "Planned" },
                StartDate = System.DateTime.Parse("1/1/2010"),
                EndDate = System.DateTime.Parse("25/1/2010"),
                TotalDuration = 250.0f,
                AssignedTrainees = "1",
                AssignedTrainers = "1",
                ModuleArrangement = "{moduleID: 1, trainTime: 8}", //error, i dont know how to fit it so that JsonConvert.DeserializeObject<ModuleArrangement[]>(customEndDateInput.ModulesArrangement).ToList()  work!
            };
            Person person1 = new Person()
            {
                Id = 1,
                Name = "trainer1",
                Company = "Rosen"
            };
            Person person2 = new Person()
            {
                Id = 2,
                Name = "trainer2",
                Company = "Rosen"
            };
            Trainer trainer1 = new Trainer()
            {
                Id = 1,
                PersonId = 1,
                Person = person1,
                IsExternal = true,
                SuitableModules = "1,2"

            };
            Trainee trainee = new Trainee()
            {
                Id = 1,
                PersonId = 2,
                Person = person2,
                DefaultDepartment = "Software Development",
                JobFunctions = "1",
                Competencies = "1",
                TargetedTrainings = "1",
                AttendedTrainings = "1"
            };
            Module module = new Module()
            {
                Id = 1,
                CompetenciesTrained = "1,2,11,12",
                AreaOfObjective = "Game design",
                TypeId = 1,
                Title = "Game design basic",
                Objectives = "Learn 3Dmax and basic design skill",
                TopicsCovered = "Step 1, step 2, step 3",
                Exercises = "Capture the event object, pass $event as a parameter in the event callback from the template",
                Theory = 6.0,
                Pratical = 3.0,
                Methods = "instructor-led training, supported by multimedia presentation",
                ReferencesDoc = "urlXYZ",
                ExamInclude = false,
                RoomOrEquipment = "beamer, access to QAS, pc / laptop",
                LearningTransfer = "training system, daily work, discussion board",
                ExpirationDate = System.DateTime.Parse("02/01/2017"),
                TargetGroup = "Software Developer",
                PersonId = 1,
            };
            Competence competence = new Competence()
            {
                Id = 1,
                Name = "competence test",
                Description = "test"
            };
            //Mock
            mockTrainingRepository.Setup(repo => repo.Get(1)).Returns(a);

            mockProgramRepository.Setup(repo => repo.Get(1)).Returns(prg1);

            mockConfigurationRepository.Setup(repo => repo.GetMaximumHoursPerDay()).Returns(8);

            mockTraineeRepository.Setup(repo => repo.getTraineesByArrayId(new int[] { 1 })).Returns(new List<Trainee>() { trainee });
            mockTrainerRepository.Setup(repo => repo.GetTrainersByArrayId(new int[] { 1 })).Returns(new List<Trainer> { trainer1 });
            mockPersonRepository.Setup(repo => repo.Get(1)).Returns(person1);
            mockPersonRepository.Setup(repo => repo.Get(2)).Returns(person2);
            mockModuleRepository.Setup(repo => repo.Get(1)).Returns(module);
            mockCompetenceRepository.Setup(repo => repo.GetCompetenciesByCompetenceID(new int[1] { 1 })).Returns(new List<Competence>() { competence });

            //Act
            TrainingDetailDTO expected = new TrainingDetailDTO()
            {
                TrainingId = 1,
                ProgramId = 1,
                ProgramName = "test1",
                StatusId = 1,
                StatusName = "OnGoing",
                StartDate = "1/1/2010",
                EndDate = "25/1/2010",
                TotalDuration = 250,
                MaxHoursPerDay = 8
            };
            TrainingDetailDTO output = trainingAppService.GetTrainingDetailByID(1);
            //Assert
            Assert.AreEqual(expected.ProgramId, output.ProgramId);

        }
        */
    }
}
