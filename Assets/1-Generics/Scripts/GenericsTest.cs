using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics
{
    public class GenericsTest : MonoBehaviour
    {
        public GameObject prefab;
        public int spawnAmount = 20;
        public float spawnRadius = 10f;
        public CustomList<GameObject> gameObjects = new CustomList<GameObject>();
        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                GameObject clone = Instantiate(prefab);
                Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
                clone.transform.position = randomPos;
                gameObjects.Add(clone);
                
            }
            Debug.Log(gameObjects.amount);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                gameObjects.Clear();

                Debug.Log(gameObjects.amount);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
                Debug.Log(gameObjects.amount);
            }
        }
    }
}
