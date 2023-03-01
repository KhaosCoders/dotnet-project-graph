using System.Collections.Generic;
using System.CommandLine;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Arguments;

internal interface IBuildArgumentsBuilder
{
    IEnumerable<Argument> Build();
}