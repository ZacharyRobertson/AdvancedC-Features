using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StarFoxClone
{
    public class ArwingController : MonoBehaviour
    {
        public enum Mode
        {
            CONFINED = 0,
            ALL_RANGE = 1
        }
        public Mode arwingMode = Mode.CONFINED;

        [Header("Camera")]
        public float cameraYSpeed = 10f;
        public float cameraMoveSpeed = 90f;
        public float cameraDistance = 20f;

        [Header("Arwing")]
        public Transform moveTarget;
        public Transform aimTarget;
        public float aimingSpeed = 15f;
        public float movementSpeed = 40f;
        public float rotationSpeed = 10f;

        private Camera parentCam;
        private float startDistance = 5f;
        private Vector3 up = Vector3.up;

        // Use this for initialization
        void Start()
        {
            // Get the parentCam from Parent
            parentCam = GetComponentInParent<Camera>();
            // Set startDistance to distance between parentCam and Arwing
            startDistance = Vector3.Distance(parentCam.transform.position, transform.position);
        }
        void moveXToY(Transform x, Transform y, float speed)
        {
            Vector3 xPos = x.position;
            Vector3 yPos = y.position;

            // Move x towards y
            xPos = Vector3.MoveTowards(xPos, yPos, speed * Time.deltaTime);

            x.position = xPos;
        }

        // moves the arwing to the moveTarget
        void MoveArwingToMoveTarget()
        {
            // Move the arwing towards moveTarget
            moveXToY(transform, moveTarget, movementSpeed);

            // Reset z locally to startDistance
            Vector3 localArwingPos = transform.localPosition;
            localArwingPos.z = startDistance;
            transform.localPosition = localArwingPos;
        }
        //Moves the moveTarget to Arwing
        void MoveTargetToArwing()
        {
            // Get the aimTarget and arwing's local position
            Vector3 localAimPosition = aimTarget.transform.localPosition;
            Vector3 localArwingPosition = transform.localPosition;

            // move aim towards local arwing
            localAimPosition = Vector3.MoveTowards(localAimPosition, localArwingPosition, movementSpeed * Time.deltaTime);

            // Apply aim target's local position
            aimTarget.localPosition = localAimPosition;
            transform.localPosition = localArwingPosition;
        }
        //Rotates the arwing to AimTarget
        void RotateArwingToAimTarget()
        {
            // Get Direction to aimTarget from Arwing
            Vector3 dir = aimTarget.position - transform.position;
            // Get rotation to up
            Quaternion rotation = Quaternion.LookRotation(dir.normalized, up);
            // Lerp rotation for arwing
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, aimingSpeed * Time.deltaTime);
        }
        // Moves forward with the Camera
        void MoveForward()
        {
            parentCam.transform.position += parentCam.transform.forward * cameraMoveSpeed * Time.deltaTime;
        }
        //Moves the  camera to follow arwing (in ALL_RANGE Mode only)
        void FollowArwing()
        {
            // Get camera's position & rotation
            Vector3 position = parentCam.transform.position;
            Quaternion rotation = parentCam.transform.rotation;
            // Get Local position of target and arwing
            Vector3 localArwingPos = transform.localPosition;
            Vector3 localTargetPos = moveTarget.transform.localPosition;
            // Calculate direction with localPos
            Vector3 direction = localTargetPos - localArwingPos;
            // Rotate the camera to direction
            rotation *= Quaternion.AngleAxis(direction.x * rotationSpeed * Time.deltaTime, Vector3.up);
            // Move the camera up at different speed
            position.y += direction.y * cameraYSpeed * Time.deltaTime;
            // Apply newly made position to camera
            parentCam.transform.position = position;
            parentCam.transform.rotation = rotation;
        }
        //Move the moveTarget using INput Axis (on x and y)
        void MoveTarget(float inputH, float inputV)
        {
            // Get inputDir
            Vector3 inputDir = new Vector3(inputH, inputV, 0);
            // calculate force by inputDir x movementSpeed
            Vector3 force = inputDir * movementSpeed;
            //Offset aimTarget by force
            moveTarget.localPosition += force * Time.deltaTime;
        }
        // Public accesor for movement (call this to move arwing using input)
        public void Move(float inputH, float inputV)
        {
            // Move to target
            MoveTarget(inputH, inputV);
            // Move forward
            MoveForward();
            // move based on arwing Mode
            switch (arwingMode)
            {
                case Mode.CONFINED:
                    break;
                case Mode.ALL_RANGE:
                    FollowArwing();
                    break;
                default:
                    break;
            }
            // IF no input is made
            if (inputH == 0 && inputV == 0)
            {
                //Move Target to Arwing
                MoveTargetToArwing();
            }
            // Move the arwing to movetarget
            MoveArwingToMoveTarget();
            // Rotate the arwing to aimTarget
            RotateArwingToAimTarget();
        }
    }
}