using DotNet.ProjectGraph.Tool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNet.ProjectGraph.Tool.Services;

internal class ProjectReferenceResolver : IProjectReferenceResolver
{
    private readonly IMSBuildService msBuildService;
    private readonly Dictionary<string, WeakReference<CSProject>> csProjectCache;

    public ProjectReferenceResolver(IMSBuildService msBuildService)
    {
        this.msBuildService = msBuildService;
        this.csProjectCache = new();
    }

    public CSProject Resolve(CSProject project)
    {
        var projectDir = Path.GetDirectoryName(project.FullPath);
        ArgumentException.ThrowIfNullOrEmpty(projectDir);

        foreach (var reference in project.References.ToList())
        {
            CSProject referencedProject = Resolve(reference, projectDir);
            project.References.Insert(project.References.IndexOf(reference), referencedProject);
            project.References.Remove(reference);
        }

        return project;
    }

    public CSProject Resolve(ProjectReference reference, string projectDir)
    {
        var fullPath = Path.GetFullPath(Path.Combine(projectDir, reference.FullPath));

        if (this.csProjectCache.TryGetValue(fullPath, out var refProject)
            && refProject.TryGetTarget(out var cachedProject))
        {
            return cachedProject;
        }

        var project = this.msBuildService.ReadProject(fullPath);
        project.OutputType = reference.OutputType;

        this.csProjectCache.Add(fullPath, new(project));

        return Resolve(project);
    }
}
