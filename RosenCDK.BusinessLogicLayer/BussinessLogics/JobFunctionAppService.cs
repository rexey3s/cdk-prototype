using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosenCDK.DTO;
using RosenCDK.Repositories;
using RosenCDK.Entities;

namespace RosenCDK.BussinessLogics
{
    public class JobFunctionAppService : RosenCDKAppServiceBase, IJobFunctionAppService
    {
        private readonly IJobFunctionRepository _jobFunctionRepository;

        public JobFunctionAppService(IJobFunctionRepository jobFunctionRepo)
        {
            _jobFunctionRepository = jobFunctionRepo;
        }

        public ListJobFunctionDTO GetAllJobFunctions()
        {
            List<JobFunction> JobFunctionList = _jobFunctionRepository.GetAllList();

            List<JobFunctionDTO> jobFunctionDtos = new List<JobFunctionDTO>();
            foreach (JobFunction jobFunction in JobFunctionList)
            {
                JobFunctionDTO jobFunctionDto = new JobFunctionDTO();

                jobFunctionDto.JobFunctionID = jobFunction.Id;
                jobFunctionDto.JobFunctionTitle = jobFunction.JobFunctionTitle;
                jobFunctionDto.RequiredCompetencies = jobFunction.ArrayOfCompetencies.ToList();

                jobFunctionDtos.Add(jobFunctionDto);
            }

            ListJobFunctionDTO listJobFunctionDto = new ListJobFunctionDTO();
            listJobFunctionDto.JobFunctionList = jobFunctionDtos;
            return listJobFunctionDto;
        }

        public JobFunctionDTO GetJobFunctionById(int jobFunctionId)
        {
            JobFunctionDTO jobFunction = new JobFunctionDTO();

            var jobFunctionEntity = _jobFunctionRepository.FirstOrDefault(jobFunctionId);
            if (jobFunctionEntity == null) return null;

            jobFunction.JobFunctionID = jobFunctionEntity.Id;
            jobFunction.JobFunctionTitle = jobFunctionEntity.JobFunctionTitle;
            jobFunction.RequiredCompetencies = jobFunctionEntity.ArrayOfCompetencies.ToList();

            return jobFunction;
        }
    }
}
