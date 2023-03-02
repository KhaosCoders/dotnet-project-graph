using DotNet.ProjectGraph.Tool.Models;
using System.Collections.Generic;

namespace DotNet.ProjectGraph.Tool.Services;

internal interface IOutputService
{
    void Output(CSProject graph, string? outputFile, bool showPackages);
    void Output(IReadOnlyCollection<CSProject> order, string? outputFile, bool showPackages);
}
