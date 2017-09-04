using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recusrion
{
    public class RecursiveSpawner : MonoBehaviour
    {
        public GameObject spawnPrefab;
        public int amount = 10;
        public float posOffset = 1f;
        public float ScaleFactor = 0.7f;


        void RecursiveSpawn(int currentAmount, Vector3 position, Vector3 scale)
        {
            if(amount <= 0)
            {
                return;
            }
            //Decrement amount
            amount--;
            //Calculate the adjusted scale
            Vector3 adjustedScale = scale * (1 - ScaleFactor);
            //calculate the adjusted posiion
            Vector3 adjustedPos = position + Vector3.up * posOffset;
            //Create our prefab
            GameObject clone = Instantiate(spawnPrefab);
            //Set the clones positon and scale
            clone.transform.position = adjustedPos;
            clone.transform.localScale = adjustedScale;

            //Call Recursive spawn
            RecursiveSpawn(amount, adjustedPos, adjustedScale);
        }

        // Use this for initialization
        void Start()
        {
            Vector3 position = transform.position;
            Vector3 scale = spawnPrefab.transform.localScale;
            RecursiveSpawn(amount, position, scale); 
        }
    }
}