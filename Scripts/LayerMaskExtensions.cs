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

        public static LayerMask CreateMask(GameObject go) {
            return new LayerMask() { value = Mathf.RoundToInt(Mathf.Pow(2, go.layer)) };
        }

        public static bool Matches(this LayerMask layerMask, GameObject go) {
            return layerMask.ContainsLayer(CreateMask(go));
        }

        /// <summary>
        /// Determine if a layer mask contains at least one layer from an int mask.
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="layer">An int containing the layers to check.</param>
        /// <returns>
        /// <code>false</code> if none of the layers in layer is in this LayerMask;
        /// <code>true</code> otherwise.
        /// </returns>
        public static bool ContainsLayer(this LayerMask layerMask, int layer) {
            return (layerMask.value & layer) != 0;
        }

        /// <summary>
        /// Determine if a layer mask contains all the layers from an int mask.
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="layer">An int containing the layers to check.</param>
        /// <returns>
        /// <code>true</code> if all of the layers in layer are in this LayerMask;
        /// <code>false</code> otherwise.
        /// </returns>
        public static bool ContainsLayers(this LayerMask layerMask, int layers) {
            return (layerMask.value & layers) == layers;
        }

    }

}
