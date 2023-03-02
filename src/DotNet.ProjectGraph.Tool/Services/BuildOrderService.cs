using DotNet.ProjectGraph.Tool.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DotNet.ProjectGraph.Tool.Services;

internal class BuildOrderService : IBuildOrderService
{
    public ReadOnlyCollection<CSProject> OrderProjects(CSProject project)
    {
        HashSet<CSProject> projectOrder = new();

        VisitProject(project, projectOrder);

        return new List<CSProject>(projectOrder).AsReadOnly();
    }

    private static void VisitProject(CSProject project, HashSet<CSProject> projectOrder)
    {
        foreach (var reference in GetUnorderedReferences(project, projectOrder))
        {
            VisitProject(reference, projectOrder);
        }

        projectOrder.Add(project);
    }

    private static IEnumerable<CSProject> GetUnorderedReferences(CSProject project, HashSet<CSProject> projectOrder)
    {
        foreach (var reference in project.References.OfType<CSProject>())
        {
            if (!projectOrder.Contains(reference))
            {
                yield return reference;
            }
        }
    }
}
