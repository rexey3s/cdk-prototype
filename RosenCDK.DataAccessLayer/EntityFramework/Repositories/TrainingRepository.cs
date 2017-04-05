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
    public class TrainingRepository : RosenCDKRepositoryBase<Training, int>, ITrainingRepository
    {
        public TrainingRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<Training> GetTrainingsByID(int[] trainingID)
        {
            List<Training> retList = new List<Training>();
            foreach (int Id in trainingID)
            {
                retList.Add(Get(Id));
            }
            return retList;
        }

        public List<Training> GetTrainingsByProgramIdAndStatusId(int programId, int statusId)
        {
            var query = Context.Trainings.Where(
                training => training.ProgramId == programId && training.StatusId == statusId); ;
            return query.ToList();
        }
    }
}
