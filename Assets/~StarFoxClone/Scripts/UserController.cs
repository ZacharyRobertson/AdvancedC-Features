using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StarFoxClone
{
    public class UserController : MonoBehaviour
    {
        public ArwingController arwingController;

        // Update is called once per frame
        void Update()
        {
            // GET inputH and inputV
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");

            arwingController.Move(inputH, inputV);
        }
    }
}