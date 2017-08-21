using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AbstractClasses
{
    public class Explosive : Bullet
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Fire(Vector2 direction, float? speed = default(float?))
        {
            throw new NotImplementedException();
        }
    }
}