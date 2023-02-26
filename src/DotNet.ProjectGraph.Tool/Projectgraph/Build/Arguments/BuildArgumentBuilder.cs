using System;
using System.IO;
using System.CommandLine;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Arguments
{    
    internal class BuildArgumentBuilder : IBuildArgumentBuilder
    {                                        
        public Argument Build()
        {
            var argument = new Argument<object>()
            {
                Name = "projectfile",
                Description = "Input project file (*.csproj or *.sln)"
            };
            
            return argument;
        }
    }
}