using DotNet.ProjectGraph.Tool.ErrorHandling;
using DotNet.ProjectGraph.Tool.Projectgraph;
using DotNet.ProjectGraph.Tool.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet.ProjectGraph.Tool;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IErrorHandler, ErrorHandler>();
        services.AddSingleton<IConsoleService, ConsoleService>();
        services.AddSingleton<IMSBuildService, MSBuildService>();
        services.AddSingleton<IProjectgraphCommandBuilder, ProjectgraphCommandBuilder>();

        ConfigureBuild(services);
    }

    private static void ConfigureBuild(IServiceCollection services)
    {
        services.AddSingleton<Projectgraph.Build.Arguments.IBuildArgumentBuilder, Projectgraph.Build.Arguments.BuildArgumentBuilder>();
        services.AddSingleton<Projectgraph.Build.Options.IBuildOptionsBuilder, Projectgraph.Build.Options.BuildOptionsBuilder>();
        services.AddSingleton<IProjectgraphSubCommandBuilder, Projectgraph.Build.BuildCommandBuilder>();
        services.AddSingleton<Projectgraph.Build.Service.IBuildService, Projectgraph.Build.Service.BuildService>();
    }
}