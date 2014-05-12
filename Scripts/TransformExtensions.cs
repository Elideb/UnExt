using UnityEngine;

namespace UnExt {

    public static class TransformExtensions {

        /// <summary>
        /// Syntactic sugar for <see cref="Transform.InverseTransformPoint(Vector3)"/>.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="point">A point in world space coordinates.</param>
        /// <returns>The point in local space coordinates.</returns>
        public static Vector3 PointToLocalSpace(this Transform transform, Vector3 point) {
            return transform.InverseTransformPoint( point );
        }

        /// <summary>
        /// Syntactic sugar for <see cref="Transform.TransformPoint(Vector3)"/>.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="point">A point in local space coordinates.</param>
        /// <returns>The point in world space coordinates.</returns>
        public static Vector3 PointToWorldSpace(this Transform transform, Vector3 point) {
            return transform.TransformPoint( point );
        }

        /// <summary>
        /// Syntactic sugar for <see cref="Transform.InverseTransformDirection(Vector3)"/>.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="point">A direction in world space coordinates.</param>
        /// <returns>The direction in local space coordinates.</returns>
        public static Vector3 DirectionToLocalSpace(this Transform transform, Vector3 direction) {
            return transform.InverseTransformDirection( direction );
        }

        /// <summary>
        /// Syntactic sugar for <see cref="Transform.TransformDirection(Vector3)"/>.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="point">A direction in local space coordinates.</param>
        /// <returns>The direction in world space coordinates.</returns>
        public static Vector3 DirectionToWorldSpace(this Transform transform, Vector3 direction) {
            return transform.TransformDirection( direction );
        }

        /// <summary>
        /// Calculate the projection of a point on the plane defined by this transform.
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="point">Point to project into the transform's plane.</param>
        /// <returns>A point inside the transform's plane.</returns>
        public static Vector3 ProjectPointInPlane(this Transform plane, Vector3 point) {
            Vector3 relativePos = plane.InverseTransformPoint( point );
            relativePos.z = 0;
            return plane.TransformPoint( relativePos );
        }

        /// <summary>
        /// Calculate the projection of a direction on the plane defined by this transform.
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="point">Direction to project into the transform's plane.</param>
        /// <returns>A direction parallel to the plane.</returns>
        public static Vector3 ProjectDirectionInPlane(this Transform plane, Vector3 direction) {
            Vector3 relativeDir = plane.InverseTransformDirection( direction );
            relativeDir.z = 0;
            return plane.TransformDirection( relativeDir );
        }
    }
}
