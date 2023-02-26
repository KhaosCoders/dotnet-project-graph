using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using DotNet.ProjectGraph.Tool.Projectgraph;
using DotNet.ProjectGraph.Tool.Services;
using DotNet.ProjectGraph.Tool.ErrorHandling;    
using DotNet.ProjectGraph.Tool.Projectgraph.Build;
using DotNet.ProjectGraph.Tool.Projectgraph.Build.Arguments;
using DotNet.ProjectGraph.Tool.Projectgraph.Build.Options;
using DotNet.ProjectGraph.Tool.Projectgraph.Build.Service;

namespace DotNet.ProjectGraph.Tool
{    
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddSingleton<IErrorHandler, ErrorHandler>();
            services.AddSingleton<IConsoleService, ConsoleService>();
            services.AddSingleton<IProjectgraphCommandBuilder, ProjectgraphCommandBuilder>();
            
            ConfigureBuild(services);
        }

        private static void ConfigureBuild(IServiceCollection services)
        {
            services.AddSingleton<DotNet.ProjectGraph.Tool.Projectgraph.Build.Arguments.IBuildArgumentBuilder, DotNet.ProjectGraph.Tool.Projectgraph.Build.Arguments.BuildArgumentBuilder>();
            services.AddSingleton<DotNet.ProjectGraph.Tool.Projectgraph.Build.Options.IBuildOptionsBuilder, DotNet.ProjectGraph.Tool.Projectgraph.Build.Options.BuildOptionsBuilder>();
            services.AddSingleton<DotNet.ProjectGraph.Tool.Projectgraph.IProjectgraphSubCommandBuilder, DotNet.ProjectGraph.Tool.Projectgraph.Build.BuildCommandBuilder>();
            services.AddSingleton<DotNet.ProjectGraph.Tool.Projectgraph.Build.Service.IBuildService, DotNet.ProjectGraph.Tool.Projectgraph.Build.Service.BuildService>();
        }
    }
}