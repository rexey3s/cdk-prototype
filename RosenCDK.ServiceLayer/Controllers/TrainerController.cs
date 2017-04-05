using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abp.WebApi.Controllers;
using NUnit.Framework;
using RosenCDK.BussinessLogics;
using RosenCDK.DTO;

namespace RosenCDK.ServiceLayer.Controllers
{
    /// <summary>
    /// TrainerController provides APIs to work with Trainers data
    /// </summary>
    public class TrainerController : AbpApiController
    {
        public ITrainerAppService _trainerAppService { get; set; }
        //public ITraineeAppService _traineeAppService { get; set; }


        public TraineeDTO Get(int Id)
        {
           return new TraineeDTO();
        }


    }
}