using DotNet.ProjectGraph.Tool.Helpers;
using DotNet.ProjectGraph.Tool.Models;
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
    public string GenerateDgml(CSProject project)
    {
        DgmlBuilder builder = new()
        {
            NodeBuilders = new[]
            {
                new NodeBuilder<CSProject>(p => new Node
                {
                    Id = p.Name,
                    Label = $"{p.Name} (v.{p.Version})",
                    Category = p == project ? "Root" : string.IsNullOrWhiteSpace(p.OutputType) ? null : p.OutputType,
                })
            },
            LinkBuilders = new[]
            {
                new LinkBuilder<ProjectRef>(p => new Link()
                {
                    Source = p.Source,
                    Target = p.Target
                })
            },
            CategoryBuilders = new[]
            {
                new CategoryBuilder<Style>(s => new Category()
                {
                    Id = s.Name,
                    Label = s.Name,
                    Background = s.Background
                })
            }
        };

        List<object> graphObjects = new()
        {
            new Style("Root", "Orange"),
            new Style("Analyzer", "Yellow")
        };

        CollectGraphObjects(project, graphObjects);

        var graph = builder.Build(graphObjects);

        StringBuilder sb = new();
        using Utf8StringWriter writer = new(sb);
        XmlSerializer serializer = new(typeof(DirectedGraph));
        serializer.Serialize(writer, graph);

        return sb.ToString();
    }

    private void CollectGraphObjects(CSProject project, List<object> graphObjects)
    {
        graphObjects.Add(project);
        foreach (var reference in project.References.OfType<CSProject>())
        {
            CollectGraphObjects(reference, graphObjects);
            graphObjects.Add(new ProjectRef(project.Name, reference.Name));
        }
    }

    private record ProjectRef(string Source, string Target);

    private record Style(string Name, string Background);
}
