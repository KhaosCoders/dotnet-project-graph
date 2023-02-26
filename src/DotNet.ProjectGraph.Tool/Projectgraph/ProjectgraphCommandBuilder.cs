using System.Collections.Generic;
using System.CommandLine;
using System.Linq;

namespace DotNet.ProjectGraph.Tool.Projectgraph
{
    public class ProjectgraphCommandBuilder : IProjectgraphCommandBuilder
    {
        private readonly IEnumerable<IProjectgraphSubCommandBuilder> _projectgraphSubCommandBuilders;

        public ProjectgraphCommandBuilder(IEnumerable<IProjectgraphSubCommandBuilder> projectgraphSubCommandBuilders)
        {
            _projectgraphSubCommandBuilders = projectgraphSubCommandBuilders;
        }

        public RootCommand Build()
        {
            var rootCommand = new RootCommand
            {
                Name = "projectgraph",
                Description = @"Run 'projectgraph [command] --help' in order to get specific information."
            };

            _projectgraphSubCommandBuilders.ToList().ForEach(builder => rootCommand.AddCommand(builder.Build()));
            return rootCommand;
        }
    }
}