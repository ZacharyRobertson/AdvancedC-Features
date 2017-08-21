using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    public class MuzzleFlash : MonoBehaviour
    {
        public int frameDelay = 1;

        private int frameCount = 0;

        // Update is called once per frame
        void Update()
        {
            if(frameCount > frameDelay)
            {
                Destroy(gameObject);
            }
        }
        void LateUpdate()
        {
            frameCount++;
        }
    }
}