using System.Collections.Generic;

namespace DotNet.ProjectGraph.Tool.Models;

internal class CSProject : ProjectReference
{
    public CSProject(string name,
                     string version,
                     string fullPath,
                     string outputType = "") :
        base(name, version, fullPath, outputType)
    {
        Packages = new();
        References = new();
    }

    public List<NugetPackage> Packages { get; set; }

    public List<ProjectReference> References { get; set; }
}
