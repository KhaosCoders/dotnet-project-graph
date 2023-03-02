# Projectgraph CLI

## Install

Make sure you have .NET SDK 7.0 or higher installed on your machine. Then run

```bash
dotnet tool install -g Project.Graph.Tool
```

## Run

```bash
dotnet-projectgraph --help
```

## Output

Use the `--output <file>` option to write the result to a `.json` or `.dgml` file. If ommitted the output will be retured as JSON to the console.

Use the `--packages` options to also include Nuget packages in the DGML-graph.

Use the `--order` option to recieve an ordered list of the projects by dependency

## Develop

### Prerequisites

* VS Code
* VS 2019 or higher
* .NET 7.0 SDK or higher

### Get started

Open in VS Code, run the task `pack and install`.

*Note: To run tasks in VS Code, press* <kbd>CTRL</kbd>+<kbd>⇧</kbd>+<kbd>P</kbd>*, enter `Run` and select `Tasks: Run Task`.*
