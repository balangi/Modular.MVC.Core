using ExtraDepenencyTest;
using Modular.Core;
using Modular.Core.Domain;
using Modular.Modules.ModuleA.Models;
using Modular.Modules.ModuleA.Services;

namespace Modular.Modules.ModuleA
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Init(IServiceCollection services)
        {

            services.AddTransient<IAnotherTestService, AnotherTestService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IRepository<Sample>, SampleRepository>();
        }
    }
}
