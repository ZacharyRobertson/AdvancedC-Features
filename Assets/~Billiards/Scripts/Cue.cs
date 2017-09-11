using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{
    public class Cue : MonoBehaviour
    {
        public Ball targetBall; //the ball selected (generally the Cue Ball)
        // The min and max power mapped to distance
        public float minPower = 0f;
        public float maxPower = 20f;
        public float maxDistance = 5f; // The maximum distance the cue can be dragged back 

        private float hitPower; //The calculated hit power to fire the ball
        private Vector3 aimDirection; //The aim direction the ball should fire
        private Vector3 prevMousePos; //The mouse position upon left click
        private Ray mouseRay; //The ray of the mouse

        void Update()
        {
            //Check if left mouse button is pressed
            if(Input.GetMouseButtonDown(0))
            {
                //Store the click position as the prevMousePos
                prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            //Check if the left mouse button is held down
            if(Input.GetMouseButton(0))
            {
                //Perform the Drag
                Drag();
            }
            else
            {
                Aim();
            }

            //Check if we lift the left Mouse button
            if(Input.GetMouseButtonUp(0))
            {
                Fire();
            }

        }

        //Visualise  the mouse ray and direction of fire
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(mouseRay.origin, mouseRay.origin + mouseRay.direction * 1000f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(targetBall.transform.position, targetBall.transform.position + aimDirection * hitPower);
        }

        //Rotate the cue to where the mouse is pointing
        void Aim()
        {
            //Calculate mouseRay 
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Create a variable for the hit information of the raycast
            RaycastHit hit;
            //Perform the raycast check
            if(Physics.Raycast(mouseRay, out hit))
            {
                // Obtain direction from the cue's position to the raycast point
                Vector3 dir = transform.position - hit.point;
                //Convert direction to an angle (in degrees)
                float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                //Rotate towards the angle
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                // Position the cue to the ball's position
                transform.position = targetBall.transform.position;
            }
        }

        //Deactivates the Cue
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        //Activate the cue
        public void Activate()
        {
            Aim();
            gameObject.SetActive(true);
        }

        //Allows you to drag the cue stick back and calculate the power dealt to the ball
        void Drag()
        {
            // Store the ball's position in a smaller variable
            Vector3 targetPos = targetBall.transform.position;
            //Get Mouse Pos in world Units
            Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Calculate the distance from previous mouse pos to current mouse pos
            float distance = Vector3.Distance(prevMousePos, currentMousePos);
            // Clamp the distance between 0 - maxDistance
            distance = Mathf.Clamp(distance, 0, maxDistance);
            // Calculate the distant as a percentage
            float distPercentage = distance / maxDistance;
            // use percentage of distance to map the min and maxPower variables
            hitPower = Mathf.Lerp(minPower, maxPower, distPercentage);
            //Position the cue back using distance
            transform.position = targetPos - transform.forward * distance;
            //Get direction to targetBall
            aimDirection = (targetPos - transform.position).normalized;
        }

        void Fire()
        {
            //Hit the ball with Direction and Power
            targetBall.Hit(aimDirection, hitPower);
            //Deactivate the cue when done
            Deactivate();
        }
    }
}