#!/bin/bash

# Install .NET SDK if needed (Render will handle this)
# dotnet restore
dotnet publish -c Release -o ./publish