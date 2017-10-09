using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    [RequireComponent(typeof(Player))]
    public class NetworkUser : NetworkBehaviour
    {
        public Camera cam;
        public AudioListener audioListener;

        private Player player;

        // Use this for initialization
        void Start()
        {
            player = GetComponent<Player>();
            if (!isLocalPlayer)
            {
                cam.enabled = false;
                audioListener.enabled = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isLocalPlayer)
            {
                float h = Input.GetAxis("Mouse X");
                h += Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                player.Move(v, h);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.Jump();
                }
            }
        }
    }
}