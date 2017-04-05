
using RosenCDK.Entities;
using RosenCDK.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rosencdk.Migrations.SeedData
{
    class RosenCDKRoleAndActivityAndPeopleCreator
    {

        private readonly RosenCDKDbContext _context;

        private Role TcRole;
        private Role TraineeRole;
        private Role TrainingRequestorRole;
        private Role TrainerRole;

        private Activity UC_ExecuteDelta_View;
        private Activity UC_ExecuteDelta_SelectTrainee;
        private Activity UC_TrainingManagement_View;
        private Activity UC_TrainingManagement_New;
        private Activity UC_TrainingManagement_Complete;
        private Activity UC_TrainingManagement_Update;
        private Activity UC_TrainingManagement_Cancel;
        private Activity UC_ProgramManagement_View;
        private Activity UC_JobFunctionManagement_View;
        private Activity UC_UserManagement_View;
        private Activity UC_ExecuteDelta_Execute;

        public RosenCDKRoleAndActivityAndPeopleCreator(RosenCDKDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateRoles();
            CreateActivtiesAndRoleDistribution();
            CreateTrainees();
            CreateTrainers();
            CreateTrainingRequestors();
            CreateTcManagers();
        }

        public void CreateRoles()
        {
            TcRole = _context.Roles.Add(new Role()
            {
                RoleName = "TCManager"
            });
            TraineeRole = _context.Roles.Add(new Role()
            {
                RoleName = "Trainee"
            });
            TrainerRole = _context.Roles.Add(new Role()
            {
                RoleName = "Trainer"
            });
            TrainingRequestorRole = _context.Roles.Add(new Role()
            {
                RoleName = "TrainingRequestor"
            });
          
            _context.SaveChanges();
        }

        public void CreateActivtiesAndRoleDistribution()
        {
            UC_ExecuteDelta_View = _context.Activities.Add(new Activity("UC_ExecuteDelta_View"));
            UC_ExecuteDelta_SelectTrainee = _context.Activities.Add(new Activity("UC_ExecuteDelta_SelectTrainee"));
            UC_TrainingManagement_View = _context.Activities.Add(new Activity("UC_TrainingManagement_View"));
            UC_TrainingManagement_New = _context.Activities.Add(new Activity("UC_TrainingManagement_New"));
            UC_TrainingManagement_Complete = _context.Activities.Add(new Activity("UC_TrainingManagement_Complete"));
            UC_TrainingManagement_Update = _context.Activities.Add(new Activity("UC_TrainingManagement_Update"));
            UC_TrainingManagement_Cancel = _context.Activities.Add(new Activity("UC_TrainingManagement_Cancel"));
            UC_ProgramManagement_View = _context.Activities.Add(new Activity( "UC_ProgramManagement_View"));
            UC_JobFunctionManagement_View = _context.Activities.Add(new Activity("UC_JobFunctionManagement_View"));
            UC_UserManagement_View = _context.Activities.Add(new Activity("UC_UserManagement_View")) ;
            UC_ExecuteDelta_Execute = _context.Activities.Add(new Activity("UC_ExecuteDelta_Execute"));

            _context.SaveChanges();
            _context.RoleDistributions.Add(new RoleDistribution(TcRole.Id, UC_ExecuteDelta_Execute.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TcRole.Id, UC_ExecuteDelta_SelectTrainee.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TcRole.Id, UC_TrainingManagement_View.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TcRole.Id, UC_TrainingManagement_New.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TcRole.Id, UC_TrainingManagement_Complete.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TcRole.Id, UC_TrainingManagement_Update.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TcRole.Id, UC_TrainingManagement_Cancel.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TcRole.Id, UC_JobFunctionManagement_View.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TcRole.Id, UC_UserManagement_View.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TcRole.Id, UC_ExecuteDelta_Execute.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TcRole.Id, UC_ProgramManagement_View.Id));

            _context.RoleDistributions.Add(new RoleDistribution(TraineeRole.Id, UC_ExecuteDelta_View.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TraineeRole.Id, UC_TrainingManagement_View.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TraineeRole.Id, UC_ExecuteDelta_Execute.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TrainingRequestorRole.Id, UC_ExecuteDelta_Execute.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TrainingRequestorRole.Id, UC_ExecuteDelta_SelectTrainee.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TrainingRequestorRole.Id, UC_TrainingManagement_View.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TrainingRequestorRole.Id, UC_ProgramManagement_View.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TrainingRequestorRole.Id, UC_JobFunctionManagement_View.Id));
            _context.RoleDistributions.Add(new RoleDistribution(TrainingRequestorRole.Id, UC_ExecuteDelta_Execute.Id));

            _context.SaveChanges();
        }


        private void CreateTrainees()
        {

            Person andygarcia = _context.People.Add(new Person()
            {
                Name = "Andy Garcia",
                Username = "andy.garcia",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TraineeRole.Id

            });
            _context.Trainees.Add(new Trainee()
            {
                PersonId = andygarcia.Id,
                DefaultDepartment = "Software Development",
                JobFunctions = "2,3,4",
                Competencies = "8,9",
                ArrayOfTargetedTraining = new int[] {1,3,5}
            });
            _context.SaveChanges();

            Person allanHudson = _context.People.Add(new Person()
            {
                Name = "Allan Hudson",
                Username = "allan.hudson",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TraineeRole.Id
            });
            _context.Trainees.Add(new Trainee()
            {
                PersonId = allanHudson.Id,
                DefaultDepartment = "Software Development",
                JobFunctions = "5,6,7",
                Competencies = "15,5",
                ArrayOfTargetedTraining = new int[] {7}
            });
            _context.SaveChanges();

            Person elenaMarion = _context.People.Add(new Person()
            {
                Name = "Elena Marion",
                Username = "elena.marion",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TraineeRole.Id
            });
            _context.Trainees.Add(new Trainee()
            {
                PersonId = elenaMarion.Id,
                DefaultDepartment = "Software Development",
                JobFunctions = "1,3,5",
                Competencies = "6,17",
                ArrayOfTargetedTraining =  new int[] {2}
            });
            _context.SaveChanges();

            Person daveFilloni = _context.People.Add(new Person()
            {
                Name = "Dave Filloni",
                Username = "dave.filloni",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TraineeRole.Id
            });
            _context.Trainees.Add(new Trainee()
            {
                PersonId = daveFilloni.Id,
                DefaultDepartment = "Software Development",
                JobFunctions = "2,4,6",
                Competencies = "13,11,2",

            });
            _context.SaveChanges();

            Person davidEvans = _context.People.Add(new Person()
            {
                Name = "David Evans",
                Username = "david.evans",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TraineeRole.Id
            });
            _context.Trainees.Add(new Trainee()
            {
                PersonId = davidEvans.Id,
                DefaultDepartment = "Software Development",
                JobFunctions = "4,5,6",
                Competencies = "1,11",
                ArrayOfTargetedTraining = new int[] { 6 }

            });
            _context.SaveChanges();

            Person dougchiang = _context.People.Add(new Person() {
                Name = "Chiang Doug",
                Username = "doug.chiang",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TraineeRole.Id
            });
            _context.Trainees.Add(new Trainee()
            {
                PersonId = dougchiang.Id,
                DefaultDepartment = "Software Development",
                JobFunctions = "7",
                Competencies = "7,1,17",
                ArrayOfTargetedTraining = new int[] {4 }

            });
            _context.SaveChanges();
            Person SimpsonLizzie = _context.People.Add(new Person()
            {
                Name = "Simpson Lizzie",
                Username = "simpson.lizzie",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TraineeRole.Id
            });
            _context.Trainees.Add(new Trainee()
            {
                PersonId = SimpsonLizzie.Id,
                DefaultDepartment = "Software Development",
                JobFunctions = "3,4,5",
                Competencies = "13,10,2"
            });
            _context.SaveChanges();


            Person HubertFarnsworth = _context.People.Add(new Person()
            {
                Name = "Hubert Farnsworth",
                Username = "hubert.farnsworth",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TraineeRole.Id
            });
            _context.Trainees.Add(new Trainee()
            {
                PersonId = HubertFarnsworth.Id,
                DefaultDepartment = "Software Development",
                JobFunctions = "",
                Competencies = "10,8,9"
            });
            _context.SaveChanges();

        }
        
        private void CreateTrainers()
        {
            Person kathleenKennedy1 = _context.People.Add(new Person()
            {
                Name = "Kathleen Kennedy",
                Username = "kathleen.kennedy",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TrainerRole.Id
            });
            _context.Trainers.Add(new Trainer()
            {
                PersonId = kathleenKennedy1.Id,
                IsExternal = true,
                ArrayOfSuitableModules = new int[] {1,2,3,9}
            });
            _context.SaveChanges();

            Person DollySpears = _context.People.Add(new Person()
            {
                Name = "Dolly Spears",
                Username = "dolly.spears",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TrainerRole.Id
            });
            _context.Trainers.Add(new Trainer()
            {
                PersonId = DollySpears.Id,
                IsExternal = false,
                ArrayOfSuitableModules = new int[] {4,5,6}
            });
            _context.SaveChanges();

            Person pabloHidalgo = _context.People.Add(new Person()
            {
                Name = "Pablo Hidalgo",
                Username = "pablo.hidalgo",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TrainerRole.Id

            });
            _context.Trainers.Add(new Trainer()
            {
                PersonId = pabloHidalgo.Id,
                IsExternal = false,
                ArrayOfSuitableModules = new int[] {6,7,8}
            });
            _context.SaveChanges();

        }

        private void CreateTrainingRequestors()
        {
            Person MarionEmma = _context.People.Add(new Person()
            {
                Name = "Marion Emma",
                Username = "marion.emma",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TrainingRequestorRole.Id
            });
            _context.TrainingRequestors.Add(new TrainingRequestor()
            {
                PersonId = MarionEmma.Id,
                DefaultDepartment = "Software Development",
                JobFunctions = "2"
            });
            _context.SaveChanges();

        }
        private void CreateTcManagers()
        {
            Person RafaelSteward = _context.People.Add(new Person()
            {
                Name = "Rafael Steward",
                Username = "rafael.steward",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TcRole.Id
            });
            _context.TCmanagers.Add(new TCManager()
            {
                PersonId = RafaelSteward.Id,
                DefaultDepartment = "Software Development",
            });
            _context.SaveChanges();

            Person RobLucci = _context.People.Add(new Person()
            {
                Name = "Rob Lucci",
                Username = "rob.lucci",
                Company = "ROSEN GROUP",
                Password = "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3",
                RoleId = TcRole.Id

            });
            _context.TCmanagers.Add(new TCManager()
            {
                PersonId = RobLucci.Id,
                DefaultDepartment = "Software Development",
             });
            _context.SaveChanges();

        }
    }
}
