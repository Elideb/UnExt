using UnityEngine;
using System.Collections.Generic;
using UnExt;

public class TestTextureExtensions : MonoBehaviour {

    public Sprite sprite;
    private Dictionary<int, Color> colors = new Dictionary<int, Color>() {
        { 0, Color.black },
        { 1, Color.blue },
        { 2, Color.cyan },
        { 3, Color.gray },
        { 4, Color.green },
        { 5, Color.grey },
        { 6, Color.magenta },
        { 7, Color.red },
        { 8, Color.white },
        { 9, Color.yellow }
    };

    public Sprite alphaSprite;

    // Use this for initialization
    void Start() {
        var go = new GameObject( "Original cube", typeof( SpriteRenderer ) );
        var spRenderer = go.GetComponent<SpriteRenderer>();
        spRenderer.sprite = this.sprite;

        go = new GameObject( "Alpha cube", typeof( SpriteRenderer ) );
        go.transform.position = new Vector3( 0, 2, 0 );
        spRenderer = go.GetComponent<SpriteRenderer>();
        spRenderer.sprite = this.alphaSprite;
    }

    int alphaPainted = 1;

    void OnGUI() {

        if (GUI.Button( new Rect( 10, 10, 100, 15 ), "Outlined" )) {
            for (int i = 0; i < 20; ++i) {
                var go = new GameObject( string.Format( "cube {0:00}", i ) );
                go.transform.position = new Vector3( 2 * i, 0, 0 );
                var spRenderen = go.AddComponent<SpriteRenderer>();

                var sourceTex = this.sprite.texture;
                int colorIdx = i % this.colors.Count;
                Color color = this.colors[colorIdx];
                var newSprite = Sprite.Create( sourceTex.Outlined( color, (uint)i ),
                                               new Rect( 0, 0, sourceTex.width, sourceTex.height ),
                                               new Vector2( .5f, .5f ) );
                newSprite.name = string.Format( "{0} {1:00}", this.sprite.name, i );
                spRenderen.sprite = newSprite;
            }
        }

        if (GUI.Button( new Rect( 10, 30, 100, 15 ), "Alpha" )) {
            var sourceTex = this.alphaSprite.texture;
            var newTexture = sourceTex.CreateCopy();
            float cutoff = Random.Range( .05f, .8f );
            newTexture.ClearAlpha( cutoff );
            var newSprite = Sprite.Create( newTexture,
                                           new Rect( 0, 0, sourceTex.width, sourceTex.height ),
                                           new Vector2( .5f, .5f ) );
            newSprite.name = string.Format( "{0} cutoff {1:.00}", sourceTex.name, cutoff );

            var go = new GameObject( string.Format( "{0} cutoff {1:.00}", sourceTex.name, cutoff ) );
            go.transform.position = new Vector3( this.alphaPainted * 2, 2, 0 );
            var spRenderer = go.AddComponent<SpriteRenderer>();
            spRenderer.sprite = newSprite;
            ++this.alphaPainted;
        }
    }
}
