using System;
using RosenCDK.DTO;
using RosenCDK.Repositories;
using System.Linq;
using RosenCDK.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using Abp.Domain.Uow;
using Newtonsoft.Json;
using NUnit.Framework;
using RosenCDK.Utilities;

namespace RosenCDK.BussinessLogics
{
    public class TraineeAppService : RosenCDKAppServiceBase, ITraineeAppService
    {
        private readonly ITraineeRepository _traineeRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IJobFunctionRepository _jobFunctionRepository;
        private readonly ICompetenceRepository _competenceRepository;
        private readonly IProgramRepository _programRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICompareAppService _compareAppService;
        private readonly ITokenAppService _tokenAppService;
        public IConfigurationRepository ConfigurationRepository { get; set; }

        public TraineeAppService(ITraineeRepository traineeRepo, ITrainerRepository trainerRepository, 
            IJobFunctionRepository jobFunctionRepo, 
            ICompetenceRepository competenceRepo, IProgramRepository programRepo, 
            ITrainingRepository trainingRepo, ICompareAppService compareService, 
            IModuleRepository moduleRepo, IPersonRepository personRepo, ITokenAppService tokenAppService)
        {
            _traineeRepository = traineeRepo;
            _trainerRepository = trainerRepository;
            _jobFunctionRepository = jobFunctionRepo;
            _competenceRepository = competenceRepo;
            _programRepository = programRepo;
            _trainingRepository = trainingRepo;
            _compareAppService = compareService;
            _moduleRepository = moduleRepo;
            _personRepository = personRepo;
            _tokenAppService = tokenAppService;
        }

        public FullTraineeDetailDTO GetFullTraineeInfo(string personAuthToken)
        {
            string username = _tokenAppService.GetUsernameByToken(personAuthToken);

            Trainee trainee = _traineeRepository.FirstOrDefault(thisTrainee => thisTrainee.Person.Username == username);
            if (trainee == null)
            {
                return null;
            }
            FullTraineeDetailDTO fullTraineeDetailDto = new FullTraineeDetailDTO();
            //Have a trainee
            fullTraineeDetailDto.TraineeID = trainee.Id;

            var person = _personRepository.Get(trainee.PersonId);

            fullTraineeDetailDto.Name = person.Name;
            fullTraineeDetailDto.DefaultDepartment = trainee.DefaultDepartment;

            #region Get array Job Function specialized with this trainee

            List<ShortJobFunctionDTO> shortJobFunctionDtos = new List<ShortJobFunctionDTO>();

            var jobFunctions = _jobFunctionRepository.getJobFunctionsByJobFunctionIds(trainee.ArrayOfJobFunction);
            foreach (JobFunction jobFunction in jobFunctions)
            {
                ;
                shortJobFunctionDtos.Add(new ShortJobFunctionDTO(
                    jobFunction.Id,
                    jobFunction.JobFunctionTitle
                ));
            }
            fullTraineeDetailDto.JobFunctions = shortJobFunctionDtos;

            #endregion

            #region Get array Competencies specialized with this trainee

            List<CompetenceDTO> listCompetencies = new List<CompetenceDTO>();
            var competencies = _competenceRepository.GetCompetenciesByCompetenceID(trainee.ArrayOfCompetence);
            foreach (Competence competence in competencies)
            {
                listCompetencies.Add(new CompetenceDTO(
                    competence.Id,
                    competence.Name,
                    competence.Description
                ));
            }
            fullTraineeDetailDto.Competencies = listCompetencies;

            #endregion

            #region Get array targeted trainings specialized with this trainee

            List<TrainingDTO> targetedForTrainings = new List<TrainingDTO>();
            var targetedPrograms = _trainingRepository.GetTrainingsByID(trainee.ArrayOfTargetedTraining);
            foreach (Training training in targetedPrograms)
            {
                
               
                targetedForTrainings.Add(CreateTrainingDTO(training));
            }
            fullTraineeDetailDto.TargetedForTrainings = targetedForTrainings;

            #endregion

            #region Get array Trainings attented specialized with this trainee

            List<TrainingDTO> listAttentedTrainings = new List<TrainingDTO>();
            var attentedTrainings = _trainingRepository.GetTrainingsByID(trainee.ArrayOfAttendedTraining);
            foreach (Training training in attentedTrainings)
            {
                listAttentedTrainings.Add(CreateTrainingDTO(training));
            }
            fullTraineeDetailDto.TrainingsAttended = listAttentedTrainings;

            #endregion

            return fullTraineeDetailDto;
        }

        private TrainingDTO CreateTrainingDTO(Training training)
        {
            List<ShortTraineeDetailDTO> shortTraineeDetailDtos = new List<ShortTraineeDetailDTO>();
            List<ShortTrainerDetailDTO> shortTrainerDetailDtos = new List<ShortTrainerDetailDTO>();
            var trainees = _traineeRepository.getTraineesByArrayId(training.ArrayOfAssignedTrainees);
            var trainers = _trainerRepository.GetTrainersByArrayId(training.ArrayOfAssignedTrainees);
            trainees.ForEach(thisTrainee => shortTraineeDetailDtos.Add(new ShortTraineeDetailDTO(thisTrainee.Id, thisTrainee.Person.Name, thisTrainee.DefaultDepartment)));
            trainers.ForEach(thisTrainer => shortTrainerDetailDtos.Add(new ShortTrainerDetailDTO(thisTrainer.Id, thisTrainer.Person.Name)));

            TrainingDTO trainingDto = new TrainingDTO(
                training.Id, training.ProgramId, training.Programs.ProgramTitle,
                ConfigurationRepository.GetMaximumHoursPerDay(),
                training.TrainingStatus, training.StartDate, training.EndDate, training.TotalDuration,
                shortTraineeDetailDtos,
                shortTrainerDetailDtos,
                BuildFullModuleArrangementDTO(training.ModuleArrangement)
            );
            return trainingDto;
        }
        public TraineeDTO GetTraineeById(int traineeId)
        {
            var trainee = _traineeRepository.FirstOrDefault(traineeId);
            if (trainee == null)
            {
                return null;
            }
            List<JobFunction> jobFunctionsOfTrainee =
                _jobFunctionRepository.getJobFunctionsByJobFunctionIds(trainee.ArrayOfJobFunction);
            List<ShortJobFunctionDTO> jobFunctionDtos = new List<ShortJobFunctionDTO>();
            jobFunctionsOfTrainee.ForEach(JobFunction => jobFunctionDtos.Add(new ShortJobFunctionDTO(JobFunction.Id,JobFunction.JobFunctionTitle)));
            List<Competence> competencesOfTrainee =
                _competenceRepository.GetCompetenciesByCompetenceID(trainee.ArrayOfCompetence);
            List<ShortCompetenceDTO> shortCompetenceDtos = new List<ShortCompetenceDTO>();
            competencesOfTrainee.ForEach(competence => shortCompetenceDtos.Add(new ShortCompetenceDTO(competence.Id,competence.Name)));

            List<Training> targetTrainingsOfTrainee = 
                _trainingRepository.GetTrainingsByID(trainee.ArrayOfTargetedTraining);
            List<ShortTrainingDTO> targetShortTrainingDtos = new List<ShortTrainingDTO>();
            targetTrainingsOfTrainee.ForEach(training => targetShortTrainingDtos.Add(new ShortTrainingDTO(training.Programs.ProgramTitle)));
            List<Training> attendedTrainingsOfTrainee =
                _trainingRepository.GetTrainingsByID(trainee.ArrayOfAttendedTraining);
            List<ShortTrainingDTO> attendShortTrainingDtos = new List<ShortTrainingDTO>();
            attendedTrainingsOfTrainee.ForEach(training => attendShortTrainingDtos.Add(new ShortTrainingDTO(training.Programs.ProgramTitle)));

            TraineeDTO traineeDto = new TraineeDTO(trainee.Id, trainee.PersonId, trainee.Person.Name, 
                trainee.DefaultDepartment, jobFunctionDtos, shortCompetenceDtos, targetShortTrainingDtos, attendShortTrainingDtos);

            return traineeDto;
        }
       
         

        public ResponseMessageDTO RegisterTrainee(RegisterTraineeInputDTO registerTraineeInput)
        {
            try
            {
                //Get trainee who want to register to the training
                Trainee registerTrainee = _traineeRepository.Get(registerTraineeInput.TraineeId);

                //Get training that open for the trainee to register
                Training registerTraining = _trainingRepository.Get(registerTraineeInput.TrainingId);

                //Check the trainee is already registered for the training or not
                if (_compareAppService.Duplicate(registerTrainee.Id, registerTraining.ArrayOfAssignedTrainees)
                    || _compareAppService.Duplicate(registerTraining.Id, registerTrainee.ArrayOfTargetedTraining))
                {
                    //Already registered -> return message
                    return new ResponseMessageDTO() {
                        Status = false,
                        Message = L("Register_TraineeFailed_TraineeExisted")
                    };
                }
                else
                {
                    //Not yet register -> register trainee to training
                    List<int> traineesAssigned = registerTraining.ArrayOfAssignedTrainees.ToList();
                    List<int> targetedForProgram = registerTrainee.ArrayOfTargetedTraining.ToList();

                    traineesAssigned.Add(registerTraineeInput.TraineeId);
                    targetedForProgram.Add(registerTraineeInput.TrainingId);

                    registerTraining.ArrayOfAssignedTrainees = traineesAssigned.ToArray();
                    registerTrainee.ArrayOfTargetedTraining = targetedForProgram.ToArray();

                    //Update trainee and training to database
                    var updateTraining = _trainingRepository.Update(registerTraining);
                    var updateTrainee = _traineeRepository.Update(registerTrainee);
                    if (updateTraining.Id != 0)
                    {
                        if (updateTrainee.Id != 0)
                        {
                            return new ResponseMessageDTO(true, L("Register_TraineeSuccess"));
                        }
                        else
                        {
                            return new ResponseMessageDTO(false, L("Register_Trainee_UpdateFail"));
                        }
                    }
                    else
                    {
                        return new ResponseMessageDTO(false, L("Register_Trainee_UpdateTrainingFail"));
                    }
                }
            }
            catch
            {
                return new ResponseMessageDTO(false, L("Register_TraineeFaile"));
            }
        }

        public ListSuitableTraineeDTO GetSuitableTrainees(int programID)
        {
            try
            {
                ListSuitableTraineeDTO suitableTrainees = new ListSuitableTraineeDTO();

                List<ShortTraineeDetailDTO> listSuitableTrainees = new List<ShortTraineeDetailDTO>();

                #region Get Competencies trained by the program, we will use it to compare with trainee's Competencies

                List<int> competenciesTrainedID = new List<int>();

                //Get program by ProgramId
                var program = _programRepository.Get(programID);

                int[] modulesIncludedID = program.ArrayOfIncludedModules;

                //Get list module by array ModuleId
                var modulesIncluded = _moduleRepository.GetModulesByArrayID(modulesIncludedID);

                foreach (Module module in modulesIncluded)
                {
                    //Get list Competencies trained by module
                    List<int> competenciesTrainedIDByModule = module.ArrayOfTrainingCompetencies.ToList();

                    //Add there Competencies to list Competencies trained by program
                    foreach (int competenceID in competenciesTrainedIDByModule)
                    {
                        competenciesTrainedID.Add(competenceID);
                    }
                }

                //Remove the duplicate Competencies
                competenciesTrainedID = competenciesTrainedID.Distinct().ToList();

                #endregion

                #region Get all the trainee in database, we will findout the suitable trainees

                var trainees = _traineeRepository.GetAllIncluding(trainee => trainee.Person);

                foreach (Trainee trainee in trainees)
                {
                    int[] competenciesGained = trainee.ArrayOfCompetence;

                    if (_compareAppService.IsMissingCompetencies(competenciesGained, competenciesTrainedID.ToArray()))
                    {
                        //This one is a suitable trainee
                        ShortTraineeDetailDTO suitableTrainee = new ShortTraineeDetailDTO();
                        
                        suitableTrainee.TraineeId = trainee.Id;
                        suitableTrainee.Name = trainee.Person.Name;
                        suitableTrainee.DefaultDepartment = trainee.DefaultDepartment;

                        listSuitableTrainees.Add(suitableTrainee);
                    }
                }

                suitableTrainees.ListSuitableTrainees = listSuitableTrainees;

                #endregion

                return suitableTrainees;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public ListSuitableTraineeDTO GetAllTrainee()
        {
            ListSuitableTraineeDTO listSuitableTraineeDto = new ListSuitableTraineeDTO();

            List<ShortTraineeDetailDTO> shortTraineeDetailDtos = new List<ShortTraineeDetailDTO>();

            var traineeList = _traineeRepository.GetAllList();

            foreach (var trainee in traineeList)
            {
                ShortTraineeDetailDTO shortTraineeDetailDto = 
                    new ShortTraineeDetailDTO(trainee.Id,trainee.Person.Name,trainee.DefaultDepartment);
        
                shortTraineeDetailDtos.Add(shortTraineeDetailDto);
            }

            listSuitableTraineeDto.ListSuitableTrainees = shortTraineeDetailDtos;

            return listSuitableTraineeDto;
        }

        public ResponseMessageDTO RegisterTraineeProfile(RegisterTraineeProfileInputDTO registerTraineeProfileInput,
            string personAuthToken)
        {
            string username = _tokenAppService.GetUsernameByToken(personAuthToken);

            Trainee trainee = _traineeRepository.FirstOrDefault(thisTrainee => thisTrainee.Person.Username == username);

            if (trainee == null) return null;

            return this.RegisterTrainee(new RegisterTraineeInputDTO()
            {
                TraineeId = trainee.Id,
                TrainingId = registerTraineeProfileInput.TrainingId,
                ProgramId = registerTraineeProfileInput.ProgramId
            });
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
                        module.ModuleDuration.ToString(), shortCompetenceDtos));
                }
            });
            return fullModuleArrangementDtos;
        }
    }
}
