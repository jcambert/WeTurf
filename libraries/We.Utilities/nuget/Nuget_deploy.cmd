rem Ouvrir un terminal dans VS Studio a partir de la racine du projet
MSBuild.exe /t:pack /p:Configuration=Release
nuget add bin\release\We.Utilities.1.0.2.nupkg -source D:\source\nuget_repo

