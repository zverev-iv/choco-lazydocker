#addin nuget:?package=Cake.FileHelpers&version=6.0.0
#addin nuget:?package=Cake.Json&version=7.0.1

#load "lib/PackageInfo.cs"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Publish");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup<PackageInfo>(setupContext => DeserializeJsonFromFile<PackageInfo>("config.json"));

Teardown(ctx =>
{
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Print config")
    .Does<PackageInfo>(data =>
{
	Information(SerializeJson<PackageInfo>(data));
});

Task("Clean")
    .Does<PackageInfo>(data =>
{
    DeleteFiles("./**/*.nupkg");
    DeleteFiles("./**/*.nuspec");
    DeleteFiles(new DirectoryPath(data.BinDir).Combine("*").ToString());
    if (DirectoryExists(data.BinDir))
    {
        DeleteDirectory(data.BinDir, new DeleteDirectorySettings {
        Force = true
        });
    }
    DeleteFiles(new DirectoryPath(data.TempDir).Combine("*").ToString());
    if (DirectoryExists(data.TempDir))
    {
        DeleteDirectory(data.TempDir, new DeleteDirectorySettings {
        Force = true
        });
    }
});

Task(".gitignore clean")
    .Does<PackageInfo>(data =>
{
    var regexes = FileReadLines("./.gitignore");
    foreach(var regex in regexes)
    {
        DeleteFiles(regex);
    }
});

Task("Copy src to bin")
    .Does<PackageInfo>(data =>
{
    if (!DirectoryExists(data.BinDir))
    {
        CreateDirectory(data.BinDir);
    }
    CopyFiles("src/*", data.BinDir);
});

Task("Set package args")
    .IsDependentOn("Copy src to bin")
    .Does<PackageInfo>(data =>
{
    ReplaceTextInFiles(new DirectoryPath(data.BinDir).Combine("*").ToString(), "${softwareName}", data.PackageSettings.Id);

    string hash  = null;
    string hash64 = null;
    if (!DirectoryExists(data.TempDir))
    {
        CreateDirectory(data.TempDir);
    }
    if(!string.IsNullOrWhiteSpace(data.Package32Url))
    {
        Information("Download x86 binary");
        var uri = new Uri(data.Package32Url);
        var fileName = System.IO.Path.GetFileName(uri.LocalPath);
        var fullFileName = new DirectoryPath(data.TempDir).Combine(fileName).ToString();
        DownloadFile(data.Package32Url, fullFileName);
        Information("Calculate sha256 for x86 binary");
        hash = CalculateFileHash(fullFileName).ToHex();
        Information("Write x86 data in sources");
        ReplaceTextInFiles(new DirectoryPath(data.BinDir).Combine("*").ToString(), "${url}", data.Package32Url);
        ReplaceTextInFiles(new DirectoryPath(data.BinDir).Combine("*").ToString(), "${checksum}", hash);
        ReplaceTextInFiles(new DirectoryPath(data.BinDir).Combine("*").ToString(), "${checksumType}", "sha256");
    }
    if(data.Package64Url == data.Package32Url && hash != null)
    {
        Information("x86 and x64 uri are the same");
        Information("Write x64 data in sources");
        ReplaceTextInFiles(new DirectoryPath(data.BinDir).Combine("*").ToString(), "${url64bit}", data.Package64Url);
        ReplaceTextInFiles(new DirectoryPath(data.BinDir).Combine("*").ToString(), "${checksum64}", hash);
        ReplaceTextInFiles(new DirectoryPath(data.BinDir).Combine("*").ToString(), "${checksumType64}", "sha256");
    }
    else if(!string.IsNullOrWhiteSpace(data.Package64Url))
    {
        Information("Download x64 binary");
        var uri = new Uri(data.Package64Url);
        var fullFileName = System.IO.Path.Combine(data.TempDir, System.IO.Path.GetFileName(uri.LocalPath));
        DownloadFile(data.Package64Url, fullFileName);
        Information("Calculate sha256 for x86 binary");
        hash64 = CalculateFileHash(fullFileName).ToHex();
        Information("Write x64 data in sources");
        ReplaceTextInFiles(new DirectoryPath(data.BinDir).Combine("*").ToString(), "${url64bit}", data.Package64Url);
        ReplaceTextInFiles(new DirectoryPath(data.BinDir).Combine("*").ToString(), "${checksum64}", hash64);
        ReplaceTextInFiles(new DirectoryPath(data.BinDir).Combine("*").ToString(), "${checksumType64}", "sha256");
    }
});

Task("Pack")
    .IsDependentOn("Clean")
    .IsDependentOn("Set package args")
    .Does<PackageInfo>(data =>
{
    ChocolateyPack(data.PackageSettings);
});

Task("Publish")
    .IsDependentOn("Pack")
    .Does<PackageInfo>(data =>
{
    var publishKey = EnvironmentVariable<string>("CHOCOAPIKEY", null);
    var package = $"{data.PackageSettings.Id}.{data.PackageSettings.Version}.nupkg";

    ChocolateyPush(package, new ChocolateyPushSettings
    {
        ApiKey = publishKey
    });
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
