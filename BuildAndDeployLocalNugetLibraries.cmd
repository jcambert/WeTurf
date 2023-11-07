cls
@echo off
set MSBUILD_PATH="E:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin"
set VERSION=1.0.5
set NUGET_PATH=E:\projets\nuget_repo
set SOURCE_PATH=%cd%\libraries

%MSBUILD_PATH%\MSBuild.exe /t:pack /p:Configuration=Release 
if exist %NUGET_PATH%\We.Results\%VERSION% rmdir /s /q %NUGET_PATH%\We.Results\%VERSION%
if exist %NUGET_PATH%\we.abpextensions\%VERSION% rmdir /s /q %NUGET_PATH%\we.abpextensions\%VERSION%
if exist %NUGET_PATH%\We.Mediatr\%VERSION% rmdir /s /q %NUGET_PATH%\We.Mediatr\%VERSION%
if exist %NUGET_PATH%\we.utilities\%VERSION% rmdir /s /q %NUGET_PATH%\we.utilities\%VERSION%



nuget add %SOURCE_PATH%\We.AbpExtensions\bin\release\We.AbpExtensions.%VERSION%.nupkg -source %NUGET_PATH%
nuget add %SOURCE_PATH%\We.Mediatr\bin\release\We.Mediatr.%VERSION%.nupkg -source %NUGET_PATH%
nuget add %SOURCE_PATH%\We.Utilities\bin\release\We.Utilities.%VERSION%.nupkg -source %NUGET_PATH%

nuget add %SOURCE_PATH%\We.Result\bin\release\We.Results.%VERSION%.nupkg -source %NUGET_PATH%
