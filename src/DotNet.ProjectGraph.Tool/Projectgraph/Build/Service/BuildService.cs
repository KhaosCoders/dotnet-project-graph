using DotNet.ProjectGraph.Tool.Services;
using System.Threading.Tasks;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Service;

internal class BuildService : IBuildService
{
    private readonly IOutputService outputService;
    private readonly IDependencyGraphService dependencyTreeService;

    public BuildService(IOutputService outputService, IDependencyGraphService dependencyTreeService)
    {
        this.outputService = outputService;
        this.dependencyTreeService = dependencyTreeService;
    }

    public Task HandleAsync(BuildParameters parameters)
    {
        var graph = this.dependencyTreeService.BuildGraph(parameters.ProjectFile);
        if (!string.IsNullOrWhiteSpace(parameters.OutputFile))
        {
            this.outputService.OutputToFile(graph, parameters.OutputFile);
        }
        else
        {
            this.outputService.OutputToConsole(graph);
        }

        return Task.CompletedTask;
    }
}