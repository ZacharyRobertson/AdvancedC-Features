using UnityEngine;
using System.Collections;

namespace Delegates
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float distance = 3.0f;
        public float height = 3.0f;
        public float damping = 5.0f;
        public bool smoothRotation = true;
        public bool followBehind = true;
        public float rotationDamping = 10.0f;

        void Update()
        {
           if(target != null)
            {
                Follow();
            }
        }
        void Follow()
        {
            Vector3 wantedPosition;
            if (followBehind)
                wantedPosition = (target.position - target.transform.position) + new Vector3(0,height, -distance);
            else
                wantedPosition = (target.position - target.transform.position) + new Vector3(0, height, -distance);

            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

            if (smoothRotation)
            {
                Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
            }
            else transform.LookAt(target, Vector3.up);
        }
    }
}