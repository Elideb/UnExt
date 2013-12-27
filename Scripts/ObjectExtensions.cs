using UnityEngine;

namespace UnExt {

    public static class ObjectExtensions {

        /// <summary>
        /// Instantiate a new Unity object, using MonoBehaviour.Instantiate.
        /// </summary>
        /// <typeparam name="T">Type of the object to duplicate.</typeparam>
        /// <param name="original">Object to replicate.</param>
        /// <returns>A new object of the specified type.</returns>
        public static T Instantiate<T>(this UnityEngine.Object _, T original) where T : UnityEngine.Object {
            return (T)UnityEngine.Object.Instantiate( original );
        }

        /// <summary>
        /// Instantiate a new Unity object, using MonoBehaviour.Instantiate.
        /// </summary>
        /// <typeparam name="T">Type of the object to duplicate.</typeparam>
        /// <param name="original">Object to replicate.</param>
        /// <returns>A new object of the specified type.</returns>
        public static T Instantiate<T>(this UnityEngine.Object _,
                                       T original,
                                       Vector3 position,
                                       Quaternion rotation) where T : UnityEngine.Object {

            return (T)UnityEngine.Object.Instantiate( original, position, rotation );
        }
    }
}
