using UnityEditor;
using UnExt.Editor;

public class TestBuildConfiguration {

    private static BuildConfiguration baseConfig = BuildConfiguration.Create()
                                                   .SetExecName( "CoolName" )
                                                   .AddScene( "OutlineTest" )
                                                   .AddBuildOption( BuildOptions.ShowBuiltPlayer )
                                                   .AddBuildOption( BuildOptions.AutoRunPlayer )
                                                   .AddBuildOption( BuildOptions.Development )
                                                   .RemoveBuildOption( BuildOptions.AutoRunPlayer )
                                                   .AddFileMapping( "Sprites/Ship.png", "Ship.png" )
                                                   .AddDirMapping( "Sprites", "Sprites" );

    [MenuItem( "Build/Win" )]
    public static void BuildWin() {
        baseConfig.SetTargetDir( "../../Builds/Win" )
                  .Build( BuildTarget.StandaloneWindows );
    }

    [MenuItem( "Build/Win 64" )]
    public static void BuildWin64() {
        baseConfig.SetTargetDir( "../../Builds/Win64" )
                  .Build( BuildTarget.StandaloneWindows64 );
    }

    [MenuItem( "Build/OSX" )]
    public static void BuildOSX() {
        baseConfig.SetTargetDir( "../../Builds/OSX" )
                  .Build( BuildTarget.StandaloneOSXIntel );
    }

    [MenuItem( "Build/OSX 64" )]
    public static void BuildOSX64() {
        baseConfig.SetTargetDir( "../../Builds/OSX64" )
                  .Build( BuildTarget.StandaloneOSXIntel64 );
    }

    [MenuItem( "Build/OSX" )]
    public static void BuildOSXUniversal() {
        baseConfig.SetTargetDir( "../../Builds/OSXUniversal" )
                  .Build( BuildTarget.StandaloneOSXUniversal );
    }

    [MenuItem( "Build/Linux" )]
    public static void BuildLinux() {
        baseConfig.SetTargetDir( "../../Builds/linux_x86" )
                  .SetExecName( baseConfig.ExecName.ToLower() )
                  .RemoveBuildOption( BuildOptions.AutoRunPlayer )
                  .Build( BuildTarget.StandaloneLinux );
    }

    [MenuItem( "Build/Linux" )]
    public static void BuildLinux64() {
        baseConfig.SetTargetDir( "../../Builds/linux_x86_64" )
                  .SetExecName( baseConfig.ExecName.ToLower() )
                  .RemoveBuildOption( BuildOptions.AutoRunPlayer )
                  .Build( BuildTarget.StandaloneLinux64 );
    }

    [MenuItem( "Build/Linux Universal" )]
    public static void BuildLinuxUniversal() {
        baseConfig.SetTargetDir( "../../Builds/linux" )
                  .SetExecName( baseConfig.ExecName.ToLower() )
                  .RemoveBuildOption( BuildOptions.AutoRunPlayer )
                  .Build( BuildTarget.StandaloneLinuxUniversal );
    }

    [MenuItem( "Build/Web" )]
    public static void BuildWeb() {
        baseConfig.SetTargetDir( "../../Builds/Web" )
                  .RemoveBuildOption( BuildOptions.AutoRunPlayer )
                  .Build( BuildTarget.WebPlayer );
    }

    [MenuItem( "Build/All" )]
    public static void BuildAll() {
        BuildWin();
        BuildWin64();
        BuildLinux();
        BuildLinux64();
        BuildLinuxUniversal();
        BuildOSX();
        BuildOSX64();
        BuildOSXUniversal();
        BuildWeb();
    }
}
