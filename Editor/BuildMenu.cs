
using System;
using System.IO;
using System.Linq;
using UnityEditor;

#nullable enable

namespace BuildMenu.Editor
{
    public static class BuildMenu
    {
        // Desktop

        [MenuItem("Build/Build Windows 32bit", priority = 1)]
        public static void BuildWin32() => BuildUtils.Build(BuildTarget.StandaloneWindows);

        [MenuItem("Build/Build Windows 64bit", priority = 2)]
        public static void BuildWin64() => BuildUtils.Build(BuildTarget.StandaloneWindows64);

        [MenuItem("Build/Build macOS", priority = 3)]
        public static void BuildMacOS() => BuildUtils.Build(BuildTarget.StandaloneOSX);

        [MenuItem("Build/Build Linux", priority = 4)]
        public static void BuildLinux() => BuildUtils.Build(BuildTarget.StandaloneLinux64);

        // Mobile

        [MenuItem("Build/Build Android", priority = 101)]
        public static void BuildAndroid() => BuildUtils.Build(BuildTarget.Android);

        [MenuItem("Build/Build iOS", priority = 102)]
        public static void BuildIOS() => BuildUtils.Build(BuildTarget.iOS);

        // Development

        [MenuItem("Build/Build Development Windows 64bit", priority = 1001)]
        public static void BuildDevelopmentWin64() => BuildUtils.Build(BuildTarget.StandaloneWindows64, BuildOptions.Development);

        [MenuItem("Build/Build Development macOS", priority = 1002)]
        public static void BuildDevelopmentMacOS() => BuildUtils.Build(BuildTarget.StandaloneOSX, BuildOptions.Development);

        [MenuItem("Build/Build Development Linux", priority = 1003)]
        public static void BuildDevelopmentLinux() => BuildUtils.Build(BuildTarget.StandaloneLinux64, BuildOptions.Development);
    }
}
