using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosenCDK.DTO;
using RosenCDK.Repositories;
using RosenCDK.Entities;
using Abp.AutoMapper;
using AutoMapper;

namespace RosenCDK.BussinessLogics
{
    public class PersonAppService : RosenCDKAppServiceBase, IPersonAppService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IActivityRepository _activityRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly ITokenAppService _tokenAppService;
        private readonly IRoleDistributionRepository _roleDistributionRepository;

        public PersonAppService(IPersonRepository personRepo,
            IUserTokenRepository userTokenRepo, IActivityRepository activityRepo,
            ITokenAppService tokenService, IRoleDistributionRepository roleDistributionRepository)
        {
            _personRepository = personRepo;
            _activityRepository = activityRepo;
            _userTokenRepository = userTokenRepo;
            _tokenAppService = tokenService;
            _roleDistributionRepository = roleDistributionRepository;
        }

        public ResponseMessageDTO CheckAuthorizeActivity(string username, string activityName)
        {
            Person person = _personRepository.GetPersonByUsername(username);
            if (person == null)
            {
                return new ResponseMessageDTO(false, L("Unauthorized_Request_NoUser"));
            }
            List<RoleDistribution> roleDistributions = _roleDistributionRepository
                .GetAllList(roleDistribution => roleDistribution.RoleId == person.RoleId);
            bool isAuthorized =
                roleDistributions.Any(roleDistribution => roleDistribution.Activity.ActivityName == activityName);
            return isAuthorized ? new ResponseMessageDTO(isAuthorized,L("Authorized_Request_AccessGranted")) 
                : new ResponseMessageDTO(isAuthorized, L("NotAuthorized_ToExecuteAction"));

        }

        public LoginMessageOutputDTO CheckLoginCredential(LoginMessageInputDTO loginMessageInput)
        {
            var person = _personRepository.GetWithUsernameAndPassword(loginMessageInput.Username,
                loginMessageInput.Password);

            if (person == null)
            {
                return new LoginMessageOutputDTO(false,
                    new PersonCredentialDTO(L("Wrong_UsernameOrPassword"), "", null));
            }

            //If username and password are correct -> get user information

            #region Check the author token of user. If user already have token -> update token, else -> create a new token for this user

            PersonCredentialDTO personCredentialDto = new PersonCredentialDTO();
            UserTokenDTO userTokenDto = _tokenAppService.CheckUserTokenByUsername(loginMessageInput.Username);
            personCredentialDto.Name = person.Name;
            personCredentialDto.AuthToken = userTokenDto.AuthToken;


            #endregion

            #region Get list activities that user have permission to act with system

            if (person.RoleId.HasValue)
            {
                personCredentialDto.Activities = new List<string>();

                foreach (var roleDistribution in person.Role.RoleDistributions)
                {
                    personCredentialDto.Activities.Add(roleDistribution.Activity.ActivityName);
                }
            }


            #endregion

            return new LoginMessageOutputDTO(true, personCredentialDto);
        }

        public string Localize(string localizationPropertyName)
        {
            return L(LocalizationSourceName);
        }
    }
}
