using Microsoft.AspNetCore.Identity;
using Modular.Core.Domain.Models;

namespace Modular.Modules.Core.Models
{
    public class Role : IdentityRole<long>, IEntityWithTypedId<long>
    {
        public Role()
        {
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}
