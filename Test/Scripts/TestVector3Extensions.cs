using UnityEngine;
using UnExt;

public class TestVector3Extensions : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        rotation = Quaternion.Euler( new Vector3( 0, 0, Time.time * 10 ) );
    }

    private Vector3 point = new Vector3( 3, 2, 4 );
    Quaternion rotation = Quaternion.Euler( 0, 0, 0 );
    void OnDrawGizmos() {
        Color prevColor = Gizmos.color;
        Gizmos.color = Color.white;

        Gizmos.DrawLine( Vector3.zero, point.Rotate( rotation ) );
        Gizmos.DrawLine( point, (point * 2).RotateAround( point, rotation ) );

        foreach (var cam in GameObject.FindObjectsOfType<Camera>()) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine( Vector3.zero.Project( cam.transform.forward ), point.Rotate( rotation ).Project( cam.transform.forward ) );
            Gizmos.DrawLine( point.Project( cam.transform.forward ), ((point * 2).RotateAround( point, rotation )).Project( cam.transform.forward ) );

            Gizmos.color = Color.blue;
            Gizmos.DrawLine( Vector3.zero.Project( cam.transform.position, cam.transform.rotation ),
                             point.Rotate( rotation ).Project( cam.transform.position, cam.transform.rotation ) );
            Gizmos.DrawLine( point.Project( cam.transform.position, cam.transform.rotation ),
                             ((point * 2).RotateAround( point, rotation )).Project( cam.transform.position, cam.transform.rotation ) );
        }

        Gizmos.color = prevColor;
    }
}
