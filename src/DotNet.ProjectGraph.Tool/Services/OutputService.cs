using DotNet.ProjectGraph.Tool.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DotNet.ProjectGraph.Tool.Services;

internal class OutputService : IOutputService
{
    private readonly IDgmlService dgmlService;
    private readonly IConsoleService consoleService;

    public OutputService(IDgmlService dgmlService, IConsoleService consoleService)
    {
        this.dgmlService = dgmlService;
        this.consoleService = consoleService;
    }

    public void OutputToFile(CSProject project, string outputFile)
    {
        switch(Path.GetExtension(outputFile).ToLowerInvariant())
        {
            case ".dgml":
                WriteDgml(project, outputFile);
                break;
            case ".json":
                WriteJson(project, outputFile);
                break;
            default:
                throw new NotSupportedException($"Can't output to {Path.GetExtension(outputFile)} file");
        }
    }

    public void OutputToConsole(CSProject project)
    {
        string json = JsonConvert.SerializeObject(project);
        this.consoleService.WriteInfo(json);
    }

    private void WriteDgml(CSProject project, string outputFile)
    {
        string dgml = this.dgmlService.GenerateDgml(project);
        File.WriteAllText(outputFile, dgml, Encoding.UTF8);
    }

    private void WriteJson(CSProject project, string outputFile)
    {
        string json = JsonConvert.SerializeObject(project);
        File.WriteAllText(outputFile, json, Encoding.UTF8);
    }
}
