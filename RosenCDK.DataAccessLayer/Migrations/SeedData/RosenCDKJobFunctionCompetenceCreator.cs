using RosenCDK.Entities;
using RosenCDK.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Migrations.SeedData
{
    class RosenCDKJobFunctionCompetenceCreator
    {
        private readonly RosenCDKDbContext _context;
        public RosenCDKJobFunctionCompetenceCreator(RosenCDKDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateCompetencesAndJobFunctions();
        }
       

        private void CreateCompetencesAndJobFunctions()
        {
            Competence OOPComp1 = _context.Competences.Add(new Competence()
            {
                Name = "Object-Oriented Programming lv1",
                Description = "Basic knowledge in Object-Orient Programming."
            });
            Competence designPatternComp1 = _context.Competences.Add(new Competence()
            {
                Name = "Design Pattern lv1",
                Description = "Basic knowledge in Software Design Patterns"
            });
            Competence designPatternComp2 = _context.Competences.Add(new Competence()
            {
                Name = "Design Pattern lv2",
                Description = "Strong knowledge in Software Design Patterns"
            });
            Competence softArchComp1 = _context.Competences.Add(new Competence()
            {
                Name = "Software Development Process lv1",
                Description = "Basic knowledge in Software development models: scrum, waterfall,..."
            });
            Competence softArchComp2 = _context.Competences.Add(new Competence()
            {
                Name = "Software Development Process lv2",
                Description = "Strong experiences in Software development models: scrum, waterfall,..."
            });
            Competence htmlcssComp1 = _context.Competences.Add(new Competence()
            {
                Name = "HTML5/CSS3/JS lv1",
                Description = "Familiar with well-known front-end Web technologies like HTML5/CSS3/JavaScript",
            });
            Competence htmlcssComp2 = _context.Competences.Add(new Competence()
            {
                Name = "HTML5/CSS3/JS lv2",
                Description = "Strong experiences with well-known front-end Web technologies like HTML5/CSS3/JavaScript",
            });
            
            Competence dbComp1 = _context.Competences.Add(new Competence()
            {
                Name = "Database Architecture/Design lv1",
                Description = "Basic skills in designing relational database",
            });
            Competence dbComp2 = _context.Competences.Add(new Competence()
            {
                Name = "Database Architecture/Design lv2",
                Description = "Advanced skills in designing relational database",
            });

            Competence javaEEcomp1 = _context.Competences.Add(new Competence()
            {
                Name = "Java EE",
                Description = "Strong experiences in Java EE Technologies",
            });
            Competence aspNetcomp1 = _context.Competences.Add(new Competence()
            {
                Name = "ASP.NET",
                Description = "Strong experiences in  ASP.NET Technologies",
            });
            Competence unityComp = _context.Competences.Add(new Competence()
            {
                Name = "Unity Framework",
                Description = "Basic Knowledge with Unity Framework",
            });
            Competence networkComp = _context.Competences.Add(new Competence()
            {
                Name = "Advanced Network Specialist",
                Description = "Strong network",
            });
            Competence linuxComp = _context.Competences.Add(new Competence()
            {
                Name = "Linux lv1",
                Description = "Familiar with Linux OS",
            });
            Competence winServerComp = _context.Competences.Add(new Competence()
            {
                Name = "Window Server lv1",
                Description = "Familiar with Window Server OS",
            });
            Competence leaderShip = _context.Competences.Add(new Competence()
            {
                Name = "Leadership Competence",
                Description = "Have experience in leading teamwork projects",
            });
            Competence communication = _context.Competences.Add(new Competence()
            {
                Name = "Social communication",
                Description = "Strong communication skills"
            });
            _context.SaveChanges();

            Competence problemSolvingSkill = _context.Competences.Add(new Competence()
            {
                Name = "Problem Solving skill",
                Description = "Be able to think critically, view thing at different prespectives ..."
            });
            Competence presentationSkill = _context.Competences.Add(new Competence()
            {
                Name = "Presentation skill",
                Description = "Be confident to give an oral presentation in front of many people"
            });
            _context.SaveChanges();

            _context.JobFunctions.Add(new JobFunction()
            {
                JobFunctionTitle = "Database Designer",
                ArrayOfCompetencies = new int [] {dbComp1.Id, dbComp2.Id, winServerComp.Id, problemSolvingSkill.Id}
            });
            _context.JobFunctions.Add(new JobFunction()
            {
                JobFunctionTitle = "Software Architect",
                ArrayOfCompetencies = new int[] { presentationSkill.Id,problemSolvingSkill.Id,designPatternComp2.Id, softArchComp2.Id, leaderShip.Id}
            });
            _context.JobFunctions.Add(new JobFunction()
            {
                JobFunctionTitle = "Frontend Developer",
                ArrayOfCompetencies = new int[] { problemSolvingSkill.Id, htmlcssComp2.Id, OOPComp1.Id}
            });
            _context.JobFunctions.Add(new JobFunction()
            {
                JobFunctionTitle = ".Net Backend Developer",
                ArrayOfCompetencies = new int[] { problemSolvingSkill.Id, networkComp.Id, javaEEcomp1.Id, designPatternComp1.Id }
            });
            _context.JobFunctions.Add(new JobFunction()
            {
                JobFunctionTitle = "Java Backend Developer",
                ArrayOfCompetencies = new int[] { problemSolvingSkill.Id,networkComp.Id, aspNetcomp1.Id, designPatternComp1.Id }
            });
            _context.JobFunctions.Add(new JobFunction()
            {
                JobFunctionTitle = "System Administrator",
                ArrayOfCompetencies = new int[] { linuxComp.Id, winServerComp.Id, networkComp.Id, OOPComp1.Id}
            });
            _context.JobFunctions.Add(new JobFunction()
            {
                JobFunctionTitle = "UI/UX Developer",
                ArrayOfCompetencies = new int[] { htmlcssComp1.Id, communication.Id, presentationSkill.Id}
            });
            _context.SaveChanges();
        }
        
    }
}
