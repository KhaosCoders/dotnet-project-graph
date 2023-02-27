using System.Collections.Generic;

namespace DotNet.ProjectGraph.Tool.Models;

internal class CSProject : ProjectReference
{
    public List<NugetPackage> Packages { get; set; }

    public List<ProjectReference> References { get; set; }
}
