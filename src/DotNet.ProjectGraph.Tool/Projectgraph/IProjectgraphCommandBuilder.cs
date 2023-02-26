using System.CommandLine;

namespace DotNet.ProjectGraph.Tool.Projectgraph;

public interface IProjectgraphCommandBuilder
{
    RootCommand Build();
}