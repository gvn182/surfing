using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
    public GameObject Player;
    public CharacterController PlayerScript;
    float oldSize;
    Vector3 oldPosition;
    public Camera TheCamera;
	void Start () {
        PlayerScript = (CharacterController)FindObjectOfType(typeof(CharacterController));
        oldSize = this.camera.orthographicSize;
        oldPosition = this.camera.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(Player.transform.position.y > 2)
        this.transform.position = new Vector3(0, Player.transform.position.y, this.transform.position.z);
        else
            this.transform.position = new Vector3(0,0, this.transform.position.z);

      
            
	}
}
