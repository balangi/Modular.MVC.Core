using Microsoft.EntityFrameworkCore;
using Modular.Core;
using Modular.Modules.ModuleA.Models;

namespace Modular.Modules.ModuleA.Infrastructure
{
    public class ModuleACustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sample>()
                .Property(x => x.Name).HasColumnName("TestName");
        }
    }
}
