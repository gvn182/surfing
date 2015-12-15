using UnityEngine;
using System.Collections;

public class CoinScroller : MonoBehaviour {

	// Use this for initialization
    CharacterController Controller;
    StateController GameState;
    public float speed;
    public float horizontalDirection = -1; //move from right to left

	void Start () {

        GameState = (StateController)FindObjectOfType(typeof(StateController));
        Controller = (CharacterController)FindObjectOfType(typeof(CharacterController));

        
	}
	
	// Update is called once per frame
	void Update () {
        if (GameState.isGamePaused)
            return;

        if (!Controller.HasStarted)
            return;

        transform.Translate(new Vector2((speed * horizontalDirection * Time.deltaTime) - (Controller.Speed * 0.0005f), 0));

	}
}
