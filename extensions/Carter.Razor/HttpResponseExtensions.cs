namespace Carter.Razor
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public static class HttpResponseExtensions
    {
        public static async Task FromRazor(this HttpResponse res, string view, object model)
        {
            var renderer = res.HttpContext
                .RequestServices
                .GetService<RazorViewToStringRenderer>();

            await res.WriteAsync(await renderer.RenderViewToStringAsync(view, model));
        }
    }
}
