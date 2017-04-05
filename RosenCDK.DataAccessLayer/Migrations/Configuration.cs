using rosencdk.Migrations.SeedData;
using RosenCDK.Migrations.SeedData;
using System.Data.Entity.Migrations;

namespace RosenCDK.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<RosenCDK.EntityFramework.RosenCDKDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RosenCDK";
        }

        protected override void Seed(RosenCDK.EntityFramework.RosenCDKDbContext context)
        {
            // This method will be called every time after migrating to the latest version.
            // You can add any seed data here...
            new RosenCDKRoleAndActivityAndPeopleCreator(context).Create();
            new RosenCDKJobFunctionCompetenceCreator(context).Create();
            new RosenCDKTrainingProgramCreator(context).Create();
            context.SaveChanges();
        }
    }
}
