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
    class ProgramAppService_Tests
    {
        
        IProgramAppService sutProgramAppService;
        Mock<IProgramRepository> mockProgramRepository;
        Mock<ITraineeRepository> mockTraineeRepository;
        Mock<IModuleRepository> mockModuleRepository;
        Mock<IConfigurationRepository> mockConfigurationRepository;
        Mock<ICompetenceRepository> mockCompetenceRepository;
        Mock<IPersonRepository> mockPersonRepository;
        [TestFixtureSetUp]
        public void Setup()
        {
            mockProgramRepository = new Mock<IProgramRepository>();
            mockTraineeRepository = new Mock<ITraineeRepository>();
            mockModuleRepository = new Mock<IModuleRepository>();
            mockConfigurationRepository = new Mock<IConfigurationRepository>();
            mockCompetenceRepository = new Mock<ICompetenceRepository>();
            mockPersonRepository = new Mock<IPersonRepository>();
            sutProgramAppService = new ProgramAppService(mockProgramRepository.Object,
                mockTraineeRepository.Object,
                mockModuleRepository.Object,
                mockConfigurationRepository.Object,
                mockCompetenceRepository.Object,
                mockPersonRepository.Object);


        }


        [Test]
        public void GetAllProramShoudReturnAllProgram()
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
            List<Program> programlist = new List<Program>() { prg1, prg2 };
            //mock
            mockProgramRepository.Setup(repo => repo.GetAll()).Returns(programlist.AsQueryable());
            //act
            bool result = true;
            ListProgramDTO output = sutProgramAppService.GetAllPrograms();
            for (int i = 0; i < programlist.Count; i++)
            {
                if (programlist[i].Id != output.ProgramList[i].ProgramID)
                {
                    result = false;
                    break;
                }
            }
            //Assert
            Assert.AreEqual(true, result);

        }
        [Test]
        public void GetProgramDetailByIDReturnTrue()
        {
            //Arrange
            Person person1 = new Person()
            {
                Id = 1,
                Name = "Per1",
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
                Name = "Per2",
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
            Person person3 = new Person()
            {
                Id = 3,
                Name = "Per3",
                Company = "Rosen"
            };
            Trainee trainee3 = new Trainee()
            {
                Id = 3,
                PersonId = 3,
                Person = person3,
                DefaultDepartment = "Software Development",
                JobFunctions = "1",
                Competencies = "1",
                TargetedTrainings = "1",
                AttendedTrainings = "1"

            };
            Program prg1 = new Program()
            {
                Id = 1,
                ProgramTitle = "test1",
                IncludedModules = "1",
                NeedByPotentialTrainees = "1,2,3"
            };
            Module module1 = new Module()
            {
                Id = 1,
                CompetenciesTrained = "2",
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
                ExpirationDate = DateTime.Parse("01-01-2017"),
                TargetGroup = "Software Developer",

            };
            Competence compe = new Competence()
            {
                Id = 2,
                Name = "3D Max Design Lvl1",
                Description = "Basic skills in coding and applying ...",
            };
            //mock
            mockPersonRepository.Setup(repo => repo.Get(1)).Returns(person1);
            mockPersonRepository.Setup(repo => repo.Get(2)).Returns(person2);
            mockPersonRepository.Setup(repo => repo.Get(3)).Returns(person3);
            mockProgramRepository.Setup(repo => repo.Get(1)).Returns(prg1);
            mockConfigurationRepository.Setup(repo => repo.GetMaximumHoursPerDay()).Returns(8);
            mockModuleRepository.Setup(repo => repo.GetModulesByArrayID(new int[] { 1 })).Returns(new List<Module>() { module1 });
            mockCompetenceRepository.Setup(repo => repo.GetCompetenciesByCompetenceID(new int[] { 2 })).Returns(new List<Competence>() { compe });
            mockTraineeRepository.Setup(repo => repo.getTraineesByArrayId(new int[] { 1, 2, 3 })).Returns(new List<Trainee> { trainee1, trainee2, trainee3 });

            //assert
            bool check = false;
            ProgramDetailDTO result = sutProgramAppService.GetProgramDetailByID(1);
            if (result.ProgramId == 1 && result.ProgramTitle == "test1" && result.TotalDuration == 9)
                check = true;
            Assert.AreEqual(true, check);

        }
        [Test]
        public void GetProgramDetailByIDReturnNull()
        {
            //Arrange
            Person person1 = new Person()
            {
                Id = 1,
                Name = "Per1",
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
                Name = "Per2",
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
            Person person3 = new Person()
            {
                Id = 3,
                Name = "Per3",
                Company = "Rosen"
            };
            Trainee trainee3 = new Trainee()
            {
                Id = 3,
                PersonId = 3,
                Person = person3,
                DefaultDepartment = "Software Development",
                JobFunctions = "1",
                Competencies = "1",
                TargetedTrainings = "1",
                AttendedTrainings = "1"

            };
            Program prg1 = new Program()
            {
                Id = 1,
                ProgramTitle = "test1",
                IncludedModules = "1",
                NeedByPotentialTrainees = "1,2,3"
            };
            Module module1 = new Module()
            {
                Id = 1,
                CompetenciesTrained = "2",
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
                ExpirationDate = DateTime.Parse("01-01-2017"),
                TargetGroup = "Software Developer",

            };
            Competence compe = new Competence()
            {
                Id = 2,
                Name = "3D Max Design Lvl1",
                Description = "Basic skills in coding and applying ...",
            };
            //mock
            mockPersonRepository.Setup(repo => repo.Get(1)).Returns(person1);
            mockPersonRepository.Setup(repo => repo.Get(2)).Returns(person2);
            mockPersonRepository.Setup(repo => repo.Get(3)).Returns(person3);
            mockProgramRepository.Setup(repo => repo.Get(1)).Returns(prg1);
            mockConfigurationRepository.Setup(repo => repo.GetMaximumHoursPerDay()).Returns(8);
            mockModuleRepository.Setup(repo => repo.GetModulesByArrayID(new int[] { 1 })).Returns(new List<Module>() { module1 });
            mockCompetenceRepository.Setup(repo => repo.GetCompetenciesByCompetenceID(new int[] { 2 })).Returns(new List<Competence>() { compe });
            mockTraineeRepository.Setup(repo => repo.getTraineesByArrayId(new int[] { 1, 2, 3 })).Returns(new List<Trainee> { trainee1, trainee2, trainee3 });

            //assert
            Assert.IsNull(sutProgramAppService.GetProgramDetailByID(-999)); //use wrong id

        }
        

    }
}
