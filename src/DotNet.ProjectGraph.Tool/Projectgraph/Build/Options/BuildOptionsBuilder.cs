using System;
using System.IO;
using System.Collections.Generic;
using System.CommandLine; 

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Options
{    
    internal class BuildOptionsBuilder : IBuildOptionsBuilder
    {
        public IEnumerable<Option> Build()
        {   
            yield return BuildOutputOption();
        }

        private Option BuildOutputOption()
        {
            return new Option(new[] { "--output", "-o" }, "Output format")
            {
                Required = true
            };
        }                                       
    }
}