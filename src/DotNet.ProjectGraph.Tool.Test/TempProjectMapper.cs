using DotNet.ProjectGraph.Tool.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace DotNet.ProjectGraph.Tool.Test;

internal class TempProjectMapper
{
    public (string, CSProject) MapToTempDir(CSProject project)
    {
        var tempDirectory = Directory.CreateTempSubdirectory(Guid.NewGuid().ToString());
        return (tempDirectory.FullName, MapRootProject(project, tempDirectory));
    }

    private CSProject MapRootProject(CSProject project, DirectoryInfo tempDir)
    {
        var projectDir = tempDir.CreateSubdirectory(project.Name);
        return MapProject<CSProject>(project, projectDir);
    }

    private T MapProject<T>(CSProject project, DirectoryInfo projectDir) where T : ProjectReference
    {
        T mappedProject = typeof(T) == typeof(CSProject)
            ? (T)Convert.ChangeType(new CSProject(project.Name, project.Version, string.Empty), typeof(T))
            : (T)new ProjectReference(project.Name, project.Version, string.Empty);

        foreach (CSProject reference in project.References.OfType<CSProject>())
        {
            var referenceDir = Directory.CreateDirectory(Path.Combine(projectDir.FullName, reference.FullPath));
            var mappedReference = MapProject<ProjectReference>(reference, referenceDir);

            reference.FullPath  = Path.GetRelativePath(projectDir.FullName, mappedReference.FullPath);

            if (mappedProject is CSProject csProj)
            {
                csProj.References.Add(mappedReference);
            }
        }

        mappedProject.FullPath = WriteCsProjectFile(project, projectDir);
        return mappedProject;
    }

    private static string WriteCsProjectFile(CSProject project, DirectoryInfo projectDir)
    {
        string fileName = Path.Combine(projectDir.FullName, Path.ChangeExtension(project.Name, ".csproj"));

        StringBuilder sb = new();

        sb.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
        sb.AppendLine("  <PropertyGroup>");
        sb.AppendLine("    <TargetFramework>netstandard2.0</TargetFramework>");
        sb.Append("    <Version>").Append(project.Version).AppendLine("</Version>");
        sb.AppendLine("  </PropertyGroup>");
        sb.AppendLine("  <ItemGroup>");
        foreach (var package in project.Packages)
        {
            sb.Append("    <PackageReference Include=\"")
                .Append(package.Name)
                .Append("\" Version=\"")
                .Append(package.Version)
                .AppendLine("\" />");
        }
        sb.AppendLine("  </ItemGroup>");
        sb.AppendLine("  <ItemGroup>");
        foreach (var reference in project.References)
        {
            sb.Append("    <ProjectReference Include=\"")
                .Append(reference.FullPath)
                .AppendLine("\" />");
        }
        sb.AppendLine("  </ItemGroup>");
        sb.AppendLine("</Project>");

        File.WriteAllText(fileName, sb.ToString());

        return fileName;
    }
}
