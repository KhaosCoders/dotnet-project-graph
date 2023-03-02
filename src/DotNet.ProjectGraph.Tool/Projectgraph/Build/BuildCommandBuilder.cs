using DotNet.ProjectGraph.Tool.Projectgraph.Build.Service;
using System.CommandLine;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build;

internal class BuildCommandBuilder : IProjectgraphSubCommandBuilder
{
    private readonly IBuildService _buildService;

    public BuildCommandBuilder(IBuildService buildService)
    {
        _buildService = buildService;
    }

    public Command Build()
    {
        var projectFileArgument = new Argument<string>()
        {
            Name = "projectfile",
            Description = "Input project file (*.csproj)"
        };

        var outputOption = new Option<string?>("--output", "Output file (*.json, *.dgml)");
        var orderOption = new Option<bool>("--order", "Order projects by dependency");
        var nugetOption = new Option<bool>("--packages", "Also show Nuget packages");

        var command = new Command("build", "Builds the graph of dependant projects"){
            projectFileArgument,
            outputOption,
            orderOption,
            nugetOption
        };

        command.SetHandler((projectfile, outputfile, orderProjects, showPackages) =>
            _buildService.HandleAsync(new BuildParameters(projectfile, outputfile, orderProjects, showPackages)),
            projectFileArgument,
            outputOption,
            orderOption,
            nugetOption);

        return command;
    }
}