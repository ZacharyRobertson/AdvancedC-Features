using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Delegates
{
    public class EnemySpawner : MonoBehaviour
    {
        delegate void EnemyTypeFunc(int amount);
        private List<EnemyTypeFunc> enemyTypeFuncs = new List<EnemyTypeFunc>();

        public Transform target;
        public GameObject orcPrefab;
        public GameObject trollPrefab;
        public int minAmount = 0, maxAmount = 20;
        public float spawnRate = 1f;

        int randomAmount;
        int enemyType;
        // Use this for initialization
        void Start()
        {
            enemyTypeFuncs.Add(SpawnOrc);
            enemyTypeFuncs.Add(SpawnTroll);
            
        }

        // Update is called once per frame
        void Update()
        {
            //Set our random amount once per frame
            randomAmount = Random.Range(minAmount, maxAmount);
            enemyType = Random.Range(0, enemyTypeFuncs.Count);
            //Call the delegate function
            enemyTypeFuncs[enemyType](randomAmount);
        }

        void SpawnOrc(int amount)
        {
            GameObject orcClone = Instantiate(orcPrefab, transform.position, transform.rotation, this.transform);

        }
        void SpawnTroll(int amount)
        {
            GameObject trollClone = Instantiate(trollPrefab, transform.position, transform.rotation, this.transform);
        }
    }
}