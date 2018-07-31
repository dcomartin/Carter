namespace CarterAndMVC
{
    using Carter;
    using Carter.Razor;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCarter();
            services.AddMvc();
            services.AddRazor();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCarter();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
