using System.Threading.Tasks;
using DotNet.ProjectGraph.Tool.App;

namespace DotNet.ProjectGraph.Tool
{
    internal class Program
    {
        private static Task<int> Main(string[] args)
        {
            return new AppBuilder().Build().RunAsync(args);
        }
    }
}