using Abp.EntityFramework;
using RosenCDK.Entities;
using System.Data.Entity;

namespace RosenCDK.EntityFramework
{
    public class RosenCDKDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        //public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<Person> People { get; set; }
        public virtual IDbSet<Trainee> Trainees { get; set; }
        public virtual IDbSet<Trainer> Trainers { get; set; }
        public virtual IDbSet<TrainingRequestor> TrainingRequestors { get; set; }
        public virtual IDbSet<TCManager> TCmanagers { get; set; }
        public virtual IDbSet<Module> Modules { get; set; }
        public virtual IDbSet<ModuleType> ModuleTypes { get; set; }
        public virtual IDbSet<Program> Programs { get; set; }
        public virtual IDbSet<Training> Trainings { get; set; }
        public virtual IDbSet<TrainingStatus> TrainingStatuses { get; set; }

        public virtual IDbSet<UserToken> UserTokens { get; set; }
        public virtual IDbSet<Role> Roles { get; set; }
        public virtual IDbSet<Activity> Activities { get; set; }
        public virtual IDbSet<RoleDistribution> RoleDistributions { get; set; }
        
        public virtual IDbSet<Competence> Competences { get; set; }
        public virtual IDbSet<JobFunction> JobFunctions { get; set; }
        public virtual IDbSet<Configuration> Configurations { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public RosenCDKDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in RosenCDKDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of RosenCDKDbContext since ABP automatically handles it.
         */
        public RosenCDKDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
    }
}
