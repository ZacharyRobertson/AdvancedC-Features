using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace StarFoxClone
{
    public class NetworkUser : NetworkBehaviour
    {
        public ArwingController arwingController;

        private Camera cam;
        private AudioListener audi;
        // Use this for initialization
        void Start()
        {
            cam = GetComponent<Camera>();
            audi = GetComponent<AudioListener>();
            if(!isLocalPlayer)
            {
                cam.enabled = false;
                audi.enabled = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(isLocalPlayer)
            {
                float inputH = Input.GetAxis("Horizontal");
                float inputV = Input.GetAxis("Vertical");

                arwingController.Move(inputH, inputV);
            }
        }
    }
}