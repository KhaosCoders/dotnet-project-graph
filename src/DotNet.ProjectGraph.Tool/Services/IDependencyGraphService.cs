using DotNet.ProjectGraph.Tool.Models;

namespace DotNet.ProjectGraph.Tool.Services;

internal interface IDependencyGraphService
{
    CSProject BuildGraph(string? projectFile);
}
