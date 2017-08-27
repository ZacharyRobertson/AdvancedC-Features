using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbstractClasses
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public float aliveDistance = 5f;

        public Rigidbody2D rigid;
        public Vector3 shotPos;
        
         
        // Use this for initialization
        protected virtual void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        protected virtual void Start()
        {
            // Record our starting position from the first frame
            shotPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {

        }
        public abstract void Fire(Vector2 direction, float? speed = null);
    }
}