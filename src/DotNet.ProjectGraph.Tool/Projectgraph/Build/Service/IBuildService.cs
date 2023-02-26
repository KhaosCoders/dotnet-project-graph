using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Service;

internal interface IBuildService
{
    Task HandleAsync(BuildParameters parameters);
}