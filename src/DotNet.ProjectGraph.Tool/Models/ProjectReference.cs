namespace DotNet.ProjectGraph.Tool.Models;

internal class ProjectReference
{
    public string Name { get; set; }

    public string VersionString { get; set; }

    public string FullPath { get; set; }

    public string OutputType { get; set; }
}
