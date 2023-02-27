namespace DotNet.ProjectGraph.Tool.Models;

internal struct NugetPackage
{
    public string Name { get; set; }
    public string Version { get; set; }

    public NugetPackage(string name, string version) : this()
    {
        Name = name;
        Version = version;
    }
}
