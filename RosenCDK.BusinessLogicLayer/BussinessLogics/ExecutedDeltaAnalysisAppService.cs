using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosenCDK.DTO;
using RosenCDK.Repositories;
using RosenCDK.Entities;
using Newtonsoft.Json;
using RosenCDK.Utilities;

namespace RosenCDK.BussinessLogics
{
    public class ExecutedDeltaAnalysisAppService : RosenCDKAppServiceBase, IExecutedDeltaAnalysisAppService
    {
        private readonly IProgramRepository _programRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly ITraineeRepository _traineeRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IJobFunctionAppService _jobFunctionAppService;
        private readonly ICompetenceRepository _competenceRepository;
        private readonly ICompareAppService _compareAppService;
        private readonly ITrainingStatusRepository _trainingStatusRepository;
        public IConfigurationRepository ConfigurationRepository { get; set; }
        public ExecutedDeltaAnalysisAppService(IProgramRepository programRepo,
            ITrainingRepository trainingRepo, IModuleRepository moduleRepo,
            ITraineeRepository traineeRepo, ITrainerRepository trainerRepository,IJobFunctionAppService jobFunctionService,
            ICompetenceRepository competenceRepository, ICompareAppService compareService,
            ITrainingStatusRepository trainingStatusRepository)
        {
            _programRepository = programRepo;
            _trainingRepository = trainingRepo;
            _moduleRepository = moduleRepo;
            _traineeRepository = traineeRepo;
            _trainerRepository = trainerRepository;
            _jobFunctionAppService = jobFunctionService;
            _competenceRepository = competenceRepository;
            _compareAppService = compareService;
            _trainingStatusRepository = trainingStatusRepository;
        }

        public ListExecuteDataDTO ExecuteDeltaAnalysis(int jobFunctionId, int traineeId)
        {
            int[] missingCompetencies = GetExecuteData(jobFunctionId, traineeId);

            //Get array of programs required for missing Competencies
            List<ProgramDTO> requiredPrograms = FindAllRequiredProgram(missingCompetencies);

            //Get array of trainings curently open for there required programs
            List<ExecuteDataDTO> requiredTrainings = FindAvailableTrainings(requiredPrograms, traineeId);

            //Return list executed delta analysis
            ListExecuteDataDTO listExecutedDataObject = new ListExecuteDataDTO();
            listExecutedDataObject.ListExecuteDataObject = requiredTrainings;

            return listExecutedDataObject;
        }

        /// <summary>
        /// Get a array id of competencies that the Trainee are missing from Job Fucntion
        /// </summary>
        /// <param name="jobFunctionId">A jobFunctionId that identify a Job Function</param>
        /// <param name="traineeId">A traineeId that identify a Trainee</param>
        /// <returns>Array id of competencies that the Trainee are missing from Job Fucntion</returns>
        private int[] GetExecuteData(int jobFunctionId, int traineeId)
        {
            try
            {
                int[] missingCompetencies = null;

                //Get job function and trainee by there ids
                JobFunctionDTO targetJobFunction = _jobFunctionAppService.GetJobFunctionById(jobFunctionId);
                Trainee sourceTrainee = _traineeRepository.FirstOrDefault(traineeId);
                
                //compare the list of competence required by the job function with the list of 
                //competence gained by trainee

                missingCompetencies = _compareAppService.missingCompetencies(
                    sourceTrainee.ArrayOfCompetence, targetJobFunction.RequiredCompetencies.ToArray());

                return missingCompetencies;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get a list of Programs that required to train a Trainee
        /// </summary>
        /// <param name="missingCompetencies">A array id of competencies are missing from a specific Trainee</param>
        /// <returns>List of Programs</returns>
        protected virtual List<ProgramDTO> FindAllRequiredProgram(int[] missingCompetencies)
        {
            try
            {
                List<ProgramDTO> listProgram = new List<ProgramDTO>();

                #region Get all required module 
                List<ModuleDTO> requiredModuleDTOs = new List<ModuleDTO>();
                var modules = _moduleRepository.GetAll();
                foreach (Module module in modules.ToList())
                {
                    if (module.ArrayOfTrainingCompetencies.Intersect(missingCompetencies).Any())
                    {
                        ModuleDTO requiredModuleDTO = new ModuleDTO(
                            module.Id,module.ArrayOfTrainingCompetencies.ToList(),module.AreaOfObjective,
                            module.TypeId,module.Title,module.Objectives,module.TopicsCovered,module.Exercises,
                            module.Theory,module.Pratical,module.Methods,module.ReferencesDoc,
                            module.ExamInclude,module.RoomOrEquipment,module.LearningTransfer,module.ExpirationDate,
                            module.TargetGroup,module.PersonId);

                        requiredModuleDTOs.Add(requiredModuleDTO);
                    }
                }

                #endregion

                #region Get all program in database

                //The list that hold all the Program in database
                List<ProgramDTO> allProgramDTOs = new List<ProgramDTO>();

                var programs = _programRepository.GetAll();

                foreach (Program program in programs.ToList())
                {
                    ProgramDTO programDTO = new ProgramDTO(program.Id,program.ProgramTitle,
                        program.ArrayOfIncludedModules.ToList(),program.ArrayOfNeedByPotentialTrainees.ToList()
                    );

                    allProgramDTOs.Add(programDTO);
                }

                #endregion

                #region Get the list of Program included required Modules

                //The list that hold all Program included required Modules
                List<ProgramDTO> requiredPrograms = new List<ProgramDTO>();
                foreach (ProgramDTO program in allProgramDTOs)
                {
                    bool require = false;
                    foreach (ModuleDTO module in requiredModuleDTOs)
                    {
                        for (int i = 0; i < program.ModulesIncluded.ToArray().Length; i++)
                        {
                            if (module.ModuleID == program.ModulesIncluded[i])
                            {
                                require = true;
                                break;
                            }
                        }
                        if (require)
                        {
                            break;
                        }
                    }
                    if (require)
                    {
                        requiredPrograms.Add(program);
                    }
                }

                #endregion

                //Remove duplicate Programs in the requiredPrograms list and return it
                listProgram = requiredPrograms.GroupBy(program => program.ProgramID)
                                                .Select(program => program.First()).ToList();

                return listProgram;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get list of ExecuteDataDTO included Programs and available Trainings are already open for those Programs
        /// </summary>
        /// <param name="requiredPrograms">A list of Programs that required to get the list of Trainings</param>
        /// <param name="traineeID">A traineeID to check that the Trainee can registry to a specific Training or not</param>
        /// <returns>List of ExecuteDataDTO</returns>
        protected virtual List<ExecuteDataDTO> FindAvailableTrainings(List<ProgramDTO> requiredPrograms, int traineeID)
        {
            try
            {
                List<ExecuteDataDTO> temp = new List<ExecuteDataDTO>();

                #region Get a list of Training that already open with a specific list of Program

                foreach (ProgramDTO program in requiredPrograms)
                {
                    //The list that hold Training that already open with a specific Program
                    List<TrainingDTO> specTrainings = new List<TrainingDTO>();

                    #region Get the list of Training that already open with a specific ProgramId

                    var trainingsOfProgram = _trainingRepository.GetTrainingsByProgramIdAndStatusId(program.ProgramID, 
                        _trainingStatusRepository.GetTrainingStatusIdByName(TrainingStatus.PLANNED) );

                    foreach (Training training in trainingsOfProgram)
                    {
                        List<ShortTraineeDetailDTO> shortTraineeDetailDtos = new List<ShortTraineeDetailDTO>();
                        List<ShortTrainerDetailDTO> shortTrainerDetailDtos = new List<ShortTrainerDetailDTO>();
                        var trainees = _traineeRepository.getTraineesByArrayId(training.ArrayOfAssignedTrainees);
                        var trainers = _trainerRepository.GetTrainersByArrayId(training.ArrayOfAssignedTrainees);
                        trainees.ForEach(trainee => shortTraineeDetailDtos.Add(new ShortTraineeDetailDTO(trainee.Id, trainee.Person.Name, trainee.DefaultDepartment)));
                        trainers.ForEach(trainer => shortTrainerDetailDtos.Add(new ShortTrainerDetailDTO(trainer.Id, trainer.Person.Name)));
                       
                        TrainingDTO trainingDto = new TrainingDTO(
                            training.Id, training.ProgramId,  training.Programs.ProgramTitle, ConfigurationRepository.GetMaximumHoursPerDay(),
                            training.TrainingStatus, training.StartDate, training.EndDate, training.TotalDuration,
                            shortTraineeDetailDtos,
                            shortTrainerDetailDtos,
                            BuildFullModuleArrangementDTO(training.ModuleArrangement)
                        );

                        specTrainings.Add(trainingDto);
                    }

                    #endregion

                    #region Check if the Trainee can registry into a single Training or not

                    foreach (TrainingDTO training in specTrainings)
                    {
                        bool isCheck = true;

//                        for (int i = 0; i < training.TraineesAssigned.ToArray().Length; i++)
//                        {
//                            if (traineeID == training.TraineesAssigned[i])
//                            {
//                                isCheck = false;
//                                break;
//                            }
//                        }
                        foreach (var shortTraineeDetailDto in training.TraineesAssigned)
                        {
                            if (traineeID == shortTraineeDetailDto.TraineeId)
                            {
                                isCheck = false;
                                break;
                            }
                        }

                        training.CanRegister = isCheck;
                    }

                    #endregion

                    temp.Add(new ExecuteDataDTO(
                        program.ProgramID,
                        program.ProgramTitle,
                        specTrainings ));
                }

                #endregion

                return temp;
            }
            catch
            {
                return null;
            }
        }

        private List<FullModuleArrangementDTO> BuildFullModuleArrangementDTO(string moduleArrangementJsonString)
        {
            List<ModuleArrangementDTO> moduleArrangementDtos =
                           JsonConvert.DeserializeObject<ModuleArrangementDTO[]>(moduleArrangementJsonString).ToList();
            List<FullModuleArrangementDTO> fullModuleArrangementDtos = new List<FullModuleArrangementDTO>();
            moduleArrangementDtos.ForEach(moduleArrangementDto =>
            {
                var module = _moduleRepository.FirstOrDefault(moduleArrangementDto.ModuleId);
                if (module != null)
                {
                    List<ShortCompetenceDTO> shortCompetenceDtos = new List<ShortCompetenceDTO>();
                    _competenceRepository.GetCompetenciesByCompetenceID(module.ArrayOfTrainingCompetencies)
                    .ForEach(comp => shortCompetenceDtos.Add(new ShortCompetenceDTO(comp.Id, comp.Name)));
                    fullModuleArrangementDtos.Add(new FullModuleArrangementDTO(module.Id, 
                        moduleArrangementDto.TrainTime, module.Title, 
                        DateCalculator.CalculateModuleTotalDays(module.ModuleDuration, moduleArrangementDto.TrainTime), 
                        module.ModuleDuration.ToString(),shortCompetenceDtos));
                }
            });
            return fullModuleArrangementDtos;
        }
    }
}
