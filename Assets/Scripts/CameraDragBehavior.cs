using UnityEngine;
using System.Collections;

public class CameraDragBehavior : MonoBehaviour
{

    public float dragSpeed;
    private Vector3 dragOrigin;
    private Vector3 cameraStartPosition;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            this.dragOrigin = Input.mousePosition;
            this.cameraStartPosition = this.gameObject.transform.position;
            return;
        }
        
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        Vector3 dragDelta = Input.mousePosition - this.dragOrigin;
        dragDelta.y = 0;
        this.gameObject.transform.position = this.cameraStartPosition - dragDelta;
    }
}
