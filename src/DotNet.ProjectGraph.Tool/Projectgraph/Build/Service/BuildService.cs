using DotNet.ProjectGraph.Tool.Models;
using DotNet.ProjectGraph.Tool.Services;
using System.Threading.Tasks;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Service;

internal class BuildService : IBuildService
{
    private readonly IOutputService outputService;
    private readonly IDependencyGraphService dependencyTreeService;
    private readonly IBuildOrderService orderService;

    public BuildService(
        IOutputService outputService,
        IDependencyGraphService dependencyTreeService,
        IBuildOrderService orderService)
    {
        this.outputService = outputService;
        this.dependencyTreeService = dependencyTreeService;
        this.orderService = orderService;
    }

    public Task HandleAsync(BuildParameters parameters)
    {
        var graph = this.dependencyTreeService.BuildGraph(parameters.ProjectFile);

        if (parameters.OrderProjects)
        {
            WriteProjectOrder(graph, parameters.OutputFile);
        }
        else
        {
            WriteGraph(graph, parameters.OutputFile);
        }

        return Task.CompletedTask;
    }

    private void WriteProjectOrder(CSProject graph, string? outputFile)
    {
        var order = this.orderService.OrderProjects(graph);

    }

    private void WriteGraph(CSProject graph, string? outputFile)
    {
        if (!string.IsNullOrWhiteSpace(outputFile))
        {
            this.outputService.OutputToFile(graph, outputFile);
        }
        else
        {
            this.outputService.OutputToConsole(graph);
        }
    }
}