public class PackageInfo
{
    public string Package32Url { get; set; }
    public string Package64Url { get; set; }

    public string BinDir { get => "bin"; }
    public string TempDir { get => "temp"; }

    public ChocolateyPackSettings PackageSettings {get; set;}
}