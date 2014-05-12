using UnityEngine;
using UnExt;

public class TestTransformExtensions : MonoBehaviour {

    private GameObject origin;
    private GameObject target;
    private Vector3 point = new Vector3( 3, 2, 4 );
    Quaternion rotation = Quaternion.Euler( 0, 0, 0 );

    void Start() {
        this.origin = new GameObject( "Origin" );
        this.origin.transform.position = Vector3.zero;
        this.target = new GameObject( "Target" );
        this.target.transform.position = this.point;
    }

    // Update is called once per frame
    void Update() {
        rotation = Quaternion.Euler( new Vector3( 0, 0, Time.time * 10 ) );
        target.transform.position = point.Rotate( rotation );
    }

    void OnDrawGizmos() {
        if (this.origin != null && this.target != null) {
            Color prevColor = Gizmos.color;
            Gizmos.color = Color.magenta;

            foreach (var cam in GameObject.FindObjectsOfType<Camera>()) {
                Gizmos.DrawLine( cam.transform.ProjectPointInPlane( this.origin.transform.position ),
                                 cam.transform.ProjectPointInPlane( this.target.transform.position ) );
            }

            Gizmos.color = prevColor;
        }
    }
}
