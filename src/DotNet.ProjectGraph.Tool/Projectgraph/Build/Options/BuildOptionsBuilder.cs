using System.Collections.Generic;
using System.CommandLine;
using System.IO;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Options;

internal class BuildOptionsBuilder : IBuildOptionsBuilder
{
    public IEnumerable<Option> Build()
    {
        yield return new Option<FileInfo?>(new[] { "--output", "-o" }, "Output file (*.json, *.dgml)");
    }
}