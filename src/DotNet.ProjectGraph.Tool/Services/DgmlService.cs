using DotNet.ProjectGraph.Tool.Helpers;
using DotNet.ProjectGraph.Tool.Models;
using Microsoft.Build.Evaluation;
using OpenSoftware.DgmlTools;
using OpenSoftware.DgmlTools.Builders;
using OpenSoftware.DgmlTools.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DotNet.ProjectGraph.Tool.Services;

internal class DgmlService : IDgmlService
{
    public string GenerateDgmlForOrder(IReadOnlyCollection<CSProject> order, bool showPackages)
    {
        var objects = CollectOrderObjects(order, showPackages);
        return GenerateGraph(order.LastOrDefault(), objects);
    }

    public string GenerateDgmlForGraph(CSProject project, bool showPackages)
    {
        var objects = CollectGraphObjects(project, showPackages);
        return GenerateGraph(project, objects);
    }

    private static string GenerateGraph(CSProject? root, IEnumerable<object> graphObjects)
    {
        DgmlBuilder builder = new()
        {
            NodeBuilders = new List<NodeBuilder>()
            {
                new NodeBuilder<CSProject>(p => new Node
                {
                    Id = p.Name,
                    Label = $"{p.Name} (v.{p.Version})",
                    Category = p == root ? "Root" : string.IsNullOrWhiteSpace(p.OutputType) ? null : p.OutputType,
                }),
                new NodeBuilder<Package>(p => new Node
                {
                    Id = p.Name,
                    Label = $"{p.Name} (v.{p.Version})",
                    Category = "Package",
                })
            },
            LinkBuilders = new List<LinkBuilder>()
            {
                new LinkBuilder<ProjectRef>(p => new Link()
                {
                    Source = p.Source,
                    Target = p.Target,
                    Stroke = "White",
                    StrokeThickness = "2"
                }),
                new LinkBuilder<PackageRef>(p => new Link()
                {
                    Source = p.Source,
                    Target = p.Target,
                    Stroke = "LightGrey",
                    StrokeThickness = "1"
                })
            },
            CategoryBuilders = new List<CategoryBuilder>()
            {
                new CategoryBuilder<Style>(s => new Category()
                {
                    Id = s.Name,
                    Label = s.Name,
                    Background = s.Background
                })
            }
        };

        List<object> elements = new(graphObjects)
        {
            new Style("Root", "Orange"),
            new Style("Analyzer", "Yellow"),
            new Style("Package", "LightBlue")
        };

        StringBuilder sb = new();
        using Utf8StringWriter writer = new(sb);
        XmlSerializer serializer = new(typeof(DirectedGraph));

        var graph = builder.Build(elements);
        serializer.Serialize(writer, graph);

        return sb.ToString();
    }

    private static IEnumerable<object> CollectGraphObjects(CSProject project, bool showPackages)
    {
        yield return project;

        if (showPackages)
        {
            foreach (var package in CollectProjectPackages(project))
            {
                yield return package;
                yield return new PackageRef(project.Name, package.Name);
            }
        }

        foreach (var reference in project.References.OfType<CSProject>())
        {
            foreach (var sub in CollectGraphObjects(reference, showPackages))
            {
                yield return sub;
            }

            yield return new ProjectRef(project.Name, reference.Name);
        }
    }

    private static IEnumerable<object> CollectOrderObjects(IReadOnlyCollection<CSProject> order, bool showPackages)
    {
        var current = order.FirstOrDefault();
        if (current == default)
        {
            yield break;
        }

        yield return current;

        if (showPackages)
        {
            foreach (var package in CollectProjectPackages(current))
            {
                yield return package;
                yield return new PackageRef(current.Name, package.Name);
            }
        }

        foreach (var project in order.Skip(1))
        {
            yield return project;
            yield return new ProjectRef(current.Name, project.Name);
            current = project;

            if (showPackages)
            {
                foreach (var package in CollectProjectPackages(current))
                {
                    yield return package;
                    yield return new PackageRef(current.Name, package.Name);
                }
            }
        }
    }

    private static IEnumerable<Package> CollectProjectPackages(CSProject project)
    {
        foreach (var package in project.Packages)
        {
            yield return new(package.Name, package.Version);
        }
    }

    private record ProjectRef(string Source, string Target);
    private record PackageRef(string Source, string Target);

    private record Style(string Name, string Background);

    private record Package(string Name, string Version);
}
