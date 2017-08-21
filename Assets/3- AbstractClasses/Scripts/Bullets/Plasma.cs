using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Plasma : Bullet
    {
        private Rigidbody2D rigid;
        // Use this for initialization
        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Fire(Vector2 direction, float? speed = null)
        {
            float currentSpeed = this.speed;
            // Check if speed has been used
            if (speed != null)
            {
                currentSpeed = speed.Value;
            }
            // Fire in that direction
            rigid.AddForce(direction * currentSpeed, ForceMode2D.Impulse);
        }
    }
}