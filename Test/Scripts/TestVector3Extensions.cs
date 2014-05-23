using UnityEngine;
using UnExt;

public class TestVector3Extensions : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        rotation = Quaternion.Euler( new Vector3( 0, 0, Time.time * 10 ) );
    }

    void OnGUI() {
        foreach (var cam in GameObject.FindObjectsOfType<Camera>()) {
            GUI.Label( new Rect(50, 50, Screen.width, Screen.height ),
                       "Check Scene tab to see if projections and transformations work properly." );
        }
    }

    private Vector3 point = new Vector3( 3, 2, 4 );
    Quaternion rotation = Quaternion.Euler( 0, 0, 0 );
    void OnDrawGizmos() {
        Color prevColor = Gizmos.color;
        Gizmos.color = Color.white;

        Vector3 secondPoint = (point * 2).RotateAround(point, rotation);

        Gizmos.DrawLine( Vector3.zero, point.Rotate( rotation ) );
        Gizmos.DrawLine( point, secondPoint );

        foreach (var cam in GameObject.FindObjectsOfType<Camera>()) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine( Vector3.zero.Project( cam.transform.forward ), point.Rotate( rotation ).Project( cam.transform.forward ) );
            Gizmos.DrawLine( point.Project( cam.transform.forward ), secondPoint.Project( cam.transform.forward ) );

            Gizmos.color = Color.blue;
            Gizmos.DrawLine( Vector3.zero.Project( cam.transform.position, cam.transform.rotation ),
                             point.Rotate( rotation ).Project( cam.transform.position, cam.transform.rotation ) );
            Gizmos.DrawLine( point.Project( cam.transform.position, cam.transform.rotation ),
                             (secondPoint).Project( cam.transform.position, cam.transform.rotation ) );
        }

        // These two points should never move from (1, 2, 3)
        Vector3 worldPoint = new Vector3( 1, 2, 3 );
        Vector3 localPoint = worldPoint.InverseTransformPoint(secondPoint, rotation);
        Vector3 newWorldPoint = localPoint.TransformPoint(secondPoint, rotation);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(worldPoint, .25f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(newWorldPoint, .5f);

        // This should move with the target point, always at the same distance, rotating around it.
        Vector3 constantLocalPoint = new Vector3(1, 2, 3);
        Vector3 constantWorldPoint = constantLocalPoint.TransformPoint(secondPoint, rotation);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(constantWorldPoint, .75f);

        Gizmos.color = prevColor;
    }
}
