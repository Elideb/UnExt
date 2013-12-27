using UnityEngine;

namespace UnExt {

    public static class QuaternionExtensions {

        /// <summary>
        /// Obtain the inverse of a quaternion. Syntax sugar for Quaternion.Inverse(quaternion).
        /// </summary>
        /// <param name="rotation">Rotation to obtain the inverse of.</param>
        /// <returns>The inverse of the given rotation.</returns>
        public static Quaternion Inverse(this Quaternion rotation) {
            return Quaternion.Inverse( rotation );
        }

        /// <summary>
        /// Return the quaternion difference between two quaternions.
        /// </summary>
        /// <param name="start">Start rotation.</param>
        /// <param name="end">End rotation.</param>
        /// <returns></returns>
        public static Quaternion RotationTo(this Quaternion start, Quaternion end) {
            return end * start.Inverse();
        }
    }
}
