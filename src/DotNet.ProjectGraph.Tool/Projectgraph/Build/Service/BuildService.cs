using DotNet.ProjectGraph.Tool.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DotNet.ProjectGraph.Tool.Projectgraph.Build.Service;

internal class BuildService : IBuildService
{
    private readonly IMSBuildService msBuildService;
    private readonly IProjectReferenceResolver projectReferenceResolver;
    private readonly IOutputService outputService;

    public BuildService(IMSBuildService msBuildService,
                        IProjectReferenceResolver projectReferenceResolver,
                        IOutputService outputService)
    {
        this.msBuildService = msBuildService;
        this.projectReferenceResolver = projectReferenceResolver;
        this.outputService = outputService;
    }

    public Task HandleAsync(BuildParameters parameters)
    {
        ArgumentException.ThrowIfNullOrEmpty(parameters.ProjectFile);
        if (!File.Exists(parameters.ProjectFile)) { throw new FileNotFoundException(parameters.ProjectFile); }

        if (!Path.GetExtension(parameters.ProjectFile).Equals(".csproj", StringComparison.OrdinalIgnoreCase))
        {
            throw new NotSupportedException("Input file type not supported!");
        }

        var msProject = this.msBuildService.ReadProject(parameters.ProjectFile);
        var project = this.projectReferenceResolver.Resolve(msProject);

        if (!string.IsNullOrWhiteSpace(parameters.OutputFile))
        {
            this.outputService.OutputToFile(project, parameters.OutputFile);
        }
        else
        {
            this.outputService.OutputToConsole(project);
        }

        return Task.CompletedTask;
    }
}