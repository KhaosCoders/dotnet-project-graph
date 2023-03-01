using System.Collections.Generic;
using System.CommandLine;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Arguments;

internal class BuildArgumentsBuilder : IBuildArgumentsBuilder
{
    public IEnumerable<Argument> Build()
    {
        yield return new Argument<string>()
        {
            Name = "projectfile",
            Description = "Input project file (*.csproj)"
        };
    }
}