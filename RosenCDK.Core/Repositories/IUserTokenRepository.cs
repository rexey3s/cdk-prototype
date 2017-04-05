using Abp.Domain.Repositories;
using RosenCDK.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RosenCDK.Repositories
{
    public interface IUserTokenRepository : IRepository<UserToken, int>
    {
        UserToken GetTokenByAuthToken(string authToken);
        UserToken GetTokenByUsername(string username);
    }
}
