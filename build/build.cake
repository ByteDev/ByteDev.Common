#tool "nuget:?package=NUnit.Runners&version=2.6.4"

var nugetSources = new[] {"https://api.nuget.org/v3/index.json"};

var target = Argument("target", "Default");

var solutionFilePath = "../src/ByteDev.Common.sln";

var artifactsDirectory = Directory("../artifacts");
var nugetDirectory = artifactsDirectory + Directory("NuGet");
	
var configuration = GetConfiguration();
	

Task("Clean")
    .Does(() =>
{
    CleanDirectory(artifactsDirectory);
	
	var binDirs = GetDirectories("../src/**/bin");
	var objDirs = GetDirectories("../src/**/obj");

	CleanDirectories(binDirs);
	CleanDirectories(objDirs);
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
		var settings = new NuGetRestoreSettings
		{
			Source = nugetSources
		};

		NuGetRestore(solutionFilePath, settings);
    });

Task("Build")
	.IsDependentOn("Restore")
    .Does(() =>
	{	
        DotNetCoreBuild(
            solutionFilePath,
            new DotNetCoreBuildSettings()
            {
                Configuration = configuration
            });
	});

Task("UnitTests")
    .IsDependentOn("Build")
    .Does(() =>
	{
		var projects = GetFiles("../src/*.UnitTests/**/*.csproj");
		
		foreach(var project in projects)
		{
			DotNetCoreTest(
				project.FullPath,
				new DotNetCoreTestSettings()
				{
					Configuration = configuration,
					NoBuild = true
				});
		}
	});
	
Task("CreateNuGetPackages")
    .IsDependentOn("UnitTests")
    .Does(() =>
    {
        var settings = new DotNetCorePackSettings()
		{
			Configuration = configuration,
			OutputDirectory = nugetDirectory
		};
                
		DotNetCorePack("../src/ByteDev.Common/ByteDev.Common.csproj", settings);
    });

   
Task("Default")
    .IsDependentOn("CreateNuGetPackages");

RunTarget(target);

// -----------------------

string GetConfiguration()
{
	if(HasArgument("Configuration"))
	{
		return Argument<string>("Configuration");
	}

	return EnvironmentVariable("Configuration") != null ? 
		EnvironmentVariable("Configuration") : 
		"Release";
}