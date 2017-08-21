using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbstractClasses
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Shooting : MonoBehaviour
    {
        public int weaponIndex = 0;

        private Weapon[] attachedWeapons;
        private Rigidbody2D rigid;

        // Use this for initialization
        void Awake()
        {
            attachedWeapons = GetComponentsInChildren<Weapon>();
            rigid = GetComponent<Rigidbody2D>();
        }
        void Start()
        {
            SwitchWeapon(weaponIndex);
        }
        void Update()
        {
            CheckFire();
            if(Input.GetKeyDown(KeyCode.Q))
            {
                CycleWeapon(-1);
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                CycleWeapon(1);
            }
        }
        void CheckFire()
        {
            // Set CurrentWepaon to attachedWeapon[weaponIndex]
            Weapon currentWeapon = attachedWeapons[weaponIndex];
            //if space is down
            if(Input.GetKeyDown(KeyCode.Space))
            {
                //Fire currentWeapon
                currentWeapon.Fire();
                //apply currentWeapon's recoil to player
                rigid.AddForce(-transform.right * currentWeapon.recoil, ForceMode2D.Impulse);
            }
        }
        void CycleWeapon(int amount)
        {
            // Set desiredindex to weaponIndex + amount
            int desiredIndex = weaponIndex + amount;
            // If desired index is >= weapons length
            if(desiredIndex >= attachedWeapons.Length)
            {
                // Set desiredindex to zero
                desiredIndex = 0;
            }
            //Else if desiredIndex < 0
            else if (desiredIndex <0)
            {
                //Set desired index to weapons length - 1
                desiredIndex = attachedWeapons.Length - 1;
            }
            //Set weaponIndex to desiredIndex
            weaponIndex = desiredIndex;
            //SwitchWeapon() and pass weaponIndex
            SwitchWeapon(weaponIndex);
        }
        Weapon SwitchWeapon(int weaponIndex)
        {
            // Check Bounds
            if (weaponIndex < 0 || weaponIndex > attachedWeapons.Length)
            {
                // Return null as Error
                return null;
            }
            // Loop through all attached weapons
            for (int i = 0; i < attachedWeapons.Length; i++)
            {
                //Set w to attachedWeapons[weaponIndex]
                Weapon w = attachedWeapons[i];
                //if i == weaponIndex
                if(i == weaponIndex)
                {
                    // Activate GameObject in w variable
                    w.gameObject.SetActive(true);
                }
                else
                {
                    // Deactivate gameObject in w variable
                    w.gameObject.SetActive(false);
                }
            }
            return attachedWeapons[weaponIndex];
        }
    }
}