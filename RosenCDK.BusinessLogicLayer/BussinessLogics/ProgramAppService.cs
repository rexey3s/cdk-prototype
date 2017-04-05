using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosenCDK.DTO;
using RosenCDK.Repositories;
using RosenCDK.Entities;

namespace RosenCDK.BussinessLogics
{
    public class ProgramAppService : RosenCDKAppServiceBase, IProgramAppService
    {
        private readonly IProgramRepository _programRepository;
        private readonly ITraineeRepository _traineeRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly ICompetenceRepository _competenceRepository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IPersonRepository _personRepository;

        public ProgramAppService(IProgramRepository programRepo, 
            ITraineeRepository traineeRepo, IModuleRepository moduleRepo, 
            IConfigurationRepository configurationRepo, 
            ICompetenceRepository competenceRepo, IPersonRepository personRepo)
        {
            _programRepository = programRepo;
            _moduleRepository = moduleRepo;
            _traineeRepository = traineeRepo;
            _configurationRepository = configurationRepo;
            _competenceRepository = competenceRepo;
            _personRepository = personRepo;
        }

        public ListProgramDTO GetAllPrograms()
        {
            try
            {
                ListProgramDTO listProgramDTO = new ListProgramDTO();

                List<ProgramDTO> listPrograms = new List<ProgramDTO>();

                //Call a DAL method to  to get all Programs in database
                List<Program> programs = _programRepository.GetAll().ToList();

                foreach (var program in programs)
                {
                    ProgramDTO programDTO = new ProgramDTO(
                        program.Id,program.ProgramTitle,program.ArrayOfIncludedModules.ToList(),
                        program.ArrayOfNeedByPotentialTrainees.ToList());
                    listPrograms.Add(programDTO);
                }

                listProgramDTO.ProgramList = listPrograms;
                return listProgramDTO;
            }
            catch
            {
                return null;
            }
        }

        public ProgramDetailDTO GetProgramDetailByID(int programID)
        {
            try
            {
                ProgramDetailDTO programDetailDTO = new ProgramDetailDTO();

                //Call a DAL method to get program detail by id
                var program = _programRepository.Get(programID);

                programDetailDTO.ProgramId = program.Id;
                programDetailDTO.ProgramTitle = program.ProgramTitle;
                programDetailDTO.MaxHoursPerDay =  _configurationRepository.GetMaximumHoursPerDay();

                #region Get modules included to program

                List<ShortModuleDetailDTO> listModulesIncluded = new List<ShortModuleDetailDTO>();

                //Call a DAL method to get list module 
                List<Module> modules = _moduleRepository.GetModulesByArrayID(program.ArrayOfIncludedModules).ToList();

                foreach (Module module in modules)
                {
                    ShortModuleDetailDTO shortModule = new ShortModuleDetailDTO();

                    //Get id and TrainTime of module
                    shortModule.ModuleId = module.Id;
                    shortModule.TrainTime = _configurationRepository.GetMaximumHoursPerDay();
                    shortModule.ModuleTitle = module.Title;
                    shortModule.Duration = module.Theory + module.Pratical;

                    //Get TotalDate
                    if ((shortModule.Duration % shortModule.TrainTime) != 0)
                    {
                        double temp = shortModule.Duration / shortModule.TrainTime;
                        shortModule.TotalDate = Math.Truncate(temp) + 1;
                    }
                    else
                    {
                        shortModule.TotalDate = shortModule.Duration / shortModule.TrainTime;
                    }

                    //Get Competencies trained by this module
                    List<CompetenceDTO> listCompetenciesTrained = new List<CompetenceDTO>();
                    List<Competence> CompetenciesTrained =
                        _competenceRepository.GetCompetenciesByCompetenceID(module.ArrayOfTrainingCompetencies);

                    foreach (var competence in CompetenciesTrained)
                    {
                        CompetenceDTO competenceDto = new CompetenceDTO();

                        competenceDto.CompetenceID = competence.Id;
                        competenceDto.Name = competence.Name;
                        competenceDto.Description = competence.Description;

                        listCompetenciesTrained.Add(competenceDto);
                    }

                    shortModule.Competencies = listCompetenciesTrained;

                    listModulesIncluded.Add(shortModule);
                }

                programDetailDTO.ModulesIncluded = listModulesIncluded;

                #endregion

                #region Get potential trainees

                List<ShortTraineeDetailDTO> listPotentialTrainees = new List<ShortTraineeDetailDTO>();

                //Call a DAL method to get list potentail trainees
                List<Trainee> potentailTrainees = _traineeRepository.getTraineesByArrayId(program.ArrayOfNeedByPotentialTrainees);

                foreach (Trainee trainee in potentailTrainees)
                {
                    ShortTraineeDetailDTO Shotttrainee = new ShortTraineeDetailDTO();

                    Shotttrainee.TraineeId = trainee.Id;

                    var person = _personRepository.Get(trainee.PersonId);

                    Shotttrainee.Name = person.Name;

                    listPotentialTrainees.Add(Shotttrainee);
                }

                programDetailDTO.NeedByPotentialTrainees = listPotentialTrainees;

                #endregion

                #region Get total Duration

                double totalDuration = 0;

                foreach (ShortModuleDetailDTO module in programDetailDTO.ModulesIncluded)
                {
                    totalDuration += module.Duration;
                }

                programDetailDTO.TotalDuration = totalDuration;

                #endregion

                return programDetailDTO;
            }
            catch
            {
                return null;
            }
        }
    }
}
