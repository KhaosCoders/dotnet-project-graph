using DotNet.ProjectGraph.Tool.Models;
using System.Collections.ObjectModel;

namespace DotNet.ProjectGraph.Tool.Services;

internal partial interface IBuildOrderService
{
    ReadOnlyCollection<CSProject> OrderProjects(CSProject project);
}
