using System;
using System.IO;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build;

internal class BuildParameters
{
    public BuildParameters(object projectfile, bool output)
    {
        Projectfile = projectfile;
        Output = output;
    }

    public object Projectfile { get; }
    public bool Output { get; }
}