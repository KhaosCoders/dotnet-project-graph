namespace DotNet.ProjectGraph.Tool.Models;

internal class ProjectReference
{
    public string Name { get; set; }

    public string Version { get; set; }

    public string FullPath { get; set; }

    public string OutputType { get; set; }

    public ProjectReference(
        string name,
        string version,
        string fullPath,
        string outputType = "")
    {
        Name = name;
        Version = version;
        FullPath = fullPath;
        OutputType = outputType;
    }
}
