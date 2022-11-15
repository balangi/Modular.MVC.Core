using Modular.Core.Domain;
using Modular.Core.Domain.Models;

namespace Modular.Modules.Core.Infrastructure
{
    public class Repository<T> : RepositoryWithTypedId<T, long>, IRepository<T>
        where T : class, IEntityWithTypedId<long>
    {
        public Repository(ModularDbContext context) : base(context)
        {
        }
    }
}
