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
    public class UserTokenRepository : RosenCDKRepositoryBase<UserToken, int>, IUserTokenRepository
    {
        public UserTokenRepository(IDbContextProvider<RosenCDKDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public UserToken GetTokenByAuthToken(string authToken)
        {
            return FirstOrDefault(token => token.AuthToken == authToken);
        }

        public UserToken GetTokenByUsername(string username)
        {
            return FirstOrDefault(token => token.Username == username);
        }
    }
}
