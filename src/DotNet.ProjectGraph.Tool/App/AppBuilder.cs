using Microsoft.Extensions.DependencyInjection;

namespace DotNet.ProjectGraph.Tool.App
{
    public class AppBuilder
    {
        public App Build()
        {
            var services = new ServiceCollection().AddOptions();

            var startup = new Startup();

            startup.ConfigureServices(services);

            return new App(services.BuildServiceProvider());
        }
    }
}