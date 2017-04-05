
using Abp.Domain.Repositories;
using RosenCDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Repositories
{
    public interface ITrainingRepository : IRepository<Training, int>
    {
        List<Training> GetTrainingsByID(int[] trainingID);

        List<Training> GetTrainingsByProgramIdAndStatusId(int programId, int statusId);
    }
}
