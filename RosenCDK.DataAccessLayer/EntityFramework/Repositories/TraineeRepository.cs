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
    public class TraineeRepository : RosenCDKRepositoryBase<Trainee, int>, ITraineeRepository
    {
        public TraineeRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        public List<Trainee> getTraineesByArrayId(int[] traineeIDs)
        {
            List<Trainee> retList = new List<Trainee>();
            foreach (int Id in traineeIDs)
            {
                retList.Add(Get(Id));
            }
            return retList;
        }

    }
}
