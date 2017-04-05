using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using RosenCDK.Entities;
using RosenCDK.Repositories;

namespace RosenCDK.EntityFramework.Repositories
{
    public class CompetenceRepository : RosenCDKRepositoryBase<Competence,int>, ICompetenceRepository
    {
        public CompetenceRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<Competence> GetCompetenciesByCompetenceID(int[] competenceID)
        {
            List<Competence> retList = new List<Competence>();
            foreach(int Id in competenceID)
            {
                var competence = FirstOrDefault(Id);
                if (competence != null)
                {
                    retList.Add(competence);
                }
            }
            return retList;
        }

    }
}
