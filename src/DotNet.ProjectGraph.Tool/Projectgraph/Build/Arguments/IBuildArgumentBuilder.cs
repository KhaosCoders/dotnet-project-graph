using System;
using System.IO;
using System.CommandLine;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Arguments
{    
    internal interface IBuildArgumentBuilder
    {
        Argument Build();
    }
}