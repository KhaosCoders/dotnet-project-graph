namespace DotNet.ProjectGraph.Tool.Projectgraph.Build;

internal class BuildParameters
{
    public BuildParameters(string? projectfile, bool output)
    {
        Projectfile = projectfile;
        Output = output;
    }

    public string? Projectfile { get; }
    public bool Output { get; }
}