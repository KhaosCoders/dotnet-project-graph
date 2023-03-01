using System.IO;
using System;
using DotNet.ProjectGraph.Tool.Models;

namespace DotNet.ProjectGraph.Tool.Services;

internal class DependencyGraphService : IDependencyGraphService
{
    private readonly IMSBuildService msBuildService;
    private readonly IProjectReferenceResolver projectReferenceResolver;

    public DependencyGraphService(IMSBuildService msBuildService, IProjectReferenceResolver projectReferenceResolver)
    {
        this.msBuildService = msBuildService;
        this.projectReferenceResolver = projectReferenceResolver;
    }

    public CSProject BuildGraph(string? projectFile)
    {
        ArgumentException.ThrowIfNullOrEmpty(projectFile);
        if (!File.Exists(projectFile)) { throw new FileNotFoundException(projectFile); }

        if (!Path.GetExtension(projectFile).Equals(".csproj", StringComparison.OrdinalIgnoreCase))
        {
            throw new NotSupportedException("Input file type not supported!");
        }

        var msProject = this.msBuildService.ReadProject(projectFile);
        return this.projectReferenceResolver.Resolve(msProject);
    }
}
