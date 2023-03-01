using DotNet.ProjectGraph.Tool.Projectgraph.Build.Arguments;
using DotNet.ProjectGraph.Tool.Projectgraph.Build.Options;
using DotNet.ProjectGraph.Tool.Projectgraph.Build.Service;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build;

internal class BuildCommandBuilder : IProjectgraphSubCommandBuilder
{
    private readonly IBuildService _buildService;
    private readonly IBuildOptionsBuilder _optionsBuilder;
    private readonly IBuildArgumentsBuilder _argumentsBuilder;

    public BuildCommandBuilder(IBuildService buildService, IBuildOptionsBuilder optionsBuilder, IBuildArgumentsBuilder argumentsBuilder)
    {
        _buildService = buildService;
        _optionsBuilder = optionsBuilder;
        _argumentsBuilder = argumentsBuilder;
    }

    public Command Build()
    {
        var command = new Command("build", "Builds the graph of dependant projects");
        _optionsBuilder.Build().ToList().ForEach(command.AddOption);
        _argumentsBuilder.Build().ToList().ForEach(command.AddArgument);
        command.Handler = CommandHandler.Create<string?, FileInfo>((projectfile, outputfile) =>
            _buildService.HandleAsync(new BuildParameters(projectfile, outputfile?.FullName)));
        return command;
    }
}