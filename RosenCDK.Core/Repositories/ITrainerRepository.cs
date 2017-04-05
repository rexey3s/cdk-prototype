using Abp.Domain.Repositories;
using RosenCDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Repositories
{
    public interface ITrainerRepository : IRepository<Trainer, int>
    {
        List<Trainer> GetTrainersByArrayId(int[] trainerIDs);

        /**
         * IRepository.GetAll() đã có sẵn -> tái sử dụng lại 
         **/
        //List<Trainer> GetAllTrainers();
    }
}
