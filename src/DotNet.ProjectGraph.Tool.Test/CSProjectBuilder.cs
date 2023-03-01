using DotNet.ProjectGraph.Tool.Models;
using System;

namespace DotNet.ProjectGraph.Tool.Test;

internal class CSProjectBuilder
{
    private readonly CSProject project;

    public CSProjectBuilder()
    {
        project = new(string.Empty, string.Empty, string.Empty);
    }

    public CSProjectBuilder WithName(string name)
    {
        project.Name = name;
        return this;
    }

    public CSProjectBuilder WithVersion(string version)
    {
        project.Version = version;
        return this;
    }

    public CSProjectBuilder WithPath(string path)
    {
        project.FullPath = path;
        return this;
    }

    public CSProjectBuilder WithNuget(string name, string version)
    {
        project.Packages.Add(new(name, version));
        return this;
    }

    public CSProjectBuilder WithReference(Action<CSProjectBuilder> options)
    {
        CSProjectBuilder builder = new();
        options(builder);
        project.References.Add(builder.Build());
        return this;
    }

    public CSProject Build()
    {
        return project;
    }
}
