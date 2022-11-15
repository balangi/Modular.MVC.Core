#nullable disable

using Modular.Core;
using System.Reflection;
using System.Runtime.Loader;

namespace Modular.WebHost.Core.Services;

public class ConfigureServices
{
    public ConfigureServices()
    {

    }

    public void LoadInstalledModules(IWebHostEnvironment environment, ref List<ModuleInfo> modules)
    {
        //var moduleRootFolder = new DirectoryInfo(Path.Combine(environment.ContentRootPath, "Modules"));
        var moduleRootFolder = new DirectoryInfo("c:\\Users\\Ali-Home\\source\\repos\\- Test -\\- Modular WebHost -\\Modular WebHost Core\\Modules\\");
        var moduleFolders = moduleRootFolder.GetDirectories();

        foreach (var moduleFolder in moduleFolders)
        {
            var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "Bin\\Debug\\net6.0\\"));
            if (!binFolder.Exists)
            {
                continue;
            }

            foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
            {
                Assembly assembly = null;
                try
                {
                    assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                }
                catch (FileLoadException ex)
                {
                    if (ex.Message == "Assembly with same name is already loaded")
                    {
                        // Get loaded assembly
                        assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));
                    }
                    else
                    {
                        throw;
                    }
                }

                //if (assembly.FullName.Contains(moduleFolder.Name + "\\Debug\\net6.0\\"))
                //{
                    modules.Add(new ModuleInfo { Name = moduleFolder.Name, Assembly = assembly, Path = moduleFolder.FullName });
                //}
            }
        }

        GlobalConfiguration.Modules = modules;
    }
}

