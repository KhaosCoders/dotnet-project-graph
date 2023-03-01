using DotNet.ProjectGraph.Tool.Models;

namespace DotNet.ProjectGraph.Tool.Services;

internal interface IProjectReferenceResolver
{
    CSProject Resolve(CSProject project);
    CSProject Resolve(ProjectReference rootProject, string projectDir);
}
