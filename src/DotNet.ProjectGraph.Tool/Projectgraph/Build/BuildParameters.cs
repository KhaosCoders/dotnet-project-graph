namespace DotNet.ProjectGraph.Tool.Projectgraph.Build;

internal class BuildParameters
{
    public BuildParameters(string? projectfile, string? outputfile)
    {
        ProjectFile = projectfile;
        OutputFile = outputfile;
    }

    public string? ProjectFile { get; }
    public string? OutputFile { get; }
}