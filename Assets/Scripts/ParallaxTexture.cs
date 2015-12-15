using UnityEngine;
using System.Collections;

public class ParallaxTexture : MonoBehaviour {

    StateController GameState;
    CharacterController Controller;
    public float FRAMERATE;
    private float PASSEDFRAME;
    public float scrollSpeed;
    float offset;
    float rotate;
    public Texture[] MyTex;
    private int Index = 0;
    public float SpeedPercent;
	void Start () {
        Controller = (CharacterController)FindObjectOfType(typeof(CharacterController));
        GameState = (StateController)FindObjectOfType(typeof(StateController));
	}
	
	// Update is called once per frame
	void Update () {
        if (GameState.isGamePaused)
            return;

        offset += (Time.deltaTime * scrollSpeed) / 10.0f * (Controller.Speed * SpeedPercent);
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

        PASSEDFRAME += Time.deltaTime;

        if (PASSEDFRAME >= FRAMERATE)
         {
             if (Index < MyTex.Length -1)
                 Index++;
             else
                 Index = 0;

             PASSEDFRAME = 0;
         }
        this.renderer.material.SetTexture("_MainTex", MyTex[Index]);
	}
}
