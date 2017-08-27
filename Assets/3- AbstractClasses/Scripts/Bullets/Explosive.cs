using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbstractClasses
{
    public class Explosive : Bullet
    {
        public int explosionDamage = 10;
        public float explosionRadius = 5f;

        public Animator anim;

        // Use this for initialization
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
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

        void Explode()
        {
            //Play an animation of the bullet exploding
            if (anim != null)
            {
                anim.Play("Explosion");
            }
            //Destroy this gameObject
            Destroy(gameObject);

            //Damage enemies in range
            Collider[] objectsInRange = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider col in objectsInRange)
            {
                //Create a reference to an enemy script
                //Enemy enemy = col.GetComponent<Enemy>();

                //if(enemy != null)
                //{
                //  Deal Damage
                //}
            }
        }
        void OnCollisionEnter(Collider col)
        {
            if (col.tag == "Enemy")
            {
                Explode();
            }
        }
    }
}