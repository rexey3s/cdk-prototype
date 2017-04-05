namespace RosenCDK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables_SeedData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ACTIVITY",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ROLEDISTRIBUTION",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ACTIVITY", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.ROLE", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.ROLE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.COMPETENCE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CONFIGURATION",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JOBFUNCTION",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobFunctionTitle = c.String(nullable: false),
                        RequiredCompetences = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MODULE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompetenciesTrained = c.String(nullable: false),
                        AreaOfObjective = c.String(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Objectives = c.String(nullable: false),
                        TopicsCovered = c.String(nullable: false),
                        Exercises = c.String(nullable: false),
                        Theory = c.Double(nullable: false),
                        Pratical = c.Double(nullable: false),
                        Methods = c.String(),
                        ReferencesDoc = c.String(),
                        ExamInclude = c.Boolean(nullable: false),
                        RoomOrEquipment = c.String(),
                        LearningTransfer = c.String(),
                        ExpirationDate = c.DateTime(),
                        TargetGroup = c.String(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MODULETYPE", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.PERSON", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.TypeId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.MODULETYPE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PERSON",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Company = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RoleId = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ROLE", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PROGRAM",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProgramTitle = c.String(nullable: false),
                        IncludedModules = c.String(nullable: false),
                        NeedByPotentialTrainees = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TRAINING",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProgramId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TotalDuration = c.Double(nullable: false),
                        AssignedTrainees = c.String(nullable: false),
                        AssignedTrainers = c.String(nullable: false),
                        ModuleArrangement = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PROGRAM", t => t.ProgramId, cascadeDelete: true)
                .ForeignKey("dbo.TRAINING_STATUS", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.ProgramId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.TRAINING_STATUS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.T_C_MANAGER",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        DefaultDepartment = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PERSON", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.TRAINEE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        DefaultDepartment = c.String(nullable: false),
                        JobFunctions = c.String(),
                        Competencies = c.String(),
                        TargetedTrainings = c.String(),
                        AttendedTrainings = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PERSON", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.TRAINER",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        IsExternal = c.Boolean(nullable: false),
                        SuitableModules = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PERSON", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.TRAINING_REQUESTOR",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        DefaultDepartment = c.String(nullable: false),
                        JobFunctions = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PERSON", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.USER_TOKEN",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        AuthToken = c.String(nullable: false),
                        IssuedOn = c.DateTime(),
                        ExpiresOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TRAINING_REQUESTOR", "PersonId", "dbo.PERSON");
            DropForeignKey("dbo.TRAINER", "PersonId", "dbo.PERSON");
            DropForeignKey("dbo.TRAINEE", "PersonId", "dbo.PERSON");
            DropForeignKey("dbo.T_C_MANAGER", "PersonId", "dbo.PERSON");
            DropForeignKey("dbo.TRAINING", "StatusId", "dbo.TRAINING_STATUS");
            DropForeignKey("dbo.TRAINING", "ProgramId", "dbo.PROGRAM");
            DropForeignKey("dbo.MODULE", "PersonId", "dbo.PERSON");
            DropForeignKey("dbo.PERSON", "RoleId", "dbo.ROLE");
            DropForeignKey("dbo.MODULE", "TypeId", "dbo.MODULETYPE");
            DropForeignKey("dbo.ROLEDISTRIBUTION", "RoleId", "dbo.ROLE");
            DropForeignKey("dbo.ROLEDISTRIBUTION", "ActivityId", "dbo.ACTIVITY");
            DropIndex("dbo.TRAINING_REQUESTOR", new[] { "PersonId" });
            DropIndex("dbo.TRAINER", new[] { "PersonId" });
            DropIndex("dbo.TRAINEE", new[] { "PersonId" });
            DropIndex("dbo.T_C_MANAGER", new[] { "PersonId" });
            DropIndex("dbo.TRAINING", new[] { "StatusId" });
            DropIndex("dbo.TRAINING", new[] { "ProgramId" });
            DropIndex("dbo.PERSON", new[] { "RoleId" });
            DropIndex("dbo.MODULE", new[] { "PersonId" });
            DropIndex("dbo.MODULE", new[] { "TypeId" });
            DropIndex("dbo.ROLEDISTRIBUTION", new[] { "ActivityId" });
            DropIndex("dbo.ROLEDISTRIBUTION", new[] { "RoleId" });
            DropTable("dbo.USER_TOKEN");
            DropTable("dbo.TRAINING_REQUESTOR");
            DropTable("dbo.TRAINER");
            DropTable("dbo.TRAINEE");
            DropTable("dbo.T_C_MANAGER");
            DropTable("dbo.TRAINING_STATUS");
            DropTable("dbo.TRAINING");
            DropTable("dbo.PROGRAM");
            DropTable("dbo.PERSON");
            DropTable("dbo.MODULETYPE");
            DropTable("dbo.MODULE");
            DropTable("dbo.JOBFUNCTION");
            DropTable("dbo.CONFIGURATION");
            DropTable("dbo.COMPETENCE");
            DropTable("dbo.ROLE");
            DropTable("dbo.ROLEDISTRIBUTION");
            DropTable("dbo.ACTIVITY");
        }
    }
}
