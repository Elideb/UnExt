using UnityEngine;

namespace UnExt {

    public static class LayerMaskExtensions {

        /// <summary>
        /// Convert a layer mask (power of two) to its corresponding
        /// game object layer value (binary mask with a single 1).
        /// </summary>
        /// <param name="layerMask"></param>
        /// <returns>n such that 2 to the power of n = layer mask value.</returns>
        public static int MaskValue(this LayerMask layerMask) {
            int value = layerMask.value;

            for (int mask = 0; mask < 32; ++mask) {
                if (value == 1 << mask) {
                    return mask;
                }
            }

            throw new System.InvalidOperationException( "Cannot calculate a mask for a non power of two value" );
        }

    }

}
