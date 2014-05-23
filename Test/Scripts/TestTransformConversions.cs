using UnityEngine;
using UnExt;

namespace UnExt.Test {
    public class TestTransformConversions : MonoBehaviour {

        public Transform refPoint;
        public Vector3 relPoint = new Vector3(4, 5, 2);

        public Color xAxisColour = Color.red;
        public Color yAxisColour = Color.green;
        public Color zAxisColour = Color.blue;

        public Color worldSpaceColour = Color.green;
        public Color localSpaceColour = Color.blue;

        void OnDrawGizmos() {
            if (refPoint == null || relPoint == null) {
                return;
            }

            var origColor = Gizmos.color;

            DrawAxisGizmos(Vector3.zero, Quaternion.identity);
            DrawAxisGizmos(refPoint.position, refPoint.rotation);

            Gizmos.color = worldSpaceColour;
            Gizmos.DrawWireSphere(refPoint.position, .5f);
            Gizmos.DrawWireSphere(relPoint, .5f);


            Gizmos.color = localSpaceColour;
            Vector3 refLocalPoint = refPoint.PointToLocalSpace(refPoint.position);
            Vector3 relLocalPoint = refPoint.PointToLocalSpace(relPoint);
            Gizmos.DrawWireSphere(refLocalPoint, .25f);
            Gizmos.DrawWireSphere(relLocalPoint, .25f);

            Gizmos.color = worldSpaceColour;
            Vector3 refWorlPoint = refPoint.PointToWorldSpace(refLocalPoint);
            Vector3 relWorldPoint = refPoint.PointToWorldSpace(relLocalPoint);
            Gizmos.DrawWireSphere(refWorlPoint, .15f);
            Gizmos.DrawWireSphere(relWorldPoint, .15f);

            Gizmos.color = origColor;

            // Change base to origin's local space so projecting to (0, 0, 0) is valid.
            Vector3 localPoint = refPoint.PointToLocalSpace(relPoint);

            // xy
            Vector3 localNormal = refPoint.DirectionToLocalSpace(refPoint.forward).normalized;
            Vector3 projPoint = localPoint.Project(localNormal);
            Gizmos.color = zAxisColour;
            Gizmos.DrawLine(Vector3.zero, projPoint);

            // zx
            localNormal = refPoint.DirectionToLocalSpace(refPoint.up).normalized;
            projPoint = localPoint.Project(localNormal);
            Gizmos.color = yAxisColour;
            Gizmos.DrawLine(Vector3.zero, projPoint);

            // zy
            localNormal = refPoint.DirectionToLocalSpace(-refPoint.right).normalized;
            projPoint = localPoint.Project(localNormal);
            Gizmos.color = xAxisColour;
            Gizmos.DrawLine(Vector3.zero, projPoint);

            Gizmos.color = localSpaceColour;
        }

        void OnGUI() {
            GUI.Label(new Rect(50, 50, 500, 20), string.Format("Vector angle from reference to point: {0:0.0000}", refPoint.forward.AngleTo(relPoint - refPoint.position)));
            GUI.Label(new Rect(50, 70, 500, 20), string.Format("Transform angle from reference to point: {0:0.0000}", refPoint.AngleTo(relPoint)));

            GUI.Label(new Rect(50, 90, 500, 20), string.Format("XZ plane angle from reference to point: {0:0.0000}", refPoint.AngleZX(relPoint)));
            GUI.Label(new Rect(50, 110, 500, 20), string.Format("XY plane angle from reference to point: {0:0.0000}", refPoint.AngleXY(relPoint)));
            GUI.Label(new Rect(50, 130, 500, 20), string.Format("ZY plane angle from reference to point: {0:0.0000}", refPoint.AngleZY(relPoint)));

            GUI.Label(new Rect(50, 170, 500, 20), string.Format("Origin.up: {0} Origin.forward: {1} Origing.right: {2}", refPoint.up, refPoint.forward, refPoint.right));
        }

        private void DrawAxisGizmos(Vector3 origin, Quaternion rotation) {
            Gizmos.color = xAxisColour;
            Gizmos.DrawLine(new Vector3(-10, 0, 0).TransformPoint(origin, rotation),
                            new Vector3(10, 0, 0).TransformPoint(origin, rotation));
            for (int i = -10; i < 11; ++i) {
                Gizmos.DrawLine(new Vector3(i, 0, -.1f).TransformPoint(origin, rotation),
                                new Vector3(i, 0, .1f).TransformPoint(origin, rotation));
            }

            Gizmos.color = yAxisColour;
            Gizmos.DrawLine(new Vector3(0, -10, 0).TransformPoint(origin, rotation),
                            new Vector3(0, 10, 0).TransformPoint(origin, rotation));
            for (int i = -10; i < 11; ++i) {
                Gizmos.DrawLine(new Vector3(0, i, -.1f).TransformPoint(origin, rotation),
                                new Vector3(0, i, .1f).TransformPoint(origin, rotation));
            }

            Gizmos.color = zAxisColour;
            Gizmos.DrawLine(new Vector3(0, 0, -10).TransformPoint(origin, rotation),
                            new Vector3(0, 0, 10).TransformPoint(origin, rotation));
            for (int i = -10; i < 11; ++i) {
                Gizmos.DrawLine(new Vector3(-.1f, 0, i).TransformPoint(origin, rotation),
                                new Vector3(.1f, 0, i).TransformPoint(origin, rotation));
            }
        }
    }
}
