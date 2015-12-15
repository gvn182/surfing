using UnityEngine;
using System.Collections;

public class BigWave : MonoBehaviour {

    CharacterController Controller;
    StateController GameState;
    public float Speed = 5f;
	void Start () {
        GameState = (StateController)FindObjectOfType(typeof(StateController));
        Controller = (CharacterController)FindObjectOfType(typeof(CharacterController));
	}
	
	// Update is called once per frame
	void Update () {
        if (Controller.HasStarted)
        {
            float TheSpeed = Speed - Controller.Speed;
            this.transform.position = new Vector3(this.transform.position.x + (TheSpeed * Time.deltaTime), this.transform.position.y, this.transform.position.z);
        }
	}
    private void OnTriggerEnter2D(Collider2D c)
    {

        if (c.tag == "BigWaveTrigger" && !Controller.dieing && !Controller.inSky)
        {
            Controller.OnTube = true;
        }
        if (c.tag == "BigWaveTriggerOut" && !Controller.dieing && !Controller.inSky)
            Controller.OnTube = false;
    }
}
