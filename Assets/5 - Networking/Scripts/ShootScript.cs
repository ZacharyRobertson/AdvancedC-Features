using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    public class ShootScript : NetworkBehaviour
    {
        //Amount of bullets fired p/ second
        public float fireRate = 1f;
        //Range that the bullet can travel
        public float range = 100f;
        //LayerMask on which layer to hit
        public LayerMask mask;

        //Timer for the fire rate
        private float fireFactor = 0f;
        // Reference to the camera child
        private GameObject mainCamera;

        // Use this for initialization
        void Start()
        {
            mainCamera = GetComponentInChildren<Camera>().gameObject;
        }
        [Command]
        void Cmd_PlayerShot(string id)
        {
            Debug.Log("Player: " + id + " Has been shot");
        }
        [Client]
        void Shoot()
        {
            Ray ray = new Ray(transform.position, Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range, mask))
            {
                if(hit.collider.tag == "Player")
                {
                    Cmd_PlayerShot(hit.collider.name);
                }
            }
        }
        void HandleInput()
        {
            //SET fireFactor to fireFactor + Time.deltaTime
            fireFactor = fireFactor + Time.deltaTime;
            //SET fireInterval to 1/fireRate
            float fireInterval = 1 / fireRate;
            if(fireFactor >= fireInterval )
            {
                //If left Mouse Button is pressed
                if(Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            if(isLocalPlayer)
            {
                HandleInput();
            }
        }
    }
}