using UnityEngine;
using System.Collections.Generic;

namespace UnExt {

    public static class GameObjectExtensions {

        /// <summary>
        /// Set a game object hierarchy to the given layer.
        /// </summary>
        /// <param name="go">Parent of the hierarchy.</param>
        /// <param name="layer">A layer value, in the range [0..32).</param>
        public static void SetLayerRecursive(this GameObject go, int layer) {
            go.layer = layer;

            foreach(Transform trans in go.transform){
                trans.gameObject.SetLayerRecursive( layer );
            }
        }
    }
}