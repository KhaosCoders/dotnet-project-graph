function Install-Tool {
    dotnet tool install -g --add-source ./nupkg Project.Graph.Tool
}

if (-Not (Install-Tool)) {
    Write-Host "Installing tool failed, trying to uninstall first..."
    dotnet tool uninstall -g Project.Graph.Tool
    Install-Tool
}
