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
    public class TraineeAppService_Tests
    {
        
        ITraineeAppService sutTraineeService;
        Mock<ITraineeRepository> mockTraineeRepository;
        Mock<IJobFunctionRepository> mockJobFunctionRepository;
        Mock<ICompetenceRepository> mockCompetenceRepository;
        Mock<IProgramRepository> mockProgramRepository;
        Mock<ITrainingRepository> mockTrainingRepository;
        ICompareAppService sutCompareAppService;
        Mock<IModuleRepository> mockModuleRepository;
        Mock<IPersonRepository> mockPersonrepository;
        Mock<ITokenAppService> mockTokenAppService;

        Mock<ITrainerRepository> mockTrainerRepository;
        [OneTimeSetUp]
        public void SetUp()
        {
            mockTraineeRepository = new Mock<ITraineeRepository>();
            mockJobFunctionRepository = new Mock<IJobFunctionRepository>();
            mockCompetenceRepository = new Mock<ICompetenceRepository>();
            mockProgramRepository = new Mock<IProgramRepository>();
            mockTrainingRepository = new Mock<ITrainingRepository>();
            sutCompareAppService = new CompareAppService();
            mockModuleRepository = new Mock<IModuleRepository>();
            mockPersonrepository = new Mock<IPersonRepository>();
            mockTokenAppService = new Mock<ITokenAppService>();
            mockTrainerRepository = new Mock<ITrainerRepository>();
            sutTraineeService = new TraineeAppService(mockTraineeRepository.Object, mockTrainerRepository.Object,
                mockJobFunctionRepository.Object,mockCompetenceRepository.Object, mockProgramRepository.Object,
                mockTrainingRepository.Object, sutCompareAppService, mockModuleRepository.Object, mockPersonrepository.Object, mockTokenAppService.Object);
        }
        // Disable due to code-refactoring
        /*
        [Test]
        public void GetTraineeByIdShouldReturnATrainee()
        {
            //Arrange
            Person person1 = new Person()
            {
                Id = 1,
                Name = "trainer1",
                Company = "Rosen"
            };
            Trainee trainee = new Trainee()
            {
                Id = 1,
                PersonId = 1,
                Person = person1,
                DefaultDepartment = "Software Development",
                JobFunctions = "1",
                Competencies = "1",
                TargetedTrainings = "1",
                AttendedTrainings = "1"

            };

            //mock
            mockTraineeRepository.Setup(repo => repo.FirstOrDefault(1)).Returns(trainee);

            //assert
            Assert.AreEqual(trainee.PersonId, sutTraineeService.GetTraineeById(1).PersonId);
        }
        */
        [Test]
        public void GetAllTraineeShouldReturnAllTrainee()
        {
            //Arrange
            Person person1 = new Person()
            {
                Id = 1,
                Name = "trainer1",
                Company = "Rosen"
            };
            Trainee trainee1 = new Trainee()
            {
                Id = 1,
                PersonId = 1,
                Person = person1,
                DefaultDepartment = "Software Development",
                JobFunctions = "1",
                Competencies = "1",
                TargetedTrainings = "1",
                AttendedTrainings = "1"

            };
            Person person2 = new Person()
            {
                Id = 2,
                Name = "trainer2",
                Company = "Rosen"
            };
            Trainee trainee2 = new Trainee()
            {
                Id = 2,
                PersonId = 2,
                Person = person2,
                DefaultDepartment = "Software Development",
                JobFunctions = "1",
                Competencies = "1",
                TargetedTrainings = "1",
                AttendedTrainings = "1"

            };

            List<Trainee> expectList = new List<Trainee>() { trainee1, trainee2 };
            //mock
            mockTraineeRepository.Setup(repo => repo.GetAllList()).Returns(expectList);
            //act
            ListSuitableTraineeDTO output = sutTraineeService.GetAllTrainee();

            bool expected = true;
            for (int i = 0; i < 2; i++)
            {

                if (expectList[i].Id != output.ListSuitableTrainees[i].TraineeId)
                {

                    expected = false;
                    break;
                }
            }
            //assert
            Assert.AreEqual(true, expected);
        }
        
    }
}
