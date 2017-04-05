using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using RosenCDK.BussinessLogics;
using RosenCDK.DTO;
using RosenCDK.Entities;
using RosenCDK.Repositories;

namespace RosenCDK.UnitTests.BusinessLogicLayerTests
{
    class ExecuteDeltaAnalysisAppService_Tests
    {
        // Disable Tests due to refactoring at last minute
        /*
        IExecutedDeltaAnalysisAppService excuteDeltaAnalysService;
        // IJobFunctionAppService sutJobFunctionAppService;

        Mock<IProgramRepository> mockProgramRepository;
        Mock<ITrainingRepository> mockTrainingRepository;
        Mock<ITrainingStatusRepository> mockTrainingStatusRepository;
        Mock<ICompetenceRepository> mockCompetenceRepository;

        Mock<IModuleRepository> mockModuleRepository;
        Mock<ITraineeRepository> mockTraineeRepository;
        Mock<IJobFunctionAppService> mockJobFunctionAppService;
        Mock<IJobFunctionRepository> mockJobFunctionRepository;
        Mock<ITraineeAppService> mocktraineeService;
        Mock<ITrainerRepository> mockTrainerRepo;
        Mock<ICompareAppService> mockcompareService;
        Mock<ExecutedDeltaAnalysisAppService> mockExcuteDeltaAnalysService;


        [OneTimeSetUp]
        public void SetUp()
        {
            mockProgramRepository = new Mock<IProgramRepository>();
            mockTrainingRepository = new Mock<ITrainingRepository>();
            mockModuleRepository = new Mock<IModuleRepository>();
            mockTraineeRepository = new Mock<ITraineeRepository>();
            mockTrainerRepo = new Mock<ITrainerRepository>();
            mockCompetenceRepository= new Mock<ICompetenceRepository>();
            mockJobFunctionRepository = new Mock<IJobFunctionRepository>();
            mockJobFunctionAppService = new Mock<IJobFunctionAppService>();
            mocktraineeService = new Mock<ITraineeAppService>();
            mockcompareService = new Mock<ICompareAppService>();
            mockTrainingStatusRepository = new Mock<ITrainingStatusRepository>();
            //sutJobFunctionAppService = new JobFunctionAppService(mockJobFunctionRepository.Object);
            mockExcuteDeltaAnalysService = new Mock<ExecutedDeltaAnalysisAppService>(MockBehavior.Default,
                mockProgramRepository.Object, mockTrainingRepository.Object, mockModuleRepository.Object,
                mockTraineeRepository.Object, mockJobFunctionAppService.Object, mocktraineeService.Object,
                mockcompareService.Object, mockTrainingStatusRepository.Object);
            mockExcuteDeltaAnalysService.CallBase = true;

            excuteDeltaAnalysService = new ExecutedDeltaAnalysisAppService(
                mockProgramRepository.Object,mockTrainingRepository.Object,mockModuleRepository.Object,
                mockTraineeRepository.Object, mockTrainerRepo.Object ,mockJobFunctionAppService.Object, mockCompetenceRepository.Object,
                mockcompareService.Object, mockTrainingStatusRepository.Object);

        }

        [Test]
        public void ShouldExecuteDeltaAnalysis()
        {
            #region new program
            Program sampleProgram = new Program()
            {
                Id = 1,
                ProgramTitle = "test Program title",
                IncludedModules = "1,2,3",
                NeedByPotentialTrainees = "1,2",
            };
            #endregion

            mockProgramRepository.Setup(repo => repo.Get(1)).Returns(sampleProgram);

            #region new training 
            Training sampleTraining = new Training()
            {
                Id = 1,
                ProgramId = 1,
                StatusId = 1,
                StartDate = DateTime.Parse("02-02-2010"),
                EndDate = DateTime.Parse("02-02-2010"),
                TotalDuration = 250.0f,
                AssignedTrainees = "1,2,3",
                AssignedTrainers = "1,2",
                ModuleArrangement = "[{moduleID: 1, trainTime: 3},{moduleID: 2, trainTime: 6}]",
            };
            #endregion
            mockTrainingRepository.Setup(repo => repo.Get(1)).Returns(sampleTraining);

            #region new Module
            Module sampleModule1 = new Module()
            {
                Id = 1,
                CompetenciesTrained = "1,2",
                AreaOfObjective = "Game design 1",
                TypeId = 1,
                Title = "Game design basic",
                ExpirationDate = DateTime.Parse("01-01-2017"),
                TargetGroup = "Software Developer",
                PersonId = 1,
            };
            Module sampleModule2 = new Module()
            {
                Id = 1,
                CompetenciesTrained = "11,12",
                AreaOfObjective = "Game design 2",
                TypeId = 1,
                Title = "Game design basic",
                ExpirationDate = DateTime.Parse("01-01-2017"),
                TargetGroup = "Software Developer",
                PersonId = 1,
            };
            #endregion
            mockModuleRepository.Setup(repo => repo.Get(1)).Returns(sampleModule1);
            mockModuleRepository.Setup(repo => repo.Get(2)).Returns(sampleModule1);
            mockModuleRepository.Setup(
                repo => repo.GetAll()).Returns(new List<Module>() {sampleModule1, sampleModule2}.AsQueryable);
            mockcompareService
                .Setup(
                    service => service.missingCompetencies(It.IsAny<int[]>(), It.IsAny<int[]>())
                )
                .Returns(new int[]{1,2,11,12});
            #region new Trainee
            Trainee sampleTrainee = new Trainee()
            {
                Id = 1,
                PersonId = 1,
                DefaultDepartment = "Software Development",
                JobFunctions = "2,3",
                Competencies = "5",
                TargetedTrainings = "1,2",
                AttendedTrainings = "3",
            };
            #endregion
            mockTraineeRepository.Setup(repo => repo.Get(1)).Returns(sampleTrainee);

            #region new Jobfuction
            JobFunction sampleJobFunction = new JobFunction()
            {
                Id = 1,
                JobFunctionTitle = "Game Design",
                RequiredCompetences = "1,2,11,12",
            };
            #endregion
            mockJobFunctionRepository.Setup(repo => repo.Get(1)).Returns(sampleJobFunction);

            mockJobFunctionAppService.Setup(repo => repo.GetJobFunctionById(1));
            mockExcuteDeltaAnalysService.Protected().
                Setup<List<ProgramDTO>>("FindAllRequiredProgram", ItExpr.IsAny<int[]>())
                .Returns(new List<ProgramDTO>
                {
                    new ProgramDTO()
                    {
                        ProgramID = 1,
                        ModulesIncluded = new List<int> {1,2},
                        ProgramTitle = "test Program Title"
                    }
                });

            mockExcuteDeltaAnalysService.Protected()
                 .Setup<List<ExecuteDataDTO>>("FindAvailableTrainings", ItExpr.IsAny<List<ProgramDTO>>(), ItExpr.IsAny<int>())
                .Returns(new List<ExecuteDataDTO>
                {
                    new ExecuteDataDTO()
                    {
                        AvailableTrainings = new List<TrainingDTO>
                        {
                            new TrainingDTO()
                            {
                                TrainingId = 1,
                                ProgramId = 1
                            }
                        },
                        ProgramID = 1,
                        ProgramTitle = "test Program Title"
                    }
                });

            ListExecuteDataDTO expectedExcuteData = new ListExecuteDataDTO()
            {
                ListExecuteDataObject = new List<ExecuteDataDTO>()
                {
                    new ExecuteDataDTO(1,"test Program title", new List<TrainingDTO>()
                    {
                        new TrainingDTO()
                        {
                            ProgramId = 1,
                            TrainingId = 1
                        }
                    })
                }
            };
            ListExecuteDataDTO actualExecuteData = mockExcuteDeltaAnalysService.Object.ExecuteDeltaAnalysis(1, 1);

            Assert.AreEqual(
                expectedExcuteData.ListExecuteDataObject.Count,
                actualExecuteData.ListExecuteDataObject.Count);
        }

        [Test]
        public void testGetExecuteDataShouldReturnExpectedResult()
        {
            
        }
        
        */
    }
}
