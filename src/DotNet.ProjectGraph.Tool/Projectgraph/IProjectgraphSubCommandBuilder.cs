using System.CommandLine;

namespace DotNet.ProjectGraph.Tool.Projectgraph;

public interface IProjectgraphSubCommandBuilder
{
    Command Build();
}