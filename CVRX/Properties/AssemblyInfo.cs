using CVRX;
using MelonLoader;
using System.Reflection;

[assembly: AssemblyTitle(BuildInfo.Description)]
[assembly: AssemblyDescription(BuildInfo.Description)]
[assembly: AssemblyCompany(BuildInfo.Company)]
[assembly: AssemblyProduct(BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + BuildInfo.Author)]
[assembly: AssemblyTrademark(BuildInfo.Company)]
[assembly: AssemblyVersion(BuildInfo.Version)]
[assembly: AssemblyFileVersion(BuildInfo.Version)]
[assembly: MelonInfo(typeof(Entry), BuildInfo.Name, BuildInfo.Version, BuildInfo.Author, BuildInfo.DownloadLink)]
[assembly: MelonColor(255, 255, 0, 0)]
[assembly: MelonAuthorColor(255, 255, 0, 0)]
[assembly: MelonGame(null, null)]
public static class BuildInfo
{
    public const string Name = "CVRX";
    public const string Description = "Best ChilloutVR Tool.";
    public const string Author = "IL2LX";
    public const string Company = "IL2LX's Creation";
    public const string Version = "0.2.5";
    public const string DownloadLink = null;
}
