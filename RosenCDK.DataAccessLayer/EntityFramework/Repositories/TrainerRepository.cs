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
    public class TrainerRepository : RosenCDKRepositoryBase<Trainer,int>, ITrainerRepository
    {
        public TrainerRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<Trainer> GetTrainersByArrayId(int[] trainerIDs)
        {
            List<Trainer> retList = new List<Trainer>();
            foreach (int Id in trainerIDs)
            {
                var trainer = FirstOrDefault(Id);
                if (trainer != null)
                {
                    retList.Add(trainer);
                } 
            }
            return retList;
        }
    }
}
