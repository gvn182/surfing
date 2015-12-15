using UnityEngine;

using System.Collections;



public class SmoothFollow2D : MonoBehaviour
{
    public CharacterController PlayerScript;
    public Transform target;
    float smoothTime = 1f;
    private Transform thisTransform;
    private Vector2 velocity;
    float yOffset = 0f;
    public bool useSmoothing = true;
    public bool enabled;
    float oldSize;
    Vector3 oldPosition;

    void Start()
    {
        PlayerScript = (CharacterController)FindObjectOfType(typeof(CharacterController));
        thisTransform = transform;
        velocity = new Vector2(0.5f, 0.5f);
        oldSize = this.camera.orthographicSize;
        oldPosition = this.camera.transform.position;

    }



    void Update()
    {
        if (!enabled)
            return;

        Vector2 newPos2D = Vector2.zero;
        if (useSmoothing)
        {
            newPos2D.x = Mathf.SmoothDamp(thisTransform.position.x, target.position.x, ref velocity.x, smoothTime);
            newPos2D.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y + yOffset, ref velocity.y, smoothTime);

        }
        else
        {

            newPos2D.y = target.position.y + yOffset;
        }
      


        if (PlayerScript.OnTube)
        {
            this.camera.orthographicSize = 3;
            this.camera.transform.position = new Vector3(-1.5f, -1.4f, this.camera.transform.position.z);
        }
        else
        {

            this.camera.orthographicSize = oldSize;
            this.camera.transform.position = oldPosition;
        }
        Vector3 newPos = new Vector3(newPos2D.x, newPos2D.y, transform.position.z);
        if (newPos.y > 0)
            transform.position = Vector3.Slerp(transform.position, newPos, Time.time);

    }

}