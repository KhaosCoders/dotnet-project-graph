using System;
using System.IO;
using System.Collections.Generic;
using System.CommandLine;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Options
{    
    internal interface IBuildOptionsBuilder
    {
        IEnumerable<Option> Build();
    }
}