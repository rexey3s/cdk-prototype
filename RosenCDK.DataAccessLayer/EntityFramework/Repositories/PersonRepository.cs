using RosenCDK.Entities;
using RosenCDK.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using RosenCDK.EntityFramework.Repositories;

namespace RosenCDK.EntityFramework.Repositories
{
    public class PersonRepository : RosenCDKRepositoryBase<Person, int>, IPersonRepository
    {
        public PersonRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Person GetWithUsernameAndPassword(string username, string password)
        {
            var query =  Context.People.Where(
                            person => person.Username == username && person.Password == password);         
            return query.FirstOrDefault(); 
        }
        public Person GetPersonByUsername(string username)
        {
            var query = Context.People.Where(
                            person => person.Username == username);
            return query.FirstOrDefault();

        }

    }
}
