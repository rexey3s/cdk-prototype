using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosenCDK.DTO;
using RosenCDK.Repositories;
using RosenCDK.Entities;
using Newtonsoft.Json;

namespace RosenCDK.BussinessLogics
{
    public class TrainerAppService : RosenCDKAppServiceBase, ITrainerAppService
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IProgramRepository _programRepository;
        private readonly ICompareAppService _compareAppService;
        private readonly IPersonRepository _personRepository;

        public TrainerAppService(ITrainerRepository trainerRepo,
            IProgramRepository programRepo, ICompareAppService compareService, 
            IPersonRepository personRepo)
        {
            _trainerRepository = trainerRepo;
            _programRepository = programRepo;
            _compareAppService = compareService;
            _personRepository = personRepo;
        }

        public ListSuitableTrainerDTO GetSuitableTrainers(int programID)
        {
            try
            {
                ListSuitableTrainerDTO suitableTrainers = new ListSuitableTrainerDTO();

                List<ShortTrainerDetailDTO> listSuitableTrainers = new List<ShortTrainerDetailDTO>();

                #region Get list modules included to program

                //Get program by ProgramId
                var program = _programRepository.Get(programID);

                int[] modulesIncludedID = program.ArrayOfIncludedModules;

                #endregion

                #region Get all trainers in database, we will findout trainers who fit to train the list module above

                var trainers = _trainerRepository.GetAllIncluding(trainer => trainer.Person);

                foreach (Trainer trainer in trainers)
                {
                    int[] fitToTrainModules = trainer.ArrayOfSuitableModules;

                    if (_compareAppService.IsContainAny(fitToTrainModules, modulesIncludedID))
                    {
                        //This one is a suitable trainer                  
                        listSuitableTrainers.Add(new ShortTrainerDetailDTO(trainer.Id, trainer.Person.Name));
                    }
                }

                suitableTrainers.ListSuitableTrainers = listSuitableTrainers;

                #endregion

                return suitableTrainers;
            }
            catch
            {
                return null;
            }
        }
    }
}
