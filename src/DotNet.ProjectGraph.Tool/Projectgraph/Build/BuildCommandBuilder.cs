using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using DotNet.ProjectGraph.Tool.Projectgraph.Build.Arguments;
using DotNet.ProjectGraph.Tool.Projectgraph.Build.Options;
using DotNet.ProjectGraph.Tool.Projectgraph.Build.Service;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build;

internal class BuildCommandBuilder : IProjectgraphSubCommandBuilder
{
    private readonly IBuildService _buildService;
    private readonly IBuildOptionsBuilder _optionsBuilder;
    private readonly IBuildArgumentBuilder _argumentBuilder;

    public BuildCommandBuilder(IBuildService buildService, IBuildOptionsBuilder optionsBuilder, IBuildArgumentBuilder argumentBuilder)
    {
        _buildService = buildService;
        _optionsBuilder = optionsBuilder;
        _argumentBuilder = argumentBuilder;
    }

    public Command Build()
    {
        var command = new Command("build", "Builds the graph of dependant projects");
        _optionsBuilder.Build().ToList().ForEach(option => command.AddOption(option));
        command.AddArgument(_argumentBuilder.Build());
        command.Handler = CommandHandler.Create<object, bool>((projectfile, output) => _buildService.HandleAsync(new BuildParameters(projectfile, output)));
        return command;
    }
}