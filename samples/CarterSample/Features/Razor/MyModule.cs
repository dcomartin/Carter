namespace CarterSample.Features.Razor
{
    using Carter;
    using Carter.Razor;

    public class MyModule : CarterModule
    {
        public MyModule()
        {
            this.Get("/razor", async (req, res, routeData) =>
            {
                res.StatusCode = 200;
                await res.FromRazor("/Features/Razor/MyView.cshtml", new MyModel());
            });
        }
    }
}
