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
    public class JobFunctionAppserviceTests
    {
        
        IJobFunctionAppService sutJobFunctionService;
        Mock<IJobFunctionRepository> mockFunctionRepository;

        [OneTimeSetUp]
        public void SetUp()
        {
            mockFunctionRepository = new Mock<IJobFunctionRepository>();
            sutJobFunctionService = new JobFunctionAppService(mockFunctionRepository.Object);
        }

        [Test]
        public void GetAllJobFunctionsShoudReturnAll()
        {
            //Arrange
            JobFunction a = new JobFunction()
            {
                Id = 1,
                JobFunctionTitle = "title1",
                RequiredCompetences = "1,2,11",
            };
            JobFunction b = new JobFunction()
            {
                Id = 2,
                JobFunctionTitle = "title2",
                RequiredCompetences = "1,2,11",
            };
            JobFunction c = new JobFunction()
            {
                Id = 3,
                JobFunctionTitle = "title3",
                RequiredCompetences = "1,2,11",
            };
            List<JobFunction> jobfunctionList = new List<JobFunction>() { a, b, c };

            //Mock
            mockFunctionRepository.Setup(repo => repo.GetAllList()).Returns(jobfunctionList);



            //Act
            ListJobFunctionDTO output = sutJobFunctionService.GetAllJobFunctions();
            bool result = true;

            for (int i = 1; i < 3; i++)
            {
                if (jobfunctionList[i].Id != output.JobFunctionList[i].JobFunctionID)
                {
                    result = false;
                    break;
                }
            }

            //Assert
            Assert.AreEqual(true, result);

        }
        [Test]
        public void GetJobFunctionByIdShoudReturnSameJobfunction()
        {
            //Arrange
            JobFunction a = new JobFunction()
            {
                Id = 1,
                JobFunctionTitle = "title1",
                RequiredCompetences = "1,2,11",
            };
            JobFunction b = new JobFunction()
            {
                Id = 2,
                JobFunctionTitle = "title2",
                RequiredCompetences = "1,2,11",
            };
            JobFunction c = new JobFunction()
            {
                Id = 3,
                JobFunctionTitle = "title3",
                RequiredCompetences = "1,2,11",
            };
            List<JobFunction> jobfunctionList = new List<JobFunction>() { a, b, c };

            //Mock
            mockFunctionRepository.Setup(repo => repo.FirstOrDefault(1)).Returns(a);
            mockFunctionRepository.Setup(repo => repo.FirstOrDefault(2)).Returns(b);
            mockFunctionRepository.Setup(repo => repo.FirstOrDefault(3)).Returns(c);



            //Act
            JobFunctionDTO output = sutJobFunctionService.GetJobFunctionById(2);

            //Assert
            Assert.AreEqual("title2", output.JobFunctionTitle);

        }
        [Test]
        public void GetJobFunctionByIdShoudReturnNull()
        {
            //Arrange
            JobFunction a = new JobFunction()
            {
                Id = 1,
                JobFunctionTitle = "title1",
                RequiredCompetences = "1,2,11",
            };
            JobFunction b = new JobFunction()
            {
                Id = 2,
                JobFunctionTitle = "title2",
                RequiredCompetences = "1,2,11",
            };
            JobFunction c = new JobFunction()
            {
                Id = 3,
                JobFunctionTitle = "title3",
                RequiredCompetences = "1,2,11",
            };
            List<JobFunction> jobfunctionList = new List<JobFunction>() { a, b, c };

            //Mock
            mockFunctionRepository.Setup(repo => repo.FirstOrDefault(1)).Returns(a);
            mockFunctionRepository.Setup(repo => repo.FirstOrDefault(2)).Returns(b);
            mockFunctionRepository.Setup(repo => repo.FirstOrDefault(3)).Returns(c);



            //Act
            JobFunctionDTO output = sutJobFunctionService.GetJobFunctionById(10);

            //Assert
            Assert.Null(output);
            //Assert.IsNull(output.JobFunctionTitle); //bug when id is out of range, please fix it

        }
        
    }
}
