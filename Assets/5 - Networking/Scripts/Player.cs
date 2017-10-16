using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    public class Player : NetworkBehaviour
    {
        public float moveSpeed = 8f;
        public float rotationSpeed = 360f;
        public float jumpHeight = 10.0f;
        public string remoteLayerName = "RemotePlayer";

        private bool isGrounded = false;
        [HideInInspector] public Rigidbody rigid;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();

            //Get Audio Listener from Camera
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            //Get the Camera
            Camera cam = GetComponentInChildren<Camera>();

            //If the current instance is the local player
            if(isLocalPlayer)
            {
                //Enable everything
                cam.enabled = true;
                audioListener.enabled = true;
            }
            else
            {
                //Disable everything
                cam.enabled = false;
                audioListener.enabled = false;

                //Assign Remote Layer
                AssignRemoteLayer();
            }

            //Register player on the network
            Registerplayer();
        }
        public void Move(float vertical, float horizontal)
        {
            Vector3 position = rigid.position;

            position += transform.forward * vertical * moveSpeed * Time.deltaTime;
            position += transform.right * horizontal * moveSpeed * Time.deltaTime;

            rigid.MovePosition(position);
        }
        // Register the player's id on the network
        void Registerplayer()
        {
            //Get the id from the network identity component
            string ID = "Player " + GetComponent<NetworkIdentity>().netId;
            this.name = ID; // assign new ID to name
        }
        //Assign remote layer to current GameObject (if it is not a local player)
        void AssignRemoteLayer()
        {
            gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
        }
        public void Jump()
        {
            if(isGrounded)
            {
                rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                isGrounded = false;
            }
        }
        void OnCollisionEnter(Collision other)
        {
            isGrounded = true;
        }
    }
}