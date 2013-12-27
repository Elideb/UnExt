using UnityEngine;
using System.Collections.Generic;

namespace UnExt {

    public static class Texture2DExtensions {

        /// <summary>
        /// Paint an outline around painted areas of the texture.
        /// Warning: this operation applies the modifications directly to the texture/asset,
        /// so don't use it on your original art.
        /// If in doubt,
        /// use <see cref="Texture2dExtensions.Outlined"/> to create a new outlined texture.
        /// </summary>
        /// <param name="tex">Texture to the draw outline on.</param>
        /// <param name="outlineColor">Color to use for the outline.</param>
        /// <param name="outlineWidth">Width of the outline.</param>
        public static void Outline(this Texture2D tex, Color outlineColor, uint outlineWidth) {
            if (outlineWidth == 0) {
                return;
            }

            var pixels = tex.GetPixels();

            bool[] checkedIdx = new bool[pixels.Length];
            for (int i = 0; i < checkedIdx.Length; ++i) {
                checkedIdx[i] = false;
            }

            int width = tex.width;
            int height = tex.height;
            for (int idx = 0; idx < pixels.Length; ++idx) {
                Color pixel = pixels[idx];
                if (!checkedIdx[idx]) {
                    if (pixel.a > .2f) {
                        checkedIdx[idx] = true;
                        CreatePixelOutline( idx,
                                            width,
                                            height,
                                            checkedIdx,
                                            (int)outlineWidth,
                                            outlineColor,
                                            pixels );
                    }
                }
            }

            tex.SetPixels( pixels );
            tex.Apply();
        }

        /// <summary>
        /// Paint an outline around painted areas of the texture in a new texture.
        /// </summary>
        /// <param name="tex">Texture to create outlined texture from.</param>
        /// <param name="outlineColor">Color to use for the outline.</param>
        /// <param name="outlineWidth">Width of the outline.</param>
        /// <returns>A new texture, equal to the original, but outlined.</returns>
        public static Texture2D Outlined(this Texture2D tex, Color outlineColor, uint outlineWidth) {
            Texture2D newTex = tex.CreateCopy();
            newTex.Outline( outlineColor, outlineWidth );
            return newTex;
        }

        /// <summary>
        /// Create a complete copy of a texture into another.
        /// </summary>
        /// <param name="tex">Texture to produce the copy from.</param>
        /// <returns>A new texture identical to the original.</returns>
        public static Texture2D CreateCopy(this Texture2D tex) {
            Texture2D newTex = new Texture2D( tex.width, tex.height, tex.format, tex.mipmapCount > 1 );
            newTex.mipMapBias = tex.mipMapBias;
            for (int mipLevel = 0; mipLevel < tex.mipmapCount; ++mipLevel) {
                newTex.SetPixels( tex.GetPixels(), mipLevel );
            }

            newTex.Apply();
            return newTex;
        }

        /// <summary>
        /// Set to 0 all alpha values below a threshold.
        /// Warning: this operation applies the modifications directly to the texture/asset,
        /// so don't use it on your original art.
        /// If in doubt,
        /// use <see cref="Texture2DExtensions.ClearedAlpha"/> to create a new alpha cleared texture.
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="threshold">Alpha value below which alpha is set to 0.</param>
        public static void ClearAlpha(this Texture2D tex, float threshold = .05f) {
            var pixels = tex.GetPixels();

            for (int i = 0; i < pixels.Length; ++i) {
                if (pixels[i].a < threshold) {
                    pixels[i].a = 0;
                }
            }

            tex.SetPixels( pixels );
            tex.Apply();
        }

        /// <summary>
        /// Create a copy of the texture with all alpha values below threshold set to 0.
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="threshold">Alpha value below which alpha is set to 0.</param>
        /// <returns>A new texture identical to the first, with alpha values cleared.</returns>
        public static Texture2D ClearedAlpha(this Texture2D tex, float threshold = .05f) {
            var copyTex = tex.CreateCopy();
            copyTex.ClearAlpha( threshold );
            return copyTex;
        }

        /// <summary>
        /// Obtain the index in the texture pixels for a given pixel coordinate.
        /// This method does not test that the input coordinates are valid,
        /// so it can be used to check offsets or whatever.
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="w">Pixel horizontal coordinate.</param>
        /// <param name="h">Pixel vertical coordinate.</param>
        /// <returns></returns>
        public static int GetPixelIndex(this Texture2D tex, int w, int h) {
            return Texture2DExtensions.TexPosToIndex( w, h, tex.width, tex.height );
        }

        private static int TexPosToIndex(int w, int h, int width, int height) {
            return h * width + w;
        }

        /// <summary>
        /// Obtain the coordinates of a pixel from its index in the texture pixels.
        /// This method does not test that the index is valid,
        /// so it can be used to calculate offsets or whatever.
        /// </summary>
        /// <param name="tex"></param>
        /// <param name="idx">Pixel index.</param>
        /// <param name="w">Pixel horizontal coordinate.</param>
        /// <param name="h">Pixel vertical coordinate.</param>
        public static void GetPixelCoordinates(this Texture2D tex, int idx, out int w, out int h) {
            Texture2DExtensions.TexIndexToPos( idx, tex.width, tex.height, out w, out h );
        }

        private static void TexIndexToPos(int idx, int width, int height, out int w, out int h) {
            h = idx / width;
            w = idx - h * width;
        }

        /// <summary>
        /// Paint an outline around a single pixel.
        /// Should be called only on pixels marked as checked and not transparent.
        /// </summary>
        /// <param name="idx">Index of the pixel to paint.</param>
        /// <param name="width">Width of the texture.</param>
        /// <param name="height">Height of the texture.</param>
        /// <param name="checkedIdx">
        /// Array of already checked pixels. It is modified by this method.
        /// </param>
        /// <param name="steps">Number of pixels to paint around the given index.</param>
        /// <param name="color">Color to use for the outline.</param>
        /// <param name="pixels">Pixels of the texture.</param>
        private static void CreatePixelOutline(int idx,
                                               int width,
                                               int height,
                                               bool[] checkedIdx,
                                               int steps,
                                               Color color,
                                               Color[] pixels) {

            int x, y;
            TexIndexToPos( idx, width, height, out x, out y );

            for (int dx = 0; dx <= steps; ++dx) {
                for (int dy = 0; dy <= steps - dx; ++dy) {
                    CheckPixel( x + dx, y + dy, width, height, pixels, checkedIdx, color );
                    CheckPixel( x - dx, y + dy, width, height, pixels, checkedIdx, color );
                    CheckPixel( x + dx, y - dy, width, height, pixels, checkedIdx, color );
                    CheckPixel( x - dx, y - dy, width, height, pixels, checkedIdx, color );
                }
            }
        }

        private static void CheckPixel(int x,
                                       int y,
                                       int width,
                                       int height,
                                       Color[] pixels,
                                       bool[] checkedIdx,
                                       Color color) {

            if (x < 0 || x >= width || y < 0 || y >= height) {
                return;
            }

            int neighbour = TexPosToIndex( x, y, width, height );
            if (!checkedIdx[neighbour] && pixels[neighbour].a < .2f) {
                checkedIdx[neighbour] = true;
                pixels[neighbour] = color;
            }
        }

    }
}
