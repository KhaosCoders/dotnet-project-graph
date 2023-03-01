using DotNet.ProjectGraph.Tool.Models;
using System;
using System.IO;
using System.Linq;

namespace DotNet.ProjectGraph.Tool.Services;

internal class ProjectReferenceResolver : IProjectReferenceResolver
{
    private readonly IMSBuildService msBuildService;

    public ProjectReferenceResolver(IMSBuildService msBuildService)
    {
        this.msBuildService = msBuildService;
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
        var fullPath = Path.Combine(projectDir, reference.FullPath);

        var project = this.msBuildService.ReadProject(fullPath);
        project.OutputType = reference.OutputType;

        return Resolve(project);
    }
}
