using DotNet.ProjectGraph.Tool.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Service;

internal class BuildService : IBuildService
{
    private IMSBuildService msBuildService;

    public BuildService(IMSBuildService msBuildService)
    {
        this.msBuildService = msBuildService;
    }

    public Task HandleAsync(BuildParameters parameters)
    {
        ArgumentException.ThrowIfNullOrEmpty(parameters.Projectfile);
        if (!File.Exists(parameters.Projectfile)) { throw new FileNotFoundException(parameters.Projectfile); }

        if (!Path.GetExtension(parameters.Projectfile).Equals(".csproj", StringComparison.OrdinalIgnoreCase))
        {
            throw new NotSupportedException("Input file type not supported!");
        }

        var rootProject = msBuildService.ReadProject(parameters.Projectfile);



        return Task.CompletedTask;
    }
}