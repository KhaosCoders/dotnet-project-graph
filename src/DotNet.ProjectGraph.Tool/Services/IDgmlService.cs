using DotNet.ProjectGraph.Tool.Models;
using System.Collections.Generic;

namespace DotNet.ProjectGraph.Tool.Services;

internal interface IDgmlService
{
    string GenerateDgmlForGraph(CSProject project, bool showPackages);
    string GenerateDgmlForOrder(IReadOnlyCollection<CSProject> order, bool showPackages);
}
