UnExt : Unity Extensions Library
================================
Collection of extension methods and helpers for Unity 3D.
Runtime extensions are defined in the namespace UnExt, while editor extensions are defined in UnExt.Editor.
Check the test scripts to get an idea of what is implemented and how to use it.

License
=======
None. Do as you please with this.
If you make improvements or add new methods, I'll gladly merge them in here,
but you are not forced to publish them, as I am not forced to accept them.

Legal jabbadabbadoo
-------------------
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to [http://unlicense.org]

Runtime extensions
==================

GameObject extension methods
----------------------------
Change a complete hierarchy to a given layer

    void SetLayerRecursive(int layer)

LayerMask extension methods
---------------------------
LayerMask value (power of two) to layer value (binary mask with a single 1)

    int MaskValue()

Object extension methods
------------------------
Generic sugar

    T Instantiate<T>(T)
    T Instantiate<T>(T)

Quaternion extension methods
----------------------------
Sugar

    Quaternion Inverse()

Rotation between to rotations

    Quaternion RotationTo(Quaternion end)

Texture2D extension methods
---------------------------
Add an outline around non-transparent areas of a texture

    void Outline(Color, uint)
    Texture2D Outlined(Color, uint)

Copy a texture to a new one

    Texture2D CreateCopy()

Clear all pixels with alpha below a given threshold

    void ClearAlpha(float)
    Texture2D ClearedAlpha(float)

Index to coordinates

    int GetPixelIndex(int, int)
    void GetPixelCoordinates(int idx, out int, out int)onding pixel coordinates.

Transform extension methods
---------------------------
Sugar

    Vector3 PointToLocalSpace(Vector3)
    Vector3 PointToWorldSpace(Vector3)
    Vector3 DirectionToLocalSpace(Vector3)
    Vector3 DirectionToWorldSpace(Vector3)

Projections

    Vector3 ProjectPointInPlane(Vector3)
    Vector3 ProjectDirectionInPlane(Vector3)

Vector3 extension methods
-------------------------
Magnitudes and distances on basic planes.

    float magnitudeXZ()
    float sqrMagnitudeXZ()
    float magnitudeXY()
    float sqrMagnitudeXY()
    float magnitudeYZ()
    float sqrMagnitudeYZ()
    float distanceXZ()
    float sqrDistanceXZ()
    float distanceXY()
    float sqrDistanceXY()
    float distanceYZ()
    float sqrDistanceYZ()
    float distance(Vector3)
    float sqrDistance(Vector3)

Projection of the plane to arbitrary planes.

    Vector3 Project(Vector3)
    Vector3 Project(Vector3, Quaternion)

Magnitudes and distances on arbitrary planes.

    float magnitude(Vector3)
    float sqrMagnitude(Vector3)
    float distance(Vector3, Vector3)
    float sqrDistance(Vector3, Vector3)
    float distance(Vector3, Vector3, Quaternion)
    float sqrDistance(Vector3, Vector3, Quaternion)

Sugar

    float Dot(Vector3)
    Vector3 Cross(Vector3)

Rotations

    Vector3 Rotate(Quaternion)
    Vector3 RotateAround(Vector3, Quaternion)

Vector2 extension methods
-------------------------
Distances in 2 dimensions.

    float distance(Vector2)
    float sqrDistance(Vector2)

Rotations

    Vector2 Rotate(Quaternion)
    Vector2 RotateAround(Vector2, Quaternion)

Sugar

    float Dot(Vector2)

Build Helper methods
====================

BuildConfiguration
------------------
Use this class to ease creating and reusing build configurations, plus automating copying files to new builds.
Note: In order to create builds using scripts, a Unity Pro license is required (as of Unity 4.1.3).

Example:

    // Create a configuration with common parameters for builds.
    BuildConfiguration baseConfig = BuildConfiguration.Create()
                                                      .SetExecName( "CoolName" )
                                                      .AddScene( "OutlineTest" )
                                                      .AddBuildOption( BuildOptions.ShowBuiltPlayer )
                                                      .AddBuildOption( BuildOptions.AutoRunPlayer )
                                                      .AddFileMapping( "Sprites/Ship.png", "Ship.png" )
                                                      .AddDirMapping( "Sprites", "Sprites" );

    // Create a basic windows build
    baseConfig.SetTargetDir( "../../Builds/Win" )
              .Build( BuildTarget.StandaloneWindows );

    // Create a Linux build with customized settings.
    // Each call to base config methods (except build)
    // generates a new configuration, so baseConfig remains unchanged.
    baseConfig.SetTargetDir( "../../Builds/linux_x86" )
              .SetExecName( baseConfig.ExecName.ToLower() )
              .RemoveBuildOption( BuildOptions.AutoRunPlayer )
              .Build( BuildTarget.StandaloneLinux );

    // Retains the original exec name and auto run setting.
    baseConfig.SetTargetDir( "../../Builds/OSXUniversal" )
              .Build( BuildTarget.StandaloneOSXUniversal );

