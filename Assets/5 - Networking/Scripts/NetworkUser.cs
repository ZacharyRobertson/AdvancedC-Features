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
        [Range(0,1)]
        public float lerpRate = 1f;

        [SyncVar] Vector3 syncPosition;
        [SyncVar] Quaternion syncRotation;

        private Player player;

        void Start()
        {
            player = GetComponent<Player>();
            if (!isLocalPlayer)
            {
                cam.enabled = false;
                audioListener.enabled = false;
            }
        }
        void FixedUpdate()
        {
            if(isLocalPlayer)
            {
                //Send New Position to Sever
                Rigidbody rigid = player.rigid;
                Cmd_SendPositionToServer(rigid.position);
                Cmd_SendRotationToServer(rigid.rotation);
            }
        }
        void Update()
        {
            if (isLocalPlayer)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                player.Move(v, h);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.Jump();
                } 
            }
            else
            {
                LerpPosition();
                lerpRotation();
            }
        }
        [Command]
        void Cmd_SendPositionToServer(Vector3 position)
        {
            syncPosition = position;
        }
        [Command]
        void Cmd_SendRotationToServer(Quaternion rotation)
        {
            syncRotation = rotation;
        }
        void LerpPosition()
        {
            Rigidbody rigid = player.rigid;
            rigid.position = Vector3.Lerp(rigid.position, syncPosition, lerpRate);   
        }
        void lerpRotation()
        {
            Rigidbody rigid = player.rigid;
            rigid.rotation = Quaternion.Lerp(rigid.rotation, syncRotation, lerpRate);
        }
    }
}