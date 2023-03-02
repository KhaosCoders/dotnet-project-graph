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
            WriteProjectOrder(graph, parameters.OutputFile, parameters.ShowPackages);
        }
        else
        {
            WriteGraph(graph, parameters.OutputFile, parameters.ShowPackages);
        }

        return Task.CompletedTask;
    }

    private void WriteProjectOrder(CSProject graph, string? outputFile, bool showPackages)
    {
        var order = this.orderService.OrderProjects(graph);

        foreach (var project in order)
        {
            project.References.Clear();
        }

        this.outputService.Output(order, outputFile, showPackages);
    }

    private void WriteGraph(CSProject graph, string? outputFile, bool showPackages)
    {
        this.outputService.Output(graph, outputFile, showPackages);
    }
}