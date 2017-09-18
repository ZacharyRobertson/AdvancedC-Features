using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfWithManny
{
    public class Player : MonoBehaviour
    {
        public float speed = 50f;
        public float maxVelocity = 20f;

        private Rigidbody rigid;

        // Use this for initialization
        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Vector3 vel = rigid.velocity;
            if(vel.magnitude > maxVelocity)
            {
                vel = vel.normalized * maxVelocity;
            }
            rigid.velocity = vel;
        }

        public void Move(Vector3 direction)
        {
            rigid.AddForce(direction * speed, ForceMode.Impulse);
        }

        
    }
}