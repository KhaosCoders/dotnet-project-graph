using DotNet.ProjectGraph.Tool.Models;
using DotNet.ProjectGraph.Tool.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace DotNet.ProjectGraph.Tool.Test;

[TestClass]
public class ProjectReferenceResolverTests
{
    private readonly IProjectReferenceResolver resolver;

    public ProjectReferenceResolverTests()
    {
        resolver = new ProjectReferenceResolver(new MSBuildService());
    }

    [TestMethod]
    public void Resolve()
    {
        string cleanupDir = string.Empty;
        try
        {
            // given
            var csProject = new CSProjectBuilder()
                .WithName("Root.Project")
                .WithVersion("1.0.0")
                .WithReference(r =>
                    r.WithName("Dependency1")
                     .WithVersion("0.1.0")
                     .WithPath(@"..\Libs\Dependency1"))
                .WithReference(r =>
                    r.WithName("Dependency2")
                     .WithVersion("0.2.0")
                     .WithPath(@"..\Libs\Dependency2")
                     .WithReference(r2 =>
                        r2.WithName("Dependency3")
                          .WithVersion("0.2.1")
                          .WithPath(@"..\Dependency3")))
                .Build();

            (cleanupDir, var project) = new TempProjectMapper().MapToTempDir(csProject);

            // when
            var resolvedProject = resolver.Resolve(project);

            // then
            StripPaths(resolvedProject)
                .Should()
                .BeEquivalentTo(StripPaths(csProject));
        }
        finally
        {
            if (Directory.Exists(cleanupDir))
            {
                Directory.Delete(cleanupDir, true);
            }
        }
    }

    private CSProject StripPaths(CSProject project)
    {
        project.FullPath = string.Empty;
        project.Packages.RemoveAll(p => p.Name == "NETStandard.Library");
        foreach (CSProject reference in  project.References.OfType<CSProject>())
        {
            StripPaths(reference);
        }
        return project;
    }
}
