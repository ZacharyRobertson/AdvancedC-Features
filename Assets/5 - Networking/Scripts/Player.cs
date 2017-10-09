using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 8f;
        public float rotationSpeed = 360f;
        public float jumpHeight = 10.0f;

        private bool isGrounded = false;
        private Rigidbody rigid;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }
        public void Move(float vertical, float horizontal)
        {
            Vector3 position = rigid.position;
            Quaternion rotation = rigid.rotation;

            position += transform.forward * vertical * moveSpeed * Time.deltaTime;
            rotation *= Quaternion.AngleAxis(rotationSpeed * horizontal * Time.deltaTime, Vector3.up);

            rigid.MovePosition(position);
            rigid.MoveRotation(rotation);
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