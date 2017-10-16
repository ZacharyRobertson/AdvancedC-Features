using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    public class LookScript : NetworkBehaviour
    {
        //Sensitivity of the mouse
        public float mouseSensitivity = 2.0f;
        //Min & Max of the Y axis
        public float minY = -90f;
        public float maxY = 90f;

        //Yaw of the camera  (Rotation on Y)
        private float yaw = 0f;
        //Pitch of the camera (Rotation on X)
        private float pitch = 0f;
        //Main Camera reference
        private GameObject mainCamera;

        // Use this for initialization
        void Start()
        {
            //Lock the cursor to the centre of the screen
            Cursor.lockState = CursorLockMode.Locked;
            // Make the cursor invisible
            Cursor.visible = false;

            //Gets a reference to the camera inside this object
            Camera cam = GetComponentInChildren<Camera>();
            if (cam != null)
            {
                mainCamera = cam.gameObject;
            }
        }
        void OnDestroy()
        {
            //Release the cursor from locked state and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        void HandleInput()
        {
            //Set Yaw to yaw + MouseX * mouseSensitivity
            yaw = yaw + Input.GetAxis("Mouse X") * mouseSensitivity;
            //Set pitch to pitch + MouseY * mouseSensitivity
            pitch = pitch + Input.GetAxis("Mouse Y") * mouseSensitivity;
            // Clamp pitch between minY and maxY
            Mathf.Clamp(pitch, minY, maxY);
        }

        // Update is called once per frame
        void Update()
        {
            if (isLocalPlayer)
            {
                HandleInput();
            }
        }

        void LateUpdate()
        {
            //Check if this is the local player
            if (isLocalPlayer)
            {
                mainCamera.transform.localEulerAngles = new Vector3(-pitch, yaw, 0);
            }
        }
    }
}