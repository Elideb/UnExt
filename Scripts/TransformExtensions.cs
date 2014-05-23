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

        /// <summary>
        /// Calculate the angle in degrees from a transform's forward to a point,
        /// in the [0, 180] range.
        /// </summary>
        /// <param name="origin">Reference point and rotation.</param>
        /// <param name="point">Point to calculate the angle to.</param>
        /// <returns>The degrees from origin's forward to point, in the [180, 180] range.</returns>
        public static float AngleTo(this Transform origin, Vector3 point) {
            return origin.forward.AngleTo(point.InverseTransformPoint(origin.position, origin.rotation));
        }

        /// <summary>
        /// Calculate the angle, in degrees [-180, 180], from a transform to a point
        /// around a given axis, taking a given vector as the forward direction.
        /// </summary>
        /// <param name="origin">Transform to measure the angle from.</param>
        /// <param name="point">Point to measure the angle to.</param>
        /// <param name="axis">The normal of the plane to calculate the angle on.</param>
        /// <param name="forward">Which direction to use as origin's forward.</param>
        /// <returns>The angle, in degrees, from origin's selected forward to the point,
        /// around the given axis.</returns>
        public static float AngleTo(this Transform origin, Vector3 point, Vector3 axis, Vector3 forward) {
            // Change base to origin's local space so projecting to (0, 0, 0) is valid.
            Vector3 localPoint = PointToLocalSpace(origin, point);
            Vector3 localNormal = DirectionToLocalSpace(origin, axis).normalized;

            Vector3 projDir = DirectionToLocalSpace(origin, forward).Project(localNormal).normalized;
            Vector3 projPoint = localPoint.Project(localNormal);

            float angle = Vector3.Angle(projDir, projPoint);

            // All these operations take place in the local plane, so localPlane is the positive normal always
            Vector3 cross = projDir.Cross(projPoint);
            float dot = localNormal.Dot(cross);
            return dot > 0 ? angle : -angle;
        }

        /// <summary>
        /// Calculate the angle, in degrees [-180, 180], from a transform to a point
        /// around the transform's Y axis.
        /// </summary>
        /// <param name="origin">Transform to measure the angle from.</param>
        /// <param name="point">Point to measure the angle to.</param>
        /// <returns>The angle, in degrees, from origin's forward to the point,
        /// around origin's Y axis.</returns>
        public static float AngleZX(this Transform origin, Vector3 point) {
            return AngleTo(origin, point, origin.up, origin.forward);
        }

        /// <summary>
        /// Calculate the angle, in degrees [-180, 180], from a transform to a point
        /// around the transform's Z axis.
        /// </summary>
        /// <param name="origin">Transform to measure the angle from.</param>
        /// <param name="point">Point to measure the angle to.</param>
        /// <returns>The angle, in degrees, from origin's right to the point,
        /// around origin's Z axis.</returns>
        public static float AngleXY(this Transform origin, Vector3 point) {
            return AngleTo(origin, point, origin.forward, origin.right);
        }

        /// <summary>
        /// Calculate the angle, in degrees [-180, 180], from a transform to a point
        /// around the transform's X axis.
        /// </summary>
        /// <param name="origin">Transform to measure the angle from.</param>
        /// <param name="point">Point to measure the angle to.</param>
        /// <returns>The angle, in degrees, from origin's forward to the point,
        /// around origin's X axis.</returns>
        public static float AngleZY(this Transform origin, Vector3 point) {
            return AngleTo(origin, point, -origin.right, origin.forward);
        }
    }
}
