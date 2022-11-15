#nullable disable

using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Modular.Core;
using Modular.Core.Domain;
using Modular.Modules.Core.Infrastructure;
using Modular.Modules.Core.Models;
using Modular.WebHost.Core.Extensions;
using Modular.WebHost.Core.Services;
using System.Data;

var modules = new List<ModuleInfo>();

var builder = WebApplication.CreateBuilder(args);

new ConfigureServices().LoadInstalledModules(builder.Environment, ref modules);

builder.Services.AddDbContext<ModularDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Modular.WebHost")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ModularDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationExpanders.Add(new ModuleViewLocationExpander());
});

var mvcBuilder = builder.Services.AddMvc();
var moduleInitializerInterface = typeof(IModuleInitializer);
foreach (var module in modules)
{
    // Register controller from modules
    mvcBuilder.AddApplicationPart(module.Assembly);

    // Register dependency in modules
    var moduleInitializerType = module.Assembly.GetTypes().Where(x => typeof(IModuleInitializer).IsAssignableFrom(x)).FirstOrDefault();
    if (moduleInitializerType != null && moduleInitializerType != typeof(IModuleInitializer))
    {
        var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
        moduleInitializer.Init(builder.Services);
    }
}

// TODO: break down to new method in new class
var builder2 = new ContainerBuilder();
builder2.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
builder2.RegisterGeneric(typeof(RepositoryWithTypedId<,>)).As(typeof(IRepositoryWithTypedId<,>));

foreach (var module in GlobalConfiguration.Modules)
{
    builder2.RegisterAssemblyTypes(module.Assembly).AsImplementedInterfaces();
}

builder2.RegisterInstance(builder.Configuration);
builder2.RegisterInstance(builder.Environment);
// builder2.Populate(builder.Services);
// var container = builder2.Build();
// return container.Resolve<IServiceProvider>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Serving static file for modules
foreach (var module in modules)
{
    var wwwrootDir = new DirectoryInfo(Path.Combine(module.Path, "wwwroot"));
    if (!wwwrootDir.Exists)
    {
        continue;
    }

    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(wwwrootDir.FullName),
        RequestPath = new PathString("/" + module.ShortName)
    });
}

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
