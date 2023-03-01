using DotNet.ProjectGraph.Tool.Models;

namespace DotNet.ProjectGraph.Tool.Services;

internal interface IOutputService
{
    void OutputToConsole(CSProject project);
    void OutputToFile(CSProject project, string outputFile);
}
