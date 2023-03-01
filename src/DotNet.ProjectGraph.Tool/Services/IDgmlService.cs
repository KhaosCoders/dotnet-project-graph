using DotNet.ProjectGraph.Tool.Models;

namespace DotNet.ProjectGraph.Tool.Services;

internal interface IDgmlService
{
    string GenerateDgml(CSProject project);
}
