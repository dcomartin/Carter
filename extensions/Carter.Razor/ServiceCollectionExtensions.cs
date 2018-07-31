namespace Carter.Razor
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.ObjectPool;
    using Microsoft.Extensions.PlatformAbstractions;

    public static class ServiceCollectionExtensions
    {
        public static void AddRazor(this IServiceCollection services)
        {
            if (services.Any(x => x.ServiceType == typeof(IRazorViewEngine)) == false)
            {            
                var applicationEnvironment = PlatformServices.Default.Application;
                services.AddSingleton(applicationEnvironment);

                var appDirectory = Directory.GetCurrentDirectory();

                var environment = new HostingEnvironment
                {
                    ApplicationName = Assembly.GetEntryAssembly().GetName().Name
                };
                services.AddSingleton<IHostingEnvironment>(environment);

                services.Configure<RazorViewEngineOptions>(options =>
                {
                    options.FileProviders.Clear();
                    options.FileProviders.Add(new PhysicalFileProvider(appDirectory));
                });

                services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();

                var diagnosticSource = new DiagnosticListener("Microsoft.AspNetCore");
                services.AddSingleton<DiagnosticSource>(diagnosticSource);

                services.AddLogging();
                services.AddMvc();
            }
            
            services.AddSingleton<RazorViewToStringRenderer>();
        }
    }
}
