using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

#nullable enable

namespace BuildMenu.Editor
{
    public static class BuildUtils
    {
        const string UseDotnetRIDSetting = @"com.redwyre.buildmenu/UseDotnetRID";

        static bool? UseDotnetRID_CommandLine = CommandLineUtils.GetCommandLineParameter("usedotnetrid");

        public static bool UseDotnetRID
        {
            get { return UseDotnetRID_CommandLine ?? EditorPrefs.GetBool(UseDotnetRIDSetting, false); }
            set { EditorPrefs.SetBool(UseDotnetRIDSetting, value); }
        }

        public static void Build(BuildTarget target, BuildOptions buildOptions = BuildOptions.None)
        {
            var scenes = EditorBuildSettings.scenes;
            var path = GenerateBuildPath(target);
            var report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path, target, buildOptions);
        }

        /// <summary>
        /// Generates a build path for a BuiltTarget.
        /// </summary>
        /// <param name="buildTarget"></param>
        /// <returns>The relative path in the form "Build/[Platform]/[ProductName][Extension]" </returns>
        public static string GenerateBuildPath(BuildTarget buildTarget)
        {
            var platform = (UseDotnetRID ? BuildTargetToRuntimeIdentifier(buildTarget) : null) ?? BuildTargetToFolderName(buildTarget);
            var extension = GetPlayerExtension(buildTarget);
            return $"Builds/{platform}/{PlayerSettings.productName}{extension}";
        }

        /// <summary>
        /// Returns the extension the built player should have for the given target.
        /// </summary>
        /// <param name="buildTarget"></param>
        /// <returns>An extension starting with ".", or an empty string.</returns>
        public static string GetPlayerExtension(BuildTarget buildTarget)
        {
            return buildTarget switch
            {
                BuildTarget.StandaloneWindows => ".exe",
                BuildTarget.StandaloneWindows64 => ".exe",
                BuildTarget.Android => ".apk",
                _ => string.Empty,
            };
        }

        /// <summary>
        /// Return a dotnet runtime identifier (RID) for the build target if supported.
        /// </summary>
        /// <param name="buildTarget"></param>
        /// <returns>The runtime identifier, or null if not valid.</returns>
        public static string? BuildTargetToRuntimeIdentifier(BuildTarget buildTarget)
        {
            return buildTarget switch
            {
                BuildTarget.StandaloneWindows => "win-x86",
                BuildTarget.StandaloneWindows64 => "win-x64",
                BuildTarget.StandaloneLinux64 => "linux-x64",
                BuildTarget.StandaloneOSX => "osx-x64",
                _ => null,
            };
        }

        /// <summary>
        /// Return a nice folder name for the different build targets
        /// </summary>
        /// <param name="buildTarget"></param>
        /// <returns>The folder name.</returns>
        /// <exception cref="InvalidOperationException">An invalid BuildTarget was passed in.</exception>
        public static string BuildTargetToFolderName(BuildTarget buildTarget)
        {
            return buildTarget switch
            {
                BuildTarget.StandaloneOSX => "macos",
                BuildTarget.StandaloneWindows => "win32",
                BuildTarget.iOS => "ios",
                BuildTarget.Android => "android",
                BuildTarget.StandaloneWindows64 => "win64",
                BuildTarget.WebGL => "webgl",
                BuildTarget.WSAPlayer => "wsaplayer",
                BuildTarget.StandaloneLinux64 => "linux",
                BuildTarget.PS4 => "ps4",
                BuildTarget.XboxOne => "xboxone",
                BuildTarget.tvOS => "tvos",
                BuildTarget.Switch => "switch",
                BuildTarget.GameCoreXboxOne => "gamecorexboxone",
                BuildTarget.PS5 => "ps5",
                BuildTarget.EmbeddedLinux => "elinux",
                BuildTarget.QNX => "qnx",
                _ => throw new InvalidOperationException($"BuildTarget '{buildTarget}' is invalid"),
            };
        }
    }
}
