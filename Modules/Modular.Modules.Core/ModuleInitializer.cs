using Modular.Core;
using Modular.Modules.Core.Services;

namespace Modular.Modules.Core
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Init(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISmsSender, SmsSender>();
        }
    }
}
