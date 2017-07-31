using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Delegates
{
    public abstract class Enemy : MonoBehaviour
    {
        public enum Behaviour
        {
            IDLE = 0,
            SEEK = 1
        }

        delegate void BehaviourFunc();

        public Transform target;
        public Behaviour behaviourIndex = Behaviour.SEEK;

        private List<BehaviourFunc> behaviourFuncs = new List<BehaviourFunc>();
        private NavMeshAgent agent;
        // Use this for initialization
        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            // assign delegates to list here
            behaviourFuncs.Add(Idle);
            behaviourFuncs.Add(Seek);
            //target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        void Idle()
        {
            // Stop the agent
            agent.Stop();
        }
        void Seek()
        {
            // Make the agent resume moving
            agent.Resume();
            // If target is not null
            if (target != null)
            {
                // Move the agent to the target
                agent.SetDestination(target.position);
            }
        }
        public void SetTarget(Transform target)
        {
            this.target = target;
        }
        public bool IsCloseToTarget(float distance)
        {
            if (target != null)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (distToTarget <= distance)
                {
                    return true;
                }
            }
            return false;
        }
        
        // Update is called once per frame
        protected virtual void Update()
        {
            // Call the correct delegate function
            behaviourFuncs[(int)behaviourIndex]();
        }
    }
}