using DotNet.ProjectGraph.Tool.Models;

namespace DotNet.ProjectGraph.Tool.Services;

internal interface IMSBuildService
{
    CSProject ReadProject(string projectPath);
}
