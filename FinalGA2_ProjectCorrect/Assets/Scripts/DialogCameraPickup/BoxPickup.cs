using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPickup : MonoBehaviour
{
    public float rotationSpeed = 500.0f;

    bool isInTrigger = false;
    
    bool isPickedUp = false;

    //rotate the pickup based on the difference from last mouse position to this.
    Vector3 previousMousePos; 

    public Camera theCamera;

 

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && isPickedUp)
        {
            //DROP THE OBJ 

            transform.parent = null;

            //restore scale
            transform.localScale = Vector3.one;

            Vector3 fwd = Camera.main.transform.forward * 2.0f;
            
            //drop it in front of the camera
            transform.position += fwd;

        }
 
        if (Input.GetKeyDown(KeyCode.E) && isInTrigger && !isPickedUp)
        {
            Debug.Log("Collided with Player and Player pressed E Key");

            //TODO interpolate to position

            transform.position = Vector3.zero;
            transform.parent = theCamera.transform;

            Vector3 fwd = new Vector3(0, 0, 1);

            transform.localPosition = fwd;
            
            //make it smaller
            transform.localScale = Vector3.one / 4.0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            isPickedUp = true;

        }

        if(isPickedUp)
        {
            
            //Examine it TODO: improve!!!

            Vector3 dir = previousMousePos - Input.mousePosition;
            //XY inverted in world space
            float x = dir.x;
            float y = dir.y;
            //fix direction
            dir.x = -y;
            dir.y = x;
            dir.Normalize();
                        
            transform.Rotate(dir * Time.deltaTime * rotationSpeed);
            
        }

        //always keep track of the mouse pos this frame, to compare to next
        previousMousePos = Input.mousePosition;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player")
            return;

        isInTrigger = true;
       
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
    }

}
