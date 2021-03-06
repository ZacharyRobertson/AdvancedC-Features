﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Delegates
{
    public class Orc : Enemy
    {
        public float attackRange = 2f;
        public float meleeSpeed = 20f;
        public float meleeDelay = 0.2f;
        public GameObject attackBox;
        private bool isAttacking = false;

        public EnemySpawner spawner;
        // Use this for initialization
        void Start()
        {
            spawner = GetComponentInParent<EnemySpawner>();
            target = spawner.target;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            //If target is within attackRange and we are not attacking
            if (IsCloseToTarget(attackRange) && !isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
        IEnumerator Attack()
        {
            //During
            isAttacking = true;
            attackBox.SetActive(true);
            behaviourIndex = Behaviour.IDLE;
            yield return new WaitForSeconds(meleeDelay);
            //After
            behaviourIndex = Behaviour.SEEK;
            attackBox.SetActive(false);
            isAttacking = false;

        }
    }
}