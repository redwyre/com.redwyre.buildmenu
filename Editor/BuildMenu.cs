
using System.IO;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

public static class BuildMenu
{
    [MenuItem("Build/Build win-x64")]
    public static void BuildWinx64()
    {
        Build(BuildTarget.StandaloneWindows);
    }
    
    [MenuItem("Build/Build Development win-x64")]
    public static void BuildDevelopmentWinx64()
    {
        Build(BuildTarget.StandaloneWindows, BuildOptions.Development);
    }

    private static void Build(BuildTarget target, BuildOptions buildOptions = BuildOptions.None)
    {
        var scenes = EditorBuildSettings.scenes;
        var path = BuildPath(target);
        var report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path, target, buildOptions);
    }

    public static string BuildTargetToRti(BuildTarget buildTarget)
    {
        switch (buildTarget)
        {
            case BuildTarget.StandaloneWindows: return "win-x64";
            case BuildTarget.StandaloneLinux64: return "linux-x64";
            case BuildTarget.StandaloneOSX: return "osx-x64";
        }

        return buildTarget.ToString();
    }

    public static string BuildPath(BuildTarget buildTarget)
    {
        var platform = BuildTargetToRti(buildTarget);
        var extension = buildTarget switch { 
            BuildTarget.StandaloneWindows64 => ".exe",
            _ => ""
        };
        return $"Builds/{platform}/{PlayerSettings.productName}{extension}";
    }
}