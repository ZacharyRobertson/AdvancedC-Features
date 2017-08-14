using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        public float acceleration = 25f;
        public float hyperSpeed = 150f;
        public float decelration = 0.1f;
        public float rotationSpeed = 5f;

        public float inputV;
        public float inputH;

        private Rigidbody2D rigid;
        // Use this for initialization
        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            inputV = Input.GetAxisRaw("Vertical");
            inputH = Input.GetAxis("Horizontal");
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            Accelerate();
            Decelerate();
            Rotate();
        }

        void Accelerate()
        {
            
            Vector2 force = transform.right * inputV;
            if(Input.GetKey(KeyCode.LeftShift))
            {
                force *= hyperSpeed;
            }
            else
            {
                force *= acceleration;
            }

            rigid.AddForce(force);
        }
        void Decelerate()
        {
            rigid.velocity += -rigid.velocity * decelration;
        }
        void Rotate()
        {
            
            transform.rotation *= Quaternion.AngleAxis(rotationSpeed * inputH, Vector3.back);
        }
    }
}