#!/bin/bash

install-tool() {
    dotnet tool install -g --add-source ./nupkg Project.Graph.Tool
}

install-tool && exit 0;

echo "Installing tool failed, trying to uninstall first..."
dotnet tool uninstall -g Project.Graph.Tool && install-tool
