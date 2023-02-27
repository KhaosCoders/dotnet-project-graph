using DotNet.ProjectGraph.Tool.Models;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Locator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNet.ProjectGraph.Tool.Services;

internal class MSBuildService : IMSBuildService
{
    private Version[] brokenSdkVersions = new Version[]
    {
        new(7,0,200),
        new(7,0,201)
    };

    public MSBuildService()
    {
        SetupDotnetSdk();
    }

    private void SetupDotnetSdk()
    {
        foreach (var instance in MSBuildLocator.QueryVisualStudioInstances())
        {
            // .net sdk 7.200 and 7.201 crash this app
            if (!brokenSdkVersions.Contains(instance.Version))
            {
                MSBuildLocator.RegisterInstance(instance);
                return;
            }
        }

        throw new Exception("No supported .net Sdk installed. Plase download and install a Sdk.");
    }

    public CSProject ReadProject(string projectPath)
    {
        var buildEngine = new ProjectCollection();
        var msProject = buildEngine.LoadProject(projectPath);

        return new()
        {
            Name = Path.GetFileNameWithoutExtension(msProject.FullPath),
            FullPath = msProject.FullPath,
            VersionString = msProject.Properties
                .Where(p => p.Name == "Version")
                .Select(p => p.EvaluatedValue)
                .FirstOrDefault("0.0.0.0"),
            Packages = CollectPackages(msProject).ToList(),
            References = CollectReferences(msProject).ToList(),
        };
    }

    private IEnumerable<ProjectReference> CollectReferences(Project project) =>
        project.Items
            .Where(i => i.ItemType == "ProjectReference")
            .Select(ToProjectReference);

    private ProjectReference ToProjectReference(ProjectItem item) =>
        new()
        {
            Name = Path.GetFileNameWithoutExtension(item.EvaluatedInclude),
            FullPath = item.EvaluatedInclude,
            OutputType = item.Metadata
                .Where(m => m.Name == "OutputItemType")
            .Select(m => m.EvaluatedValue)
            .FirstOrDefault("")
        };

    private IEnumerable<NugetPackage> CollectPackages(Project project) =>
        project.Items
            .Where(i => i.ItemType == "PackageReference")
            .Select(ToNugetPackage);

    private NugetPackage ToNugetPackage(ProjectItem item) =>
        new(item.EvaluatedInclude,
            item.Metadata
                .Where(m => m.Name == "Version")
                .Select(m => m.EvaluatedValue)
                .FirstOrDefault("?"));
}
