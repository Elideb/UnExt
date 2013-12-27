using UnityEngine;

namespace UnExt {

    public static class VectorExtensions {

        #region Vector3 extension methods

        /// <summary>
        /// Magnitude of the vector in the XZ plane.
        /// </summary>
        /// <param name="v">Vector to calculate the magnitude of.</param>
        /// <returns>The magnitude of v in the XZ plane.</returns>
        public static float magnitudeXZ(this Vector3 v) {
            return new Vector2( v.x, v.z ).magnitude;
        }

        /// <summary>
        /// Squared magnitude of the vector in the XZ plane.
        /// </summary>
        /// <param name="v">Vector to calculate the magnitude of.</param>
        /// <returns>The squared magnitude of v in the XZ plane.</returns>
        public static float sqrMagnitudeXZ(this Vector3 v) {
            return new Vector2( v.x, v.z ).sqrMagnitude;
        }

        /// <summary>
        /// Magnitude of the vector in the XY plane.
        /// </summary>
        /// <param name="v">Vector to calculate the magnitude of.</param>
        /// <returns>The magnitude of v in the XY plane.</returns>
        public static float magnitudeXY(this Vector3 v) {
            return new Vector2( v.x, v.y ).magnitude;
        }

        /// <summary>
        /// Squared magnitude of the vector in the XY plane.
        /// </summary>
        /// <param name="v">Vector to calculate the magnitude of.</param>
        /// <returns>The squared magnitude of v in the XY plane.</returns>
        public static float sqrMagnitudeXY(this Vector3 v) {
            return new Vector2( v.x, v.y ).sqrMagnitude;
        }

        /// <summary>
        /// Magnitude of the vector in the YZ plane.
        /// </summary>
        /// <param name="v">Vector to calculate the magnitude of.</param>
        /// <returns>The magnitude of v in the YZ plane.</returns>
        public static float magnitudeYZ(this Vector3 v) {
            return new Vector2( v.y, v.z ).magnitude;
        }

        /// <summary>
        /// Squared magnitude of the vector in the YZ plane.
        /// </summary>
        /// <param name="v">Vector to calculate the magnitude of.</param>
        /// <returns>The squared magnitude of v in the YZ plane.</returns>
        public static float sqrMagnitudeYZ(this Vector3 v) {
            return new Vector2( v.y, v.z ).sqrMagnitude;
        }

        /// <summary>
        /// Distance to a second point, in the XZ plane.
        /// </summary>
        /// <param name="v">Origin point.</param>
        /// <param name="u">Destination point.</param>
        /// <returns>The distance from origin to destination in the XZ plane.</returns>
        public static float distanceXZ(this Vector3 v, Vector3 u) {
            return (u - v).magnitudeXZ();
        }

        /// <summary>
        /// Squared distance to a second point, in the XZ plane.
        /// </summary>
        /// <param name="v">Origin point.</param>
        /// <param name="u">Destination point.</param>
        /// <returns>The squared distance from origin to destination in the XZ plane.</returns>
        public static float sqrDistanceXZ(this Vector3 v, Vector3 u) {
            return (u - v).sqrMagnitudeXZ();
        }

        /// <summary>
        /// Distance to a second point, in the XY plane.
        /// </summary>
        /// <param name="v">Origin point.</param>
        /// <param name="u">Destination point.</param>
        /// <returns>The distance from origin to destination in the XY plane.</returns>
        public static float distanceXY(this Vector3 v, Vector3 u) {
            return (u - v).magnitudeXY();
        }

        /// <summary>
        /// Squared distance to a second point, in the XY plane.
        /// </summary>
        /// <param name="v">Origin point.</param>
        /// <param name="u">Destination point.</param>
        /// <returns>The squared distance from origin to destination in the XY plane.</returns>
        public static float sqrDistanceXY(this Vector3 v, Vector3 u) {
            return (u - v).sqrMagnitudeXY();
        }

        /// <summary>
        /// Distance to a second point, in the YZ plane.
        /// </summary>
        /// <param name="v">Origin point.</param>
        /// <param name="u">Destination point.</param>
        /// <returns>The distance from origin to destination in the YZ plane.</returns>
        public static float distanceYZ(this Vector3 v, Vector3 u) {
            return (u - v).magnitudeYZ();
        }

        /// <summary>
        /// Squared distance to a second point, in the YZ plane.
        /// </summary>
        /// <param name="v">Origin point.</param>
        /// <param name="u">Destination point.</param>
        /// <returns>The squared distance from origin to destination in the YZ plane.</returns>
        public static float sqrDistanceYZ(this Vector3 v, Vector3 u) {
            return (u - v).sqrMagnitudeYZ();
        }

        /// <summary>
        /// Distance to a second point.
        /// </summary>
        /// <param name="v">Origin point.</param>
        /// <param name="u">Destination point.</param>
        /// <returns>The distance from origin to destination.</returns>
        public static float distance(this Vector3 v, Vector3 u) {
            return (u - v).magnitude;
        }

        /// <summary>
        /// Squared distance to a second point.
        /// </summary>
        /// <param name="v">Origin point.</param>
        /// <param name="u">Destination point.</param>
        /// <returns>The squared distance from origin to destination.</returns>
        public static float sqrDistance(this Vector3 v, Vector3 u) {
            return (u - v).sqrMagnitude;
        }

        /// <summary>
        /// Calculate the projection of a vector into the plane defined by the given normal
        /// and containing the origin.
        /// </summary>
        /// <param name="v">Vector to project.</param>
        /// <param name="normal">Normal defining the plane to project on.</param>
        /// <returns>The vector resulting from projecting v on the plane.</returns>
        public static Vector3 Project(this Vector3 v, Vector3 normal) {
            return v - v.Dot( normal ) * normal;
        }

        /// <summary>
        /// Project this point into a plane defined by the given position and rotation.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="position">Reference position of the plane.</param>
        /// <param name="rotation">Rotation of the plane.</param>
        /// <returns>The point's projection into the plane.</returns>
        public static Vector3 Project(this Vector3 v, Vector3 position, Quaternion rotation) {
            var localv = rotation.Inverse() * v;
            var localPos = rotation.Inverse() * position;
            localv.z = localPos.z;
            return rotation * localv;
        }

        /// <summary>
        /// Calculate the magnitude of a vector's projection on an arbitrary plane.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="normal">Normal defining the plane to project on.</param>
        /// <returns>The magnitude of the vector's projection on the plane.</returns>
        public static float magnitude(this Vector3 v, Vector3 normal) {
            return v.Project( normal ).magnitude;
        }

        /// <summary>
        /// Calculate the squared magnitude of a vector's projection on an arbitrary plane.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="normal">Normal defining the plane to project on.</param>
        /// <returns>The squared magnitude of the vector's projection on the plane.</returns>
        public static float sqrMagnitude(this Vector3 v, Vector3 normal) {
            return v.Project( normal ).sqrMagnitude;
        }

        /// <summary>
        /// Calculate the distance between two points' projections on an arbitrary plane.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="normal">Normal defining the plane to project on.</param>
        /// <returns>The distance between the two points on the plane.</returns>
        public static float distance(this Vector3 v, Vector3 u, Vector3 normal) {
            return v.distance( u, Vector3.zero, Quaternion.LookRotation( normal ) );
        }

        /// <summary>
        /// Calculate the distance between two point's projections on a plane.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="u"></param>
        /// <param name="planePos">An arbitrary point inside the plane.</param>
        /// <param name="planeRot">The plane's rotation.</param>
        /// <returns>The distance of both points' projection in the plane.</returns>
        public static float distance(this Vector3 v, Vector3 u, Vector3 planePos, Quaternion planeRot) {
            var localv = v.Project( planePos, planeRot );
            var localu = u.Project( planePos, planeRot );
            return localv.distance( localu );
        }

        /// <summary>
        /// Calculate the squared distance between two points' projections on an arbitrary plane.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="normal">Normal defining the plane to project on.</param>
        /// <returns>The squared distance between the two points on the plane.</returns>
        public static float sqrDistance(this Vector3 v, Vector3 u, Vector3 normal) {
            var localv = v.Project( normal );
            var localu = u.Project( normal );
            return localv.distance( localu );
        }

        /// <summary>
        /// Calculate the squared distance between two point's projections on a plane.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="u"></param>
        /// <param name="planePos">An arbitrary point inside the plane.</param>
        /// <param name="planeRot">The plane's rotation.</param>
        /// <returns>The squared distance of both points' projection in the plane.</returns>
        public static float sqrDistance(this Vector3 v, Vector3 u, Vector3 planePos, Quaternion planeRot) {
            var localv = v.Project( planePos, planeRot );
            var localu = u.Project( planePos, planeRot );
            return localv.sqrDistance( localu );
        }

        /// <summary>
        /// Calculate the dot product of a couple of vectors.
        /// i.e. the product of their magnitudes and the cosine between both vectors.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="u"></param>
        /// <returns>The dot product of v and u.</returns>
        public static float Dot(this Vector3 v, Vector3 u) {
            return Vector3.Dot( v, u );
        }

        /// <summary>
        /// Calculate the cross product of two vectors
        /// (i.e. a vector pointing in the direction of the normal
        /// of the plane defined by both vectors, following the lowest angle between both,
        /// and of magnitude the area of the parallelogram with both vectors as its sides.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="u"></param>
        /// <returns>The cross product of v and u.</returns>
        public static Vector3 Cross(this Vector3 v, Vector3 u) {
            return Vector3.Cross( v, u );
        }

        /// <summary>
        /// Rotate a point around the origin.
        /// </summary>
        /// <param name="point">Vector to rotate.</param>
        /// <param name="angle">Angle to rotate.</param>
        /// <returns>The result of rotating point.</returns>
        public static Vector3 Rotate(this Vector3 point, Quaternion angle) {
            return angle * point;
        }

        /// <summary>
        /// Rotate a point around another.
        /// </summary>
        /// <param name="point">Point to rotate.</param>
        /// <param name="origin">Reference for rotation.</param>
        /// <param name="angle">Angle to rotate.</param>
        /// <returns>The result of rotating point around origin.</returns>
        public static Vector3 RotateAround(this Vector3 point, Vector3 origin, Quaternion angle) {
            return angle * (point - origin) + origin;
        }

        #endregion

        #region Vector2 extension methods

        /// <summary>
        /// Distance to a second point.
        /// </summary>
        /// <param name="v">Origin point.</param>
        /// <param name="u">Destination point.</param>
        /// <returns>The distance from origin to destination.</returns>
        public static float distance(this Vector2 v, Vector2 u) {
            return (u - v).magnitude;
        }

        /// <summary>
        /// Squared distance to a second point.
        /// </summary>
        /// <param name="v">Origin point.</param>
        /// <param name="u">Destination point.</param>
        /// <returns>The squared distance from origin to destination.</returns>
        public static float sqrDistance(this Vector2 v, Vector2 u) {
            return (u - v).sqrMagnitude;
        }

        /// <summary>
        /// Rotate a Vector2.
        /// </summary>
        /// <param name="point">Vector to rotate.</param>
        /// <param name="angle">Angle to rotate.</param>
        /// <returns>The result of rotating point.</returns>
        public static Vector2 Rotate(this Vector2 point, Quaternion angle) {
            Vector3 point3 = new Vector3( point.x, 0, point.y );
            point3 = angle * point3;

            return new Vector2( point3.x, point3.z );
        }

        /// <summary>
        /// Rotate around a given point.
        /// </summary>
        /// <param name="point">Point to rotate.</param>
        /// <param name="origin">Reference for rotation.</param>
        /// <param name="angle">Angle to rotate.</param>
        /// <returns>The result of rotating point around origin.</returns>
        public static Vector2 RotateAround(this Vector2 point, Vector2 origin, Quaternion angle) {
            Vector3 point3 = new Vector3( point.x, 0, point.y );
            Vector3 origin3 = new Vector3( origin.x, 0, origin.y );
            point3 = angle * (point3 - origin3) + origin3;

            return new Vector2( point3.x, point3.z );
        }

        /// <summary>
        /// Calculate two vector's dot product. Sugar for <see cref="Vector2.Dot(Vector2, Vector2)"/>
        /// </summary>
        /// <param name="v"></param>
        /// <param name="u"></param>
        /// <returns>The vector's dot product.</returns>
        public static float Dot(this Vector2 v, Vector2 u) {
            return Vector2.Dot( v, u );
        }

        #endregion

    }
}
