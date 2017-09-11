using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{
    public class Ball : MonoBehaviour
    {
        public float stopSpeed = 0.2f;

        private Rigidbody rigid;
        
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        
        void FixedUpdate()
        {
            Vector3 vel = rigid.velocity;

            //check if velocity on the y axis is going up
            if(vel.y > 0)
            {
                // Cap it at zero so the ball does not fly off the table
                vel.y = 0;
            }

            //If our speed is lower than the stopSpeed
            if(vel.magnitude < stopSpeed)
            {
                //Reduce velocity to zero
                vel = Vector3.zero;
            }
            rigid.velocity = vel;
        }

        //Create a public function that will move the ball in the direction it is hit
        public void Hit(Vector3 dir, float impactForce)
        {
            rigid.AddForce(dir * impactForce, ForceMode.Impulse);
        }
    }
}