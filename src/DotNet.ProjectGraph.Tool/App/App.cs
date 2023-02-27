using DotNet.ProjectGraph.Tool.ErrorHandling;
using DotNet.ProjectGraph.Tool.Projectgraph;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet.ProjectGraph.Tool.App;

public class App
{
    public App(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;

    }

    public IServiceProvider ServiceProvider { get; }

    public Task<int> RunAsync(string[] args)
    {
        var rootCommand = ServiceProvider.GetRequiredService<IProjectgraphCommandBuilder>().Build();
        var errorHandler = ServiceProvider.GetRequiredService<IErrorHandler>();

        var commandLineBuilder = new CommandLineBuilder(rootCommand);

        commandLineBuilder.UseMiddleware(errorHandler.HandleErrors);
        commandLineBuilder.UseDefaults();

        var parser = commandLineBuilder.Build();

        var option = parser.Configuration.RootCommand.Options.Single(o => o.Name == "version") as Option;
        option?.AddAlias("-v");

        return parser.InvokeAsync(args);
    }
}