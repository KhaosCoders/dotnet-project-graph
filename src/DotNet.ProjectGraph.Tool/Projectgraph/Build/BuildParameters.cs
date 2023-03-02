namespace DotNet.ProjectGraph.Tool.Projectgraph.Build;

internal record BuildParameters(
                    string? ProjectFile,
                    string? OutputFile,
                    bool OrderProjects,
                    bool ShowPackages);