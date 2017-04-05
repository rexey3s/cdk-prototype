using RosenCDK.BussinessLogics;

using RosenCDK.Repositories;

using Moq;

using NUnit.Framework;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using RosenCDK.Entities;

using RosenCDK.DTO;

using Abp.Modules;

using System.Diagnostics;
using System.Linq.Expressions;


namespace RosenCDK.UnitTests.BusinessLogicLayerTests

{

    [TestFixture]

    public class PersonAppService_Tests

    {
        
        IPersonAppService sutIPersonAppService;

        Mock<IPersonRepository> mockPersonRepository;

        Mock<IUserTokenRepository> mockUsertokenReposirity;

        Mock<IActivityRepository> mockActivityReposirity;

        ITokenAppService sutTokenAppservice;

        Mock<IRoleDistributionRepository> mockRoleDistributionRepository;

        PersonAppService sutPersonAppservice;

        [OneTimeSetUp]
        public void SetUp()

        {
            mockPersonRepository = new Mock<IPersonRepository>();
            mockUsertokenReposirity = new Mock<IUserTokenRepository>();
            mockActivityReposirity = new Mock<IActivityRepository>();
            sutTokenAppservice = new TokenAppService(mockUsertokenReposirity.Object);
            mockRoleDistributionRepository = new Mock<IRoleDistributionRepository>();
            sutIPersonAppService = new PersonAppService(mockPersonRepository.Object, mockUsertokenReposirity.Object,
            mockActivityReposirity.Object, sutTokenAppservice, mockRoleDistributionRepository.Object);
            sutPersonAppservice = new PersonAppService(mockPersonRepository.Object, mockUsertokenReposirity.Object,
            mockActivityReposirity.Object, sutTokenAppservice, mockRoleDistributionRepository.Object);
        }

        [Test]
        public void ShouldCheckAuthorizeActivityReturnTrue()
        {
            //Arrange
            Person person1 = new Person()
            {
                Id = 1,
                Name = "Judas Invisible",
                Username = "admin",
                Password = "123",
                RoleId = 1
            };

            Activity acti = new Activity()
            {
                Id = 1,
                ActivityName = "Hello",
            };

            RoleDistribution roledis = new RoleDistribution()
            {
                Id = 1,
                RoleId = 1,
                ActivityId = 1,
                Activity = acti
            };

            List<RoleDistribution> listroledis = new List<RoleDistribution>();

            listroledis.Add(roledis);

            UserToken usertok = new UserToken()
            {
                Id = 1,
                AuthToken = "asdasd",
                Username = "admin",
            };
            //Mock 
            mockPersonRepository.Setup(repo => repo.GetPersonByUsername("admin")).Returns(person1);
            mockRoleDistributionRepository.Setup(
                repo => repo.GetAllList(It.IsAny<Expression<Func<RoleDistribution, bool>>>())).Returns(listroledis);
            //Person person = mockPersonRepository.Object.GetPersonByUsername("admin");

            mockRoleDistributionRepository.Setup(repo => repo.GetAllList()).Returns(listroledis);
            //mockActivityReposirity.Setup(repo => repo.Get(1)).Returns(acti);

            //mockUsertokenReposirity.Setup(repo => repo.Get(1)).Returns(usertok);

            ResponseMessageDTO expect = new ResponseMessageDTO(true, "Authorized request");
            //Act

            ResponseMessageDTO output = sutPersonAppservice.CheckAuthorizeActivity("admin", "Hello");

            //ResponseMessageDTO repotest = sutIPersonAppService.CheckAuthorizeActivity("admin", "Hello");

            // Assert 

            Assert.AreEqual(expect.Status, output.Status);

        }
        

    }

}