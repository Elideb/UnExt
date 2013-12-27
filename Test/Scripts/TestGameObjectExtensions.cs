using UnityEngine;
using UnExt;

public class TestGameObjectExtensions : MonoBehaviour {

    private static bool firstTime = true;

    public Transform transformPrefab;
    public GameObject gameobjectPrefab;
    public TestGameObjectExtensions scriptPrefab;
    public SpriteRenderer componentPrefab;

    void Start() {
        if (TestGameObjectExtensions.firstTime) {
            TestGameObjectExtensions.firstTime = false;

            var trans = this.Instantiate<Transform>( transformPrefab );
            trans.name = "Instantiate<Transform> " + trans.GetType();
            trans = this.Instantiate<Transform>( transformPrefab, new Vector3( 2, 3, 2 ), Quaternion.identity );
            trans.name = "Instatiate<Transform> " + trans.GetType() + " (translated)";

            var go = this.Instantiate<GameObject>( gameobjectPrefab );
            go.name = "Instantiate<GameObject> " + go.GetType();
            go = this.Instantiate<GameObject>( gameobjectPrefab, new Vector3( 2, 5, 4 ), Quaternion.identity );
            go.name = "Instantiate<GameObject> " + go.GetType() + " (translated)";

            var sc = this.Instantiate<TestGameObjectExtensions>( scriptPrefab );
            sc.name = "Instantiate<TestGameObjectExtensions> " + sc.GetType();

            var sp = this.Instantiate<SpriteRenderer>( componentPrefab );
            sp.name = "Instantiate<SpriteRenderer> " + sp.GetType();

            var co = this.Instantiate<Component>( componentPrefab );
            co.name = "Instantiate<Component> " + co.GetType();
        }
    }

}
