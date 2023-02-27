using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DotNet.ProjectGraph.Tool.App;

[assembly:InternalsVisibleTo("DotNet.ProjectGraph.Tool.Test")]

namespace DotNet.ProjectGraph.Tool;

internal static class Program
{
    private static Task<int> Main(string[] args) =>
        new AppBuilder().Build().RunAsync(args);
}