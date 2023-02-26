using System;

namespace DotNet.ProjectGraph.Tool.ErrorHandling;

public class ProjectgraphException : Exception
{
    public ProjectgraphException(string message) : base(message)
    {
    }
}