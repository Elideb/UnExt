using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnExt.Editor {

    /// <summary>
    /// Class to create and build configurations, and execute the
    /// build process from Unity scripts.
    /// Note: using this functionality requires Unity Pro (as of Unity 4.3)
    /// Use BuildConfiguration.Create to get a clean configuration,
    /// set, add and remove methods to customize it
    /// and Build to create the desired executable.
    /// Each call to methods which modify settings create a new configuration
    /// and return it, so previous configurations are not overwritten.
    /// Check the example to check how to take advantadge of it.
    /// </summary>
    /// <example>
    /// // Create a configuration with common parameters for builds.
    /// BuildConfiguration baseConfig = BuildConfiguration.Create()
    ///                                                   .SetExecName( "CoolName" )
    ///                                                   .AddScene( "OutlineTest" )
    ///                                                   .AddBuildOption( BuildOptions.ShowBuiltPlayer )
    ///                                                   .AddBuildOption( BuildOptions.AutoRunPlayer )
    ///                                                   .AddBuildOption( BuildOptions.Development )
    ///                                                   .RemoveBuildOption( BuildOptions.AutoRunPlayer )
    ///                                                   .AddFileMapping( "Sprites/Ship.png", "Ship.png" )
    ///                                                   .AddDirMapping( "Sprites", "Sprites" );
    ///
    /// // Create a basic windows build
    /// baseConfig.SetTargetDir( "../../Builds/Win" )
    ///           .Build( BuildTarget.StandaloneWindows );
    ///
    /// // Create a Linux build with customized settings.
    /// // Each call to base config methods (except build)
    /// // generates a new configuration, so baseConfig remains unchanged.
    /// baseConfig.SetTargetDir( "../../Builds/linux_x86" )
    ///           .SetExecName( baseConfig.ExecName.ToLower() )
    ///           .RemoveBuildOption( BuildOptions.AutoRunPlayer )
    ///           .Build( BuildTarget.StandaloneLinux );
    ///
    /// // Retains the original exec name and auto run setting.
    /// baseConfig.SetTargetDir( "../../Builds/OSXUniversal" )
    ///           .Build( BuildTarget.StandaloneOSXUniversal );
    /// </example>
    public class BuildConfiguration {

        string targetDir;
        /// <summary>
        /// Get the directory where the build will be generated.
        /// </summary>
        public string TargetDir { get { return this.targetDir; } }

        string execName;
        /// <summary>
        /// Get the name of the executable to generate, sans extensions.
        /// </summary>
        public string ExecName { get { return this.execName; } }

        List<string> scenes;
        BuildOptions options;
        /// <summary>
        /// Get the currently configured build options.
        /// </summary>
        public BuildOptions Options { get { return this.options; } }

        Dictionary<string, string> dirMappings;
        Dictionary<string, string> fileMappings;

        private BuildConfiguration() {
            this.scenes = new List<string>();
            this.dirMappings = new Dictionary<string, string>();
            this.fileMappings = new Dictionary<string, string>();
            this.options = BuildOptions.None;
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="other">Configuration to replicate.</param>
        private BuildConfiguration(BuildConfiguration other) {
            this.scenes = new List<string>( other.scenes );

            this.dirMappings = new Dictionary<string, string>();
            foreach (var mapping in other.dirMappings) {
                this.dirMappings[mapping.Key] = mapping.Value;
            }

            this.fileMappings = new Dictionary<string, string>( other.fileMappings );
            foreach (var mapping in other.fileMappings) {
                this.fileMappings[mapping.Key] = mapping.Value;
            }

            this.options = other.Options;
            this.targetDir = other.targetDir;
            this.execName = other.execName;
        }

        /// <summary>
        /// Create a clean configuration.
        /// </summary>
        /// <returns>A new build configuration.</returns>
        public static BuildConfiguration Create() {
            return new BuildConfiguration();
        }

        /// <summary>
        /// Set the name of the executable file to generate.
        /// Do not include file extensions (i.e. ".exe").
        /// </summary>
        /// <param name="execName">Desired name for the executable.</param>
        /// <returns>A copy of this configuration, with the executable name.</returns>
        public BuildConfiguration SetExecName(string execName) {
            var clone = new BuildConfiguration( this );
            clone.execName = execName;
            return clone;
        }

        /// <summary>
        /// Add a scene to the executable. At least one is needed.
        /// </summary>
        /// <param name="scenename">The path of the scene to add.</param>
        /// <returns>A copy of this configuration, with the scene added.</returns>
        public BuildConfiguration AddScene(string scenename) {
            var clone = new BuildConfiguration( this );
            clone.scenes.Add( scenename );
            return clone;
        }

        /// <summary>
        /// Set the directory where the builds should be placed.
        /// </summary>
        /// <param name="pathname">Path where builds are placed.</param>
        /// <returns>A copy of this configuration, with the target dir.</returns>
        public BuildConfiguration SetTargetDir(string pathname) {
            var clone = new BuildConfiguration( this );
            clone.targetDir = pathname;
            return clone;
        }

        /// <summary>
        /// Add Unity build options. Can do it one by one,
        /// or add a mask of several BuildOptions.
        /// </summary>
        /// <param name="_options">Option or options to use when creating the build.</param>
        /// <returns>A copy of this configuration, with the new options.</returns>
        public BuildConfiguration AddBuildOption(BuildOptions _options) {
            var clone = new BuildConfiguration( this );
            clone.options |= _options;
            return clone;
        }

        /// <summary>
        /// Remove a previously set build option.
        /// </summary>
        /// <param name="_options">Option or options to remove.</param>
        /// <returns>A copy of this configuration, without the given options.</returns>
        public BuildConfiguration RemoveBuildOption(BuildOptions _options) {
            var clone = new BuildConfiguration( this );
            clone.options &= ~_options;
            return clone;
        }

        /// <summary>
        /// Configure a directory to copy recursively to the target data directory
        /// after the build is completed.
        /// A source directory can only appear once.
        /// </summary>
        /// <param name="projectDirPath">Source directory to copy.</param>
        /// <param name="buildDirPath">Target directory to copy the source to.
        /// The names or relative paths may not coincide.</param>
        /// <returns>A copy of this configuration, with a new directory mapping.</returns>
        public BuildConfiguration AddDirMapping(string projectDirPath, string buildDirPath) {
            var clone = new BuildConfiguration( this );
            clone.dirMappings.Add( projectDirPath, buildDirPath );
            return clone;
        }

        /// <summary>
        /// Remove a directory mapping.
        /// </summary>
        /// <param name="projectDirPath">Project path to not copy.</param>
        /// <returns>A copy of this configuration, without the given directory mapping.</returns>
        public BuildConfiguration RemoveDirMapping(string projectDirPath) {
            var clone = new BuildConfiguration( this );
            clone.dirMappings.Remove( projectDirPath );
            return clone;
        }

        /// <summary>
        /// Configure a file which shall be copied to the build data directory.
        /// </summary>
        /// <param name="projectFilePath">Path to the file to copy.</param>
        /// <param name="buildFilePath">Path where the file shall be copied.
        /// Names or relative paths may not match.</param>
        /// <returns>A copy of this configuration, with a new file mapping.</returns>
        public BuildConfiguration AddFileMapping(string projectFilePath, string buildFilePath) {
            var clone = new BuildConfiguration( this );
            clone.fileMappings.Add( projectFilePath, buildFilePath );
            return clone;
        }

        /// <summary>
        /// Remove a file from the set of files to copy.
        /// </summary>
        /// <param name="projectFilePath">Path to the file to not copy.</param>
        /// <returns>A copy of this configuration, without the file mapping.</returns>
        public BuildConfiguration RemoveFile(string projectFilePath) {
            var clone = new BuildConfiguration( this );
            clone.fileMappings.Remove( projectFilePath );
            return clone;
        }

        /// <summary>
        /// Generate the configured build for the specified target.
        /// </summary>
        /// <param name="target">Target platform of the build.</param>
        public void Build(BuildTarget target) {
            if (!Directory.Exists( this.targetDir )) {
                Directory.CreateDirectory( this.targetDir );
            }

            string targetDataDir = null;
            string targetExe = null;

            this.GetTargetPaths( target, out targetDataDir, out targetExe );

            //Build
            string buildMessage = BuildPipeline.BuildPlayer( this.scenes.ToArray(),
                                                             targetExe,
                                                             target,
                                                             this.options );
            bool buildError = !string.IsNullOrEmpty( buildMessage );
            if (buildError) {
                Debug.LogError( "Error building " + targetExe + " for " + target + ": " + buildMessage );
                return;
            } else {
                Debug.Log( new DirectoryInfo( this.targetDir ).FullName + " sucessfully built" );
            }

            if (targetDataDir != null) {
                foreach (var mapping in this.dirMappings) {
                    Directory.CreateDirectory( targetDataDir + "/" + mapping.Value );
                    CopyFilesRecursively(
                      new DirectoryInfo( Application.dataPath + "/" + mapping.Key ),
                      new DirectoryInfo( targetDataDir + "/" + mapping.Value ) );
                }

                foreach (var mapping in this.fileMappings) {
                    FileInfo sourceFile = new FileInfo( Application.dataPath + "/" + mapping.Key );
                    FileInfo targetFile = new FileInfo( targetDataDir + "/" + mapping.Value );
                    if (!sourceFile.Exists) {
                        throw new System.IO.FileNotFoundException( "Unable to copy file.", sourceFile.FullName );
                    }

                    if (!targetFile.Directory.Exists) {
                        BuildConfiguration.CreateHierarchy( targetFile.Directory );
                    }

                    sourceFile.CopyTo( targetFile.FullName, true );
                }
            }

        }

        /// <summary>
        /// Create directory hierarchy.
        /// </summary>
        /// <param name="dir">Highest level directory to create.</param>
        private static void CreateHierarchy(DirectoryInfo dir) {
            if (!dir.Parent.Exists) {
                CreateHierarchy( dir.Parent );
            }

            dir.Create();
        }

        /// <summary>
        /// Copy files and directorys recursively from one directory to another.
        /// 
        /// </summary>
        /// <param name="source">Source directory.</param>
        /// <param name="target">Target directory.</param>
        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target) {
            foreach (DirectoryInfo dir in source.GetDirectories()) {
                string targetDirName = target.FullName + "/" + dir.Name;
                DirectoryInfo targetDir = Directory.Exists( targetDirName )
                  ? new DirectoryInfo( targetDirName )
                  : target.CreateSubdirectory( dir.Name );

                CopyFilesRecursively( dir, targetDir );
            }

            foreach (FileInfo file in source.GetFiles()) {
                if (file.Extension != ".meta") {
                    file.CopyTo( Path.Combine( target.FullName, file.Name ) );
                }
            }
        }

        /// <summary>
        /// Obtain the build data directory and executable of the desired build,
        /// depending on the specified target.
        /// </summary>
        /// <param name="target">Target platform.</param>
        /// <param name="targetDataDir">Path of the data folder of the build,
        /// as currently configured.</param>
        /// <param name="targetExe">Path of the executable of the build,
        /// as currently configured.</param>
        private void GetTargetPaths(BuildTarget target, out string targetDataDir, out string targetExe) {
            switch (target) {
            case BuildTarget.StandaloneLinux:
                targetDataDir = this.targetDir + "/" + this.execName + "_Data/";
                targetExe = this.targetDir + "/" + this.execName;
                break;
            case BuildTarget.StandaloneLinux64:
                targetDataDir = this.targetDir + "/" + this.execName + "_Data/";
                targetExe = this.targetDir + "/" + this.execName;
                break;
            case BuildTarget.StandaloneLinuxUniversal:
                targetDataDir = this.targetDir + "/" + this.execName + "_Data/";
                targetExe = this.targetDir + "/" + this.execName;
                break;
            case BuildTarget.StandaloneOSXIntel:
                targetDataDir = this.targetDir + "/" + this.execName + ".app" + "/Contents/";
                targetExe = this.targetDir + "/" + this.execName + ".app";
                break;
            case BuildTarget.StandaloneOSXIntel64:
                targetDataDir = this.targetDir + "/" + this.execName + ".app" + "/Contents/";
                targetExe = this.targetDir + "/" + this.execName + ".app";
                break;
            case BuildTarget.StandaloneOSXUniversal:
                targetDataDir = this.targetDir + "/" + this.execName + ".app" + "/Contents/";
                targetExe = this.targetDir + "/" + this.execName + ".app";
                break;
            case BuildTarget.StandaloneWindows:
                targetDataDir = this.targetDir + "/" + this.execName + "_Data/";
                targetExe = this.targetDir + "/" + this.execName + ".exe";
                break;
            case BuildTarget.StandaloneWindows64:
                targetDataDir = this.targetDir + "/" + this.execName + "_Data/";
                targetExe = this.targetDir + "/" + this.execName + ".exe";
                break;
            default:
                Debug.LogWarning( "No data dir for target " + target );
                targetDataDir = null;
                targetExe = this.targetDir;
                break;
            }
        }

    }
}
