using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitWithZoom : MonoBehaviour
{
    public Transform target;// The traget point to rotate around
    public float distance = 5f;//The distancebetween the camera and target
    public float sensitivity = 1f;//How sensitive the  input is for rotating

    public float disMin = .5f;//Minimum distance of camera
    public float disMax = 15f;//Maximum distance of camera

    //Store X and Y rotation in euler angles
    float x;
    float y;

    // Use this for initialization
    void Start()
    {
        //Get the current rotation on X and Y
        Vector3 angles = transform.eulerAngles;
        //Swap over X and Y on axis
        x = angles.y;
        y = angles.x;
    }
    void HideCursor(bool isHiding)
    {
        //Is the cursor supposed to be hiding?
        if(isHiding)
        {
            //Hide the cursor
            Cursor.visible = false;
        }
        else
        {
            //Unhide the cursor
            Cursor.visible = true;
        }
    }
    void GetInput()
    {
        if (Cursor.visible == false)
        {
            // Gather X and Y mouse offset  input to rotate camera (by sensitivity)
            x += Input.GetAxis("Mouse X") * sensitivity;
            // Opposite direction for Y because it inverted
            y -= Input.GetAxis("Mouse Y") * sensitivity;
        }
        // Get Mouse ScrollWheel input offset for changing distance
        float inputScroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - inputScroll, disMin, disMax);

    }
    void Movement()
    {
        //Check if target has been set
        if(target)
        {
            // Convert x and y rotations to Quaternion using euler
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // Calculate new position offset  using rotation
            Vector3 negDistance = new Vector3(0, 0, -distance);
            Vector3 position = rotation * negDistance + target.position;

            //Apply rotation and position to transform
            transform.rotation = rotation;
            transform.position = position;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //If Right Mouse button is pressed
        if (Input.GetMouseButton(1))
        {
            //Hide the cursor
            HideCursor(true);
            
        }
        //ELSE
        else
        {
            HideCursor(false);
        }
        //Get Input 
        GetInput();
        //Call Movement
        Movement();
    }
}
