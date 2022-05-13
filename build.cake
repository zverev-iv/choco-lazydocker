var target = Argument("target", "Publish");

var packageInfo = new ChocolateyPackSettings {
    //PACKAGE SPECIFIC SECTION
    Id                       = "Lazydocker",
    Version                  = "0.18.1",
    PackageSourceUrl         = new Uri("https://github.com/zverev-iv/choco-lazydocker"),
    Owners                   = new[] {"zverev-iv"},
    //SOFTWARE SPECIFIC SECTION
    Title                    = "lazydocker",
    Authors                  = new[] {
        "Jesse Duffield"
        },
    Copyright                = "2021 Jesse Duffield",
    ProjectUrl               = new Uri("https://github.com/jesseduffield/lazydocker"),
    DocsUrl                  = new Uri("https://github.com/jesseduffield/lazydocker/blob/master/README.md"),
    BugTrackerUrl            = new Uri("https://github.com/jesseduffield/lazydocker/issues"),
    IconUrl                  = new Uri("https://cdn.statically.io/gh/zverev-iv/choco-lazydocker/master/lazydocker/logo.png"),
    LicenseUrl               = new Uri("https://raw.githubusercontent.com/jesseduffield/lazydocker/master/LICENSE"),
    RequireLicenseAcceptance = false,
    Summary                  = "A simple terminal UI for both docker and docker-compose, written in Go with the gocui library.",
    Description              = @"What a headache!

Memorising docker commands is hard. Memorising aliases is slightly less hard. Keeping track of your containers across multiple terminal windows is near impossible. What if you had all the information you needed in one terminal window with every common command living one keypress away (and the ability to add custom commands as well). Lazydocker's goal is to make that dream a reality.",
    ReleaseNotes             = new [] {"https://github.com/jesseduffield/lazydocker/releases"},
    Files                    = new [] {
        new ChocolateyNuSpecContent {Source = System.IO.Path.Combine("src", "**"), Target = "tools"}
        },
    Tags                     = new [] {
        "lazydocker",
        "docker",
        "docker-compose",
        "container",
        "kubernetes",
        "containerd"
        }
    };

Task("Clean")
    .Does(() =>
{
    DeleteFiles("./**/*.nupkg");
	DeleteFiles("./**/*.nuspec");
});

Task("Pack")
    .IsDependentOn("Clean")
    .Does(() =>
{
    ChocolateyPack(packageInfo);
});

Task("Publish")
    .IsDependentOn("Pack")
    .Does(() =>
{
	var publishKey = EnvironmentVariable<string>("CHOCOAPIKEY", null);
    var package = $"{packageInfo.Id}.{packageInfo.Version}.nupkg";

    ChocolateyPush(package, new ChocolateyPushSettings {
        ApiKey = publishKey
    });
});

RunTarget(target);
