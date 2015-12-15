using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour {

	// Use this for initialization
    double frameCount = 0;
    double dt = 0.0f;
    public double fps = 0.0f;
    float updateRate = 4.0f;  // 4 updates per sec.
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0 / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1.0 / updateRate;
        }
	}
}
