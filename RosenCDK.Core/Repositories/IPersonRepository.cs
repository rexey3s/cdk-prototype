using Abp.Domain.Repositories;
using RosenCDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Repositories
{
    public interface IPersonRepository : IRepository<Person, int>
    {
        Person GetWithUsernameAndPassword(string username, string password);
        
        /// <summary>
        /// Get Person infor by input : username 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Person GetPersonByUsername(string username);

    }
}
