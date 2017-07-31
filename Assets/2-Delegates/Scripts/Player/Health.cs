using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Delegates
{
    public class Health : MonoBehaviour
    {
        public int health = 100;
        // Use this for initialization
        void Start()
        {

        }
        void Update()
        {
            if (health <= 0)
            {
                // Destroy the Game Object
                Destroy(gameObject);
            }
        }
       public void TakeDamage(int damage)
        {
            // Reduce our health by the amount of damage
            health -= damage;
        }
    }
}