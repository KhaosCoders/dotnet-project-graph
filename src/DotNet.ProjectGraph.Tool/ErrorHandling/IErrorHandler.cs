using System;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace DotNet.ProjectGraph.Tool.ErrorHandling
{
    public interface IErrorHandler
    {
        Task HandleErrors(InvocationContext context, Func<InvocationContext, Task> next);
    }
}