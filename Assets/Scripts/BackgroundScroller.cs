using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour {

	// Use this for initialization
    CharacterController Controller;
    StateController GameState;
    public float speed;
    private Vector2 DefaultPOS;
    public float horizontalDirection = -1; //move from right to left
    public bool SpecificX;
    public bool SpecificY;
    public float BackX;
    public float BackY;
	void Start () {

        GameState = (StateController)FindObjectOfType(typeof(StateController));
        Controller = (CharacterController)FindObjectOfType(typeof(CharacterController));

        DefaultPOS = this.transform.position;
        if (SpecificX)
            DefaultPOS.x = BackX;
        if (SpecificY)
            DefaultPOS.y = BackY;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameState.isGamePaused)
            return;

        transform.Translate(new Vector2((speed * horizontalDirection * Time.deltaTime) - (Controller.Speed * 0.0005f), 0));

	}
    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Edge")
        this.transform.position = DefaultPOS;
        
    }
}
