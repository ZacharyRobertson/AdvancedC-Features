using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GolfWithManny
{
    [RequireComponent(typeof(Player))]
    public class Controller : MonoBehaviour
    {
        public Camera cam;
        private Player p;

        // Use this for initialization
        void Start()
        {
            p = GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                Vector3 forward = cam.transform.forward;
                p.Move(forward);
            }
        }
    }
}