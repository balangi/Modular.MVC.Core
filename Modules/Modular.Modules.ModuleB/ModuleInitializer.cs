using Modular.Core;
using Modular.Core.Domain;
using Modular.Modules.ModuleB.Models;

namespace Modular.Modules.ModuleB
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Init(IServiceCollection services)
        {
            services.AddTransient<IRepository<Sample>, SampleRepository>();
        }
    }
}
