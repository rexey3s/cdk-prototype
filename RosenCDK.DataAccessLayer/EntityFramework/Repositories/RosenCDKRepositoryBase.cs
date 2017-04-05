using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace RosenCDK.EntityFramework.Repositories
{
    public abstract class RosenCDKRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<RosenCDKDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected RosenCDKRepositoryBase(IDbContextProvider<RosenCDKDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class RosenCDKRepositoryBase<TEntity> : RosenCDKRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected RosenCDKRepositoryBase(IDbContextProvider<RosenCDKDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
