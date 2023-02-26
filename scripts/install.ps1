function Install-Tool {
    dotnet tool install -g --add-source ./nupkg DotNet.ProjectGraph.Tool
}

if (-Not (Install-Tool)) {
    Write-Host "Installing tool failed, trying to uninstall first..."
    dotnet tool uninstall -g DotNet.ProjectGraph.Tool
    Install-Tool
}
