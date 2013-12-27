using UnityEngine;

namespace UnExt {

    public static class LayerMaskExtensions {

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
