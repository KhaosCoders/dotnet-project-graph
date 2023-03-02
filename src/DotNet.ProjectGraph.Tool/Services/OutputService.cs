using DotNet.ProjectGraph.Tool.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

    public void Output(CSProject graph, string? outputFile, bool showPackages) =>
        Output(
            WriteToConsole,
            WriteToJsonFile,
            WriteGraphToDgmlFile,
            graph,
            outputFile,
            showPackages);

    public void Output(IReadOnlyCollection<CSProject> order, string? outputFile, bool showPackages) =>
        Output(
            WriteToConsole,
            WriteToJsonFile,
            WriteOrderToDgmlFile,
            order,
            outputFile,
            showPackages);

    private static void Output<T>(
        Action<T> writeConsole,
        Action<T, string> writeJsonFile,
        Action<T, string, bool> writeDgmlFile,
        T data,
        string? outputFile,
        bool showPackages)
    {
        if (string.IsNullOrWhiteSpace(outputFile))
        {
            writeConsole(data);
            return;
        }

        switch (Path.GetExtension(outputFile).ToLowerInvariant())
        {
            case ".dgml":
                writeDgmlFile(data, outputFile, showPackages);
                break;
            case ".json":
                writeJsonFile(data, outputFile);
                break;
            default:
                throw new NotSupportedException($"Can't output to {Path.GetExtension(outputFile)} file");
        }
    }

    private void WriteToConsole(object data)
    {
        string json = JsonConvert.SerializeObject(data);
        this.consoleService.WriteInfo(json);
    }

    private void WriteGraphToDgmlFile(CSProject project, string outputFile, bool showPackages)
    {
        string dgml = this.dgmlService.GenerateDgmlForGraph(project, showPackages);
        File.WriteAllText(outputFile, dgml, Encoding.UTF8);
    }

    private void WriteOrderToDgmlFile(IReadOnlyCollection<CSProject> order, string outputFile, bool showPackages)
    {
        string dgml = this.dgmlService.GenerateDgmlForOrder(order, showPackages);
        File.WriteAllText(outputFile, dgml, Encoding.UTF8);
    }

    private void WriteToJsonFile(object data, string outputFile)
    {
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(outputFile, json, Encoding.UTF8);
    }
}
