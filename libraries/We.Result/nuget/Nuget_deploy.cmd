MSBuild.exe /t:pack /p:Configuration=Release
nuget add bin\release\We.Results.1.0.0.nupkg -source e:\projets\nuget_repo
