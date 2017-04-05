using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abp.Domain.Uow;
using RosenCDK.DTO;
using RosenCDK.Repositories;
using RosenCDK.Entities;
using Newtonsoft.Json;
using NUnit.Framework;
using RosenCDK.Utilities;

namespace RosenCDK.BussinessLogics
{
    public class TrainingAppService : RosenCDKAppServiceBase, ITrainingAppService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IProgramRepository _programRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ICompetenceRepository _competenceRepository;
        private readonly ICompareAppService _compareAppService;
        private readonly ITraineeRepository _traineeRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly ITrainingStatusRepository _trainingStatusRepository;

        public TrainingAppService(ITrainingRepository trainingRepo, 
            IProgramRepository programRepo, IModuleRepository moduleRepo, 
            IConfigurationRepository configurationRepo, 
            ICompetenceRepository competenceRepo, ICompareAppService compareService, 
            ITraineeRepository traineeRepo, IPersonRepository personRepo, 
            ITrainerRepository trainerRepo, ITrainingStatusRepository trainingStatusRepo)
        {
            _trainingRepository = trainingRepo;
            _programRepository = programRepo;
            _moduleRepository = moduleRepo;
            _configurationRepository = configurationRepo;
            _competenceRepository = competenceRepo;
            _compareAppService = compareService;
            _traineeRepository = traineeRepo;
            _personRepository = personRepo;
            _trainerRepository = trainerRepo;
            _trainingStatusRepository = trainingStatusRepo;
        }

        public EndDateMessageDTO CalculaterEndDate(CalculateEndDateInputDTO calculateEndDateInput)
        {
            try
            {
                string endDate = "";

                #region Get total dates to train the program

                //Total dates to train the program
                double totalDates = 0;

                //Get modules included to program
                List<ShortModuleDetailDTO> listModulesIncluded = new List<ShortModuleDetailDTO>();

                //Get program by ProgramId
                var program = _programRepository.Get(calculateEndDateInput.ProgramID);

                int[] modulesIncludedID = program.ArrayOfIncludedModules;

                //Call a DAL method to get list module 
                var modules = _moduleRepository.GetModulesByArrayID(modulesIncludedID);

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

                    var competenciesTrained = _competenceRepository.GetCompetenciesByCompetenceID(module.ArrayOfTrainingCompetencies);
                        
                    foreach (Competence competence in competenciesTrained)
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

                foreach (ShortModuleDetailDTO module in listModulesIncluded)
                {
                    totalDates += module.TotalDate;
                }

                #endregion

                #region Calculate end date

                //List days off in a week
                List<int> daysOff = JsonConvert.DeserializeObject<int[]>(_configurationRepository.GetDaysOff()).ToList();

                endDate = _compareAppService.CalculateEndDate(calculateEndDateInput.StartDate, totalDates, daysOff);

                #endregion

                return new EndDateMessageDTO() {
                    status = true,
                    message = L("CalculaterEndDateSuccess"),
                    endDate = endDate
                };
            }
            catch
            {
                return new EndDateMessageDTO()
                {
                    status = false,
                    message = L("UnknownError"),
                    endDate = "null"
                };
            }
        }

        public EndDateMessageDTO CustomEndDate(CustomEndDateInputDTO customEndDateInput)
        {
            try
            {
                string endDate = "";

                #region Get total days to train those modules arrangement

                double totalDays = 0;

                //List modules arrangement was parsed from ModulesArrangement string
                List<ModuleArrangementDTO> listModulesArrangement = 
                    JsonConvert.DeserializeObject<ModuleArrangementDTO[]>(customEndDateInput.ModulesArrangement).ToList();

                //Get list modules information from list modules arrangement above
                List<ShortModuleDetailDTO> listModulesDetail = new List<ShortModuleDetailDTO>();

                foreach (ModuleArrangementDTO moduleArrangement in listModulesArrangement)
                {
                    ShortModuleDetailDTO shortModule = new ShortModuleDetailDTO();

                    shortModule.ModuleId = moduleArrangement.ModuleId;
                    shortModule.TrainTime = moduleArrangement.TrainTime;

                    var module = _moduleRepository.Get(moduleArrangement.ModuleId);

                    shortModule.Duration = module.Theory + module.Pratical;

                    //Get total days to train this module
                    if ((shortModule.Duration % shortModule.TrainTime) != 0)
                    {
                        double temp = shortModule.Duration / shortModule.TrainTime;
                        shortModule.TotalDate = Math.Truncate(temp) + 1;
                    }
                    else
                    {
                        shortModule.TotalDate = shortModule.Duration / shortModule.TrainTime;
                    }

                    listModulesDetail.Add(shortModule);
                }

                //Get total days to train those modules arrangement
                foreach (ShortModuleDetailDTO module in listModulesDetail)
                {
                    totalDays += module.TotalDate;
                }

                #endregion

                #region Calculate end date

                //List of days off in a week
                List<int> daysOff = JsonConvert.DeserializeObject<int[]>(_configurationRepository.GetDaysOff()).ToList();

                endDate = _compareAppService.CalculateEndDate(customEndDateInput.StartDate, totalDays, daysOff);

                #endregion

                return new EndDateMessageDTO()
                {
                    status = true,
                    message = L("CalculaterEndDateSuccess"),
                    endDate = endDate
                };
            }
            catch
            {
                return new EndDateMessageDTO()
                {
                    status = false,
                    message = L("UnknownError"),
                    endDate = "null"
                };
            }
        }

        public ListTrainingDTO GetAllTrainings()
        {
            try
            {
                ListTrainingDTO listTrainingDTO = new ListTrainingDTO();
                List<TrainingDTO> trainingDTOs = new List<TrainingDTO>();

                //Call a DAL method to get all training in database
                List<Training> listTraining = _trainingRepository.GetAllList();

                foreach (var training in listTraining)
                {
                                      trainingDTOs.Add(CreateTrainingDTO(training));
                }

                listTrainingDTO.TrainingList = trainingDTOs;

                return listTrainingDTO;
            }
            catch
            {
                return null;
            }
        }

        public TrainingDetailDTO GetTrainingDetailByID(int trainingID)
        {
            try
            {
                TrainingDetailDTO returnTraining = new TrainingDetailDTO();

                //Call a DAL method to get Training by TrainingId
                Training training = _trainingRepository.Get(trainingID);


                returnTraining.TrainingId = training.Id;
                returnTraining.ProgramId = training.ProgramId;

                var program = _programRepository.Get(training.ProgramId);

                returnTraining.ProgramName = program.ProgramTitle;
                returnTraining.StatusId = training.StatusId;
                returnTraining.StatusName = training.TrainingStatus.StatusName; //need to check
                returnTraining.StartDate = training.StartDate.ToString();
                returnTraining.EndDate = training.EndDate.ToString();
                returnTraining.TotalDuration = training.TotalDuration;
                returnTraining.MaxHoursPerDay = _configurationRepository.GetMaximumHoursPerDay();

                #region Get array trainees assigned

                List<ShortTraineeDetailDTO> listTraineesAssigned = new List<ShortTraineeDetailDTO>();

                //Call a DAL method to get trainees by array traineeIDs
                List<Trainee> listTrainees = _traineeRepository.getTraineesByArrayId(training.ArrayOfAssignedTrainees);

                foreach (var x in listTrainees)
                {
                    ShortTraineeDetailDTO trainee = new ShortTraineeDetailDTO();

                    trainee.TraineeId = x.Id;

                    var person = _personRepository.Get(x.PersonId);

                    trainee.Name = person.Name;

                    listTraineesAssigned.Add(trainee);
                }

                returnTraining.TraineesAssigned = listTraineesAssigned;

                #endregion

                #region Get array trainers assigned

                List<ShortTrainerDetailDTO> listTrainersAssigned = new List<ShortTrainerDetailDTO>();

                //Call a DAL method to get trainers by array trainerIDs
                List<Trainer> trainerList = _trainerRepository.GetTrainersByArrayId(training.ArrayOfAssignedTrainers);

                foreach (var x in trainerList)
                {
                    ShortTrainerDetailDTO trainer = new ShortTrainerDetailDTO();

                    trainer.TrainerId = x.Id;

                    var person = _personRepository.Get(x.PersonId);

                    trainer.Name = person.Name;

                    listTrainersAssigned.Add(trainer);
                }

                returnTraining.TrainersAssigned = listTrainersAssigned;

                #endregion

                #region Get array modules included

                List<ShortModuleDetailDTO> listModulesIncluded = new List<ShortModuleDetailDTO>();

                //Array moduleIDs
                ModuleArrangementDTO[] arrayModulesArrangement = JsonConvert.DeserializeObject<ModuleArrangementDTO[]>(training.ModuleArrangement);

                foreach (ModuleArrangementDTO moduleArrangement in arrayModulesArrangement.ToList())
                {
                    ShortModuleDetailDTO shortModule = new ShortModuleDetailDTO();

                    //Get id and TrainTime of module
                    shortModule.ModuleId = moduleArrangement.ModuleId;
                    shortModule.TrainTime = moduleArrangement.TrainTime;
                    //With each ModuleId, we get module data from database by that ModuleId
                    Module module = _moduleRepository.Get(moduleArrangement.ModuleId);

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
                    List<Competence> competencies =
                        _competenceRepository.GetCompetenciesByCompetenceID(module.ArrayOfTrainingCompetencies);

                    foreach (var competence in competencies)
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

                returnTraining.ModulesArrangement = listModulesIncluded;

                #endregion

                return returnTraining;
            }
            catch
            {
                return null;
            }
        }

        [UnitOfWork]
        public ResponseMessageDTO CreateTraining(CreateTrainingInputDTO createTrainingInput)
        {
            try
            {
                if (DateTime.Now > DateCalculator.StringToDate(createTrainingInput.StartDate))
                {
                    //Dont allow create training at present day

                    return new ResponseMessageDTO(false,L("CreateTraining_TodayFail"));
                }

                //Create training
                Training newTraining = new Training()
                {
                    ProgramId = createTrainingInput.ProgramID,
                    StatusId = _trainingStatusRepository.GetTrainingStatusIdByName(TrainingStatus.PLANNED),
                    StartDate = DateCalculator.StringToDate(createTrainingInput.StartDate),
                    EndDate = DateCalculator.StringToDate(createTrainingInput.EndDate),
                    TotalDuration = createTrainingInput.TotalDuration,
                    ArrayOfAssignedTrainees = JsonConvert.DeserializeObject<int[]>(createTrainingInput.AssignedTrainees),
                    ArrayOfAssignedTrainers = JsonConvert.DeserializeObject<int[]>(createTrainingInput.AssignedTrainers),
                    ModuleArrangement = createTrainingInput.ModulesArrangement
                };

                int trainingID = _trainingRepository.InsertAndGetId(newTraining);

                if (trainingID != 0)
                {
                    var training = _trainingRepository.Get(trainingID);

                    #region Update trainings targeted of trainees assigned

                    List<int> listAssignedTraineeID = training.ArrayOfAssignedTrainees.ToList();

                    foreach (int assignedTraineeID in listAssignedTraineeID)
                    {
                        var assignedTrainee = _traineeRepository.Get(assignedTraineeID);

                        List<int> listTrainingsTargeted = assignedTrainee.ArrayOfTargetedTraining.ToList();

                        listTrainingsTargeted.Add(trainingID);
                        listTrainingsTargeted = listTrainingsTargeted.Distinct().ToList();

                        assignedTrainee.ArrayOfTargetedTraining = listTrainingsTargeted.ToArray();

                        var updateTrainee = _traineeRepository.Update(assignedTrainee);

                        if (updateTrainee.Id == 0)
                        {
                            return new ResponseMessageDTO(false, L("CreateTraining_DbFail"));

                        }
                    }

                    #endregion

                    return new ResponseMessageDTO(true, L("CreateTraining_Success"));
                }
                else
                {
                    return new ResponseMessageDTO(false, L("CreateTraining_Fail"));
                }
            }
            catch
            {
                return new ResponseMessageDTO(false, L("CreateTraining_Fail") + L("UnknownError"));
            }
        }

        public ResponseMessageDTO UpdateTraining(UpdateTrainingInputDTO updateTrainingInput)
        {
            try
            {
                if(updateTrainingInput.TrainingID <= 0)
                {
                    return new ResponseMessageDTO(false, L("UpdateTraining_Fail"));
                   
                }
                else
                {
                    var training = _trainingRepository.Get(updateTrainingInput.TrainingID);

                    if (training.StatusId == _trainingStatusRepository.GetTrainingStatusIdByName(TrainingStatus.CANCELED) 
                        || training.StatusId == _trainingStatusRepository.GetTrainingStatusIdByName(TrainingStatus.COMPLETED))
                    {
                        //training already canceled or completed -> cant update
                        return new ResponseMessageDTO(false, L("UpdateTraining_CancelOrCompleteFail"));
                       
                    }
                    else if (training.StatusId == _trainingStatusRepository.GetTrainingStatusIdByName(TrainingStatus.ONGOING))
                    {
                        //training already on going -> can update end date, trainees assigned, trainers assigned
                        //modules arrangement

                        int[] arrayOldTraineesIDAssigned = training.ArrayOfAssignedTrainees;
                        int[] arrayNewTraineesIDAssigned = JsonConvert.DeserializeObject<int[]>(updateTrainingInput.AssignedTrainees);

                        #region Remove training targeted from old trainees assigned

                        List<int> listOldTraineesIDAssigned = _compareAppService.GetMissingIntegers(arrayNewTraineesIDAssigned, arrayOldTraineesIDAssigned).ToList();

                        foreach (int oldTraineeIDAssigned in listOldTraineesIDAssigned)
                        {
                            var oldTraineeAssigned = _traineeRepository.Get(oldTraineeIDAssigned);

                            List<int> listTrainingsTargeted = oldTraineeAssigned.ArrayOfTargetedTraining.ToList();

                            var itemToRemove = listTrainingsTargeted.SingleOrDefault(r => r == updateTrainingInput.TrainingID);

                            if (itemToRemove != 0)
                            {
                                //Remove training from trainings targeted of this trainee
                                listTrainingsTargeted.Remove(itemToRemove);
                            }

                            oldTraineeAssigned.ArrayOfTargetedTraining = listTrainingsTargeted.ToArray();

                            var updateOldTrainee = _traineeRepository.Update(oldTraineeAssigned);

                            if (updateOldTrainee.Id == 0)
                            {
                                return new ResponseMessageDTO(false, L("UpdateTraining_FailError"));
                           
                            }
                        }

                        #endregion

                        #region Add training targeted to new trainees assigned

                        List<int> listNewTraineesIDAssigned = _compareAppService.GetMissingIntegers(arrayOldTraineesIDAssigned, arrayNewTraineesIDAssigned).ToList();

                        foreach (int newTraineeIDAssigned in listNewTraineesIDAssigned)
                        {
                            var newTraineeAssigned = _traineeRepository.Get(newTraineeIDAssigned);

                            List<int> listTrainingsTargeted = newTraineeAssigned.ArrayOfTargetedTraining.ToList();

                            //Add training to trainings targeted of this trainee
                            listTrainingsTargeted.Add(updateTrainingInput.TrainingID);
                            listTrainingsTargeted = listTrainingsTargeted.Distinct().ToList();

                            newTraineeAssigned.ArrayOfTargetedTraining = listTrainingsTargeted.ToArray();

                            var updateNewTrainee = _traineeRepository.Update(newTraineeAssigned);

                            if (updateNewTrainee.Id == 0)
                            {
                                return new ResponseMessageDTO(false, L("UpdateTraining_FailError"));
                              
                            }
                        }

                        #endregion

                        //Update training information
                        training.EndDate = DateCalculator.StringToDate(updateTrainingInput.EndDate);
                        training.TotalDuration = updateTrainingInput.TotalDuration;
                        training.ArrayOfAssignedTrainees = JsonConvert.DeserializeObject<int[]>(updateTrainingInput.AssignedTrainees);
                        training.ArrayOfAssignedTrainers = JsonConvert.DeserializeObject<int[]>(updateTrainingInput.AssignedTrainers);
                        training.ModuleArrangement = updateTrainingInput.ModulesArrangement;

                        var updateTraining = _trainingRepository.Update(training);
                    }
                    else
                    {
                        //training just planed -> can update start date, end date, trainees assigned
                        //trainers assigned, modules arrangement

                        if (DateTime.Now >= DateCalculator.StringToDate(updateTrainingInput.StartDate))
                        {
                            //The StartDate is over -> cant update
                            return new ResponseMessageDTO(false, L("UpdateTraining_StartEndDate"));
                            
                        }
                        else
                        {
                            int[] arrayOldTraineesIDAssigned = training.ArrayOfAssignedTrainees;
                            int[] arrayNewTraineesIDAssigned = JsonConvert.DeserializeObject<int[]>(updateTrainingInput.AssignedTrainees);

                            #region Remove training targeted from old trainees assigned

                            List<int> listOldTraineesIDAssigned = _compareAppService.GetMissingIntegers(arrayNewTraineesIDAssigned, arrayOldTraineesIDAssigned).ToList();

                            foreach (int oldTraineeIDAssigned in listOldTraineesIDAssigned)
                            {
                                var oldTraineeAssigned = _traineeRepository.Get(oldTraineeIDAssigned);

                                List<int> listTrainingsTargeted = oldTraineeAssigned.ArrayOfTargetedTraining.ToList();

                                var itemToRemove = listTrainingsTargeted.SingleOrDefault(r => r == updateTrainingInput.TrainingID);

                                if (itemToRemove != 0)
                                {
                                    //Remove training from trainings targeted of this trainee
                                    listTrainingsTargeted.Remove(itemToRemove);
                                }

                                oldTraineeAssigned.ArrayOfTargetedTraining = listTrainingsTargeted.ToArray();

                                var updateOldTrainee = _traineeRepository.Update(oldTraineeAssigned);

                                if (updateOldTrainee.Id == 0)
                                {
                                    return new ResponseMessageDTO(false, L("UpdateTraining_FailError"));
                                    
                                }
                            }

                            #endregion

                            #region Add training targeted to new trainees assigned

                            List<int> listNewTraineesIDAssigned = _compareAppService.GetMissingIntegers(arrayOldTraineesIDAssigned, arrayNewTraineesIDAssigned).ToList();

                            foreach (int newTraineeIDAssigned in listNewTraineesIDAssigned)
                            {
                                var newTraineeAssigned = _traineeRepository.Get(newTraineeIDAssigned);

                                List<int> listTrainingsTargeted = newTraineeAssigned.ArrayOfTargetedTraining.ToList();

                                //Add training to trainings targeted of this trainee
                                listTrainingsTargeted.Add(updateTrainingInput.TrainingID);
                                listTrainingsTargeted = listTrainingsTargeted.Distinct().ToList();

                                newTraineeAssigned.ArrayOfTargetedTraining = listTrainingsTargeted.ToArray();

                                var updateNewTrainee = _traineeRepository.Update(newTraineeAssigned);

                                if (updateNewTrainee.Id == 0)
                                {
                                    return new ResponseMessageDTO(false, L("CreateTraining_Fail") + L("UnknownError"));
                                   
                                }
                            }

                            #endregion

                            //Update training information
                            training.StartDate = DateCalculator.StringToDate(updateTrainingInput.StartDate);
                            training.EndDate = DateCalculator.StringToDate(updateTrainingInput.EndDate);
                            training.TotalDuration = updateTrainingInput.TotalDuration;
                            training.ArrayOfAssignedTrainees = JsonConvert.DeserializeObject<int[]>(updateTrainingInput.AssignedTrainees);
                            training.ArrayOfAssignedTrainers = JsonConvert.DeserializeObject<int[]>(updateTrainingInput.AssignedTrainers);
                            training.ModuleArrangement = updateTrainingInput.ModulesArrangement;

                            var updateTraining = _trainingRepository.Update(training);
                        }
                    }

                    return new ResponseMessageDTO(true, L("UpdateTraining_Success"));
                }
            }
            catch
            {
                return new ResponseMessageDTO(false, L("CreateTraining_Fail") + L("UnknownError"));
            }
        }

        public ResponseMessageDTO CancelTraining(int trainingID)
        {
            try
            {
                if (trainingID <= 0)
                {
                    return new ResponseMessageDTO(false, L("CancelTraining_Fail"));
                }
                else
                {
                    //Update training status to canceled
                    var training = _trainingRepository.Get(trainingID);

                    training.StatusId = _trainingStatusRepository.GetTrainingStatusIdByName(TrainingStatus.CANCELED);

                    var updateTraining = _trainingRepository.Update(training);

                    #region Update trainings targeted of trainees assigned

                    List<int> listTraineesIDAssigned = updateTraining.ArrayOfAssignedTrainees.ToList();

                    foreach (int traineeIDAssigned in listTraineesIDAssigned)
                    {
                        var traineeAssigned = _traineeRepository.Get(traineeIDAssigned);

                        List<int> listTrainingsTargeted = traineeAssigned.ArrayOfTargetedTraining.ToList();

                        var itemToRemove = listTrainingsTargeted.SingleOrDefault(r => r == trainingID);

                        if (itemToRemove != 0)
                        {
                            //Remove training from training targeted
                            listTrainingsTargeted.Remove(itemToRemove);
                        }

                        traineeAssigned.ArrayOfTargetedTraining = listTrainingsTargeted.ToArray();

                        var updateOldTrainee = _traineeRepository.Update(traineeAssigned);

                        if (updateOldTrainee.Id == 0)
                        {
                            return new ResponseMessageDTO(false, L("CreateTraining_Fail") + L("UnknownError"));
                           
                        }
                    }

                    #endregion
                    return new ResponseMessageDTO(true, L("CancelTraining_Success"));

                }
            }
            catch
            {
                return new ResponseMessageDTO(false, L("UpdateTraining_FailError"));
            }
        }

        public ResponseMessageDTO CompleteTraining(CompleteTrainingInputDTO completeTrainingInput)
        {
            try
            {
                if (completeTrainingInput.TrainingId <= 0)
                {
                    return new ResponseMessageDTO(false, L("CompleteTraining_Fail"));
                   
                }
                else
                {
                    var training = _trainingRepository.Get(completeTrainingInput.TrainingId);

                    if (training.StatusId == _trainingStatusRepository.GetTrainingStatusIdByName(TrainingStatus.CANCELED) 
                        || training.StatusId == _trainingStatusRepository.GetTrainingStatusIdByName(TrainingStatus.PLANNED))
                    {
                        //training already canceled or planed -> cant completed
                        return new ResponseMessageDTO(false, L(""));
                    }
                    else
                    {
                        //Update status of training to completed
                        training.StatusId = _trainingStatusRepository.GetTrainingStatusIdByName(TrainingStatus.COMPLETED);

                        var updateTraining = _trainingRepository.Update(training);

                        if (updateTraining.Id == 0)
                        {
                            return new ResponseMessageDTO(false, L("CreatTraining"));

                        }

                        #region Update Competencies of trainees assigned

                        //Get list of trainees selected by user to update Competencies
                        TraineeTrainingOutcomeDTO[] trainees = JsonConvert.DeserializeObject<TraineeTrainingOutcomeDTO[]>(completeTrainingInput.TraineeTrainingOutcome);

                        //Update Competencies of trainees above
                        foreach (TraineeTrainingOutcomeDTO trainee in trainees.ToList())
                        {
                            //Get the list of Competencies that the trainee used to update
                            List<int> competenciesUpdate = new List<int>();

                            var traineeEntity = _traineeRepository.Get(trainee.TraineeId);

                            //Get the list of Competencies that the trainee already has
                            int[] sourceCompetencies = traineeEntity.ArrayOfCompetence;

                            //Compare the sourceCompetencies with the selectedCompetencies to determine the 
                            //Competencies which used to add to the trainee
                            int[] missingCompetencies = _compareAppService.missingCompetencies(sourceCompetencies, trainee.SelectedCompetencies);

                            //Add there missing Competencies to the list source Competencies
                            List<int> listSourceCompetencies = sourceCompetencies.ToList();
                            List<int> listMissingCompetencies = missingCompetencies.ToList();

                            foreach (int competence in listMissingCompetencies)
                            {
                                listSourceCompetencies.Add(competence);
                            }

                            //Use the list source Competencies to update 
                            competenciesUpdate = listSourceCompetencies.Distinct().ToList();

                            //Update trainee's Competencies
                            traineeEntity.ArrayOfCompetence = competenciesUpdate.ToArray();

                            var udTrainee = _traineeRepository.Update(traineeEntity);

                            if (udTrainee.Id == 0)
                            {
                                return new ResponseMessageDTO(false, L("UpdateTraining_FailError"));
                            }
                        }

                        #endregion

                        #region Update trainings targeted and training attended of trainees assigned

                        List<int> listTraineesIDAssigned = updateTraining.ArrayOfAssignedTrainees.ToList();

                        foreach (int traineeIDAssigned in listTraineesIDAssigned)
                        {
                            var traineeAssigned = _traineeRepository.Get(traineeIDAssigned);

                            List<int> listTrainingsTargeted = traineeAssigned.ArrayOfTargetedTraining.ToList();
                            List<int> listTrainingsAttended = traineeAssigned.ArrayOfAttendedTraining.ToList();

                            var itemToRemove = listTrainingsTargeted.SingleOrDefault(r => r == completeTrainingInput.TrainingId);

                            if (itemToRemove != 0)
                            {
                                //Remove training from training targeted
                                listTrainingsTargeted.Remove(itemToRemove);
                            }

                            //Add training to training attended
                            listTrainingsAttended.Add(completeTrainingInput.TrainingId);
                            listTrainingsAttended = listTrainingsAttended.Distinct().ToList();

                            //update trainee
                            traineeAssigned.ArrayOfTargetedTraining = listTrainingsTargeted.ToArray();
                            traineeAssigned.ArrayOfAttendedTraining = listTrainingsAttended.ToArray();

                            var updateOldTrainee = _traineeRepository.Update(traineeAssigned);

                            if (updateOldTrainee.Id == 0)
                            {
                                return new ResponseMessageDTO(false, L("UpdateTraining_FailError"));

                            }
                        }

                        #endregion

                        return new ResponseMessageDTO(true, L("UpdateTraining_Success"));

                    }
                }             
            }
            catch
            {
                return new ResponseMessageDTO(false, L("UpdateTraining_FailError"));
            }
        }

        public TrainingDTO GetTrainingDetail(int Id)
        {
            
            var training = _trainingRepository.FirstOrDefault(Id);
            if (training == null)
            {
                return null;
            }
        
            
            return CreateTrainingDTO(training);
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
                training.Id, training.ProgramId, training.Programs.ProgramTitle, _configurationRepository.GetMaximumHoursPerDay(),
                training.TrainingStatus, training.StartDate, training.EndDate, training.TotalDuration,
                shortTraineeDetailDtos,
                shortTrainerDetailDtos,
                BuildFullModuleArrangementDTO(training.ModuleArrangement)
            );
            return trainingDto;
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
                    fullModuleArrangementDtos.Add(new FullModuleArrangementDTO(module.Id, moduleArrangementDto.TrainTime, 
                        module.Title, DateCalculator.CalculateModuleTotalDays(module.ModuleDuration, moduleArrangementDto.TrainTime), 
                        module.ModuleDuration.ToString(), shortCompetenceDtos));
                }
            });
            return fullModuleArrangementDtos;
        }

        public void AutomaticMaintainTraining()
        {
            int hour = _configurationRepository.GetMaintenanceTime();
            int min = 0;
            DateTime temp = new DateTime(DateTime.Now.ToUniversalTime().AddHours(7).Year,
                DateTime.Now.ToUniversalTime().AddHours(7).Month,
                DateTime.Now.ToUniversalTime().AddHours(7).Day);
            DateTime NowUTC = DateTime.Now.ToUniversalTime().AddHours(7);
            if (NowUTC.Hour > hour || (NowUTC.Hour == hour && NowUTC.Minute > min))
            {
                temp = temp.AddDays(1);
            }

            temp = temp.AddHours(hour);
            temp = temp.AddMinutes(min);
            TimeSpan wait = temp - NowUTC;
            TimerCallback handle = new TimerCallback(MaintainTrainings);
            MaintainTrainingTimer.MaintainTimer = new Timer(handle, null, wait, new TimeSpan(24, 0, 0)); //one time per day. (hour,min,sec)
        }

        /// <summary>
        /// Maintain all trainings in database. Auto update Training's information when its reach the start date or end date
        /// </summary>
        private void MaintainTrainings(object state)
        {
            try
            {
                //Set state of system to true -> no one can access system when its maintain
                SystemState.SetSystemState(true);

                List<int> listOnGoingTrainings = new List<int>();
                List<int> listCompletedTrainings = new List<int>();

                var trainings = _trainingRepository.GetAll();

                foreach (Training training in trainings.ToList())
                {
                    #region Get array of trainings that going to start today

                    if (DateTime.Now >= training.StartDate && training.StatusId == 4)
                    {
                        listOnGoingTrainings.Add(training.Id);
                    }

                    #endregion

                    #region Get array of trainings that going to completed today

                    if (DateTime.Now > training.EndDate && training.StatusId == 3)
                    {
                        listCompletedTrainings.Add(training.Id);
                    }

                    #endregion
                }

                #region Update trainings which start today

                foreach (int trainingID in listOnGoingTrainings)
                {
                    var training = _trainingRepository.Get(trainingID);

                    training.StatusId = 4;

                    training = _trainingRepository.Update(training);
                }

                #endregion

                #region Update trainings which completed today

                foreach (int trainingID in listCompletedTrainings)
                {
                    #region Get array of trainees who was assigned to this training and update their information

                    List<TraineeTrainingOutcomeDTO> listTraineesAssigned = new List<TraineeTrainingOutcomeDTO>();

                    var training = _trainingRepository.Get(trainingID);

                    #region Get competencies trained by this training

                    List<int> competenciesTrainedID = new List<int>();

                    //Get program by programID
                    var program = _programRepository.Get(training.ProgramId);

                    int[] modulesIncludedID = program.ArrayOfIncludedModules;

                    //Get list module by array moduleID
                    var modulesIncluded = _moduleRepository.GetModulesByArrayID(modulesIncludedID);

                    foreach (Module module in modulesIncluded)
                    {
                        //Get list competencies trained by module
                        List<int> competenciesTrainedIDByModule = module.ArrayOfTrainingCompetencies.ToList();

                        //Add there competencies to list competencies trained by program
                        foreach (int competenceID in competenciesTrainedIDByModule)
                        {
                            competenciesTrainedID.Add(competenceID);
                        }
                    }

                    //Remove the duplicate competencies
                    competenciesTrainedID = competenciesTrainedID.Distinct().ToList();

                    #endregion

                    #region Get trainees ID represent the trainees who was assigned to this training

                    List<int> listTraineesIDAssigned = training.ArrayOfAssignedTrainees.ToList();

                    #endregion

                    #region Add the trainees assigned to the list of trainees assigned above

                    foreach (int traineeID in listTraineesIDAssigned)
                    {
                        TraineeTrainingOutcomeDTO traineeTrainingOutcomeDto = new TraineeTrainingOutcomeDTO();

                        traineeTrainingOutcomeDto.TraineeId = traineeID;
                        traineeTrainingOutcomeDto.SelectedCompetencies = competenciesTrainedID.ToArray();

                        listTraineesAssigned.Add(traineeTrainingOutcomeDto);
                    }

                    #endregion

                    #region Update training and trainees assigned information

                    //Convert list trainees assigned to JSON string
                    string JSONTraineesAssigned = JsonConvert.SerializeObject(listTraineesAssigned.ToArray());

                    ResponseMessageDTO updateCompleted = CompleteTraining(new CompleteTrainingInputDTO(trainingID, JSONTraineesAssigned));

                    #endregion

                    #endregion
                }

                #endregion

                System.Threading.Thread.Sleep(5 * 60 * 1000);

                //When the maintain is over, set state of system to false -> people can access system normally
                SystemState.SetSystemState(false);
            }
            catch
            {
                SystemState.SetSystemState(false);
            }
        }

    }

}
