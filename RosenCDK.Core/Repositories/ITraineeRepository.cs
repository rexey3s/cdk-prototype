using Abp.Domain.Repositories;
using RosenCDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace RosenCDK.Repositories
{
    public interface ITraineeRepository : IRepository<Trainee, int>
    { 
        List<Trainee> getTraineesByArrayId(int[] traineeIDs);
    }
}
