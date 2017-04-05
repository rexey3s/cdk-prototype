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
    class TrainerAppService_Tests
    {
        
        ITrainerAppService sutTrainerAppService;

        ICompareAppService sutCompareAppService;
        Mock<ITrainerRepository> mockTrainerRepository;
        Mock<IProgramRepository> mockProgramRepository;
        //Mock<ICompareAppService> mockCompareAppService;
        Mock<IPersonRepository> mockPersonRepository;

        [OneTimeSetUp]
        public void SetUp()
        {

            mockTrainerRepository = new Mock<ITrainerRepository>();
            mockProgramRepository = new Mock<IProgramRepository>();
            //mockCompareAppService = new Mock<ICompareAppService>();
            sutCompareAppService = new CompareAppService();
            mockPersonRepository = new Mock<IPersonRepository>();

            sutTrainerAppService = new TrainerAppService(mockTrainerRepository.Object,
                mockProgramRepository.Object,
                //mockCompareAppService.Object,

                sutCompareAppService,
                mockPersonRepository.Object);


        }
        [Test]
        public void GetSuitableTrainersShouldReturnTrainers()
        {
            //Arrange
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
            Trainer trainer2 = new Trainer()
            {
                Id = 2,
                PersonId = 2,
                Person = person2,
                IsExternal = false,
                SuitableModules = "1,3"
            };


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
                ExpirationDate = System.DateTime.Parse("02/01/2017"),
                TargetGroup = "Software Developer",
                PersonId = 1,
            };
            //Mock
            mockProgramRepository.Setup(repo => repo.Get(1)).Returns(prg);

            List<Trainer> trnr = new List<Trainer>()
            {
                trainer1,
                trainer2
            };

            //mockTrainerRepository.Setup(repo => repo.GetAll()).Returns(trnr.AsQueryable());
            mockPersonRepository.Setup(repo => repo.Get(1)).Returns(person1);
            mockPersonRepository.Setup(repo => repo.Get(2)).Returns(person2);

            mockProgramRepository.Setup(repo => repo.Get(1)).Returns(prg);
            mockTrainerRepository.Setup(repo => repo.GetAllIncluding(trainer => trainer.Person)).Returns(trnr.AsQueryable());
            ShortTrainerDetailDTO t1 = new ShortTrainerDetailDTO()
            {
                TrainerId = 1,
                Name = "traner1"

            };
            ShortTrainerDetailDTO t2 = new ShortTrainerDetailDTO()
            {
                TrainerId = 2,
                Name = "traner2"

            };

            ListSuitableTrainerDTO expectedListSuitableTrainersObject = new ListSuitableTrainerDTO()
            {
                ListSuitableTrainers = new List<ShortTrainerDetailDTO>()
                {
                    t1,
                    t2
                }
            };


            //Act
            ListSuitableTrainerDTO ListSuitableTrainersObjectOutput = sutTrainerAppService.GetSuitableTrainers(1);

            bool flag = true;
            for (int i = 0; i < ListSuitableTrainersObjectOutput.ListSuitableTrainers.Count; i++)
            {

                if (ListSuitableTrainersObjectOutput.ListSuitableTrainers[i].TrainerId != expectedListSuitableTrainersObject.ListSuitableTrainers[i].TrainerId)
                {
                    flag = false;
                    break;
                }

            }

            //Assrest
            Assert.AreEqual(true, flag);
        }

    
    }
}
