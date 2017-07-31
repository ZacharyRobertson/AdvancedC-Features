using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Delegates
{
    public class Attack : MonoBehaviour
    {
        public int damage = 10;
        protected virtual void OnTriggerEnter(Collider other)
        {
            // If object has health
            Health health = other.GetComponent<Health>();
            if(health != null)
            {
                //Deal damage to the object

                health.TakeDamage(damage);
            }
        }
    }
}