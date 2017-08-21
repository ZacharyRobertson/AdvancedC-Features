using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    public abstract class Weapon : MonoBehaviour
    {
        public int damage = 10;
        public int ammo = 0;
        public int maxAmmo = 30;
        public float fireInterval = .2f;
        public float recoil = 5f;
        public GameObject muzzleFlash;
        public GameObject bulletPrefab;

        public abstract void Fire();
        public virtual void Reload()
        {
            ammo = maxAmmo;
        }
        public Bullet SpawnBullet(Vector3 pos, Quaternion rot)
        {
            // Instantiate a new bullet
            GameObject clone = Instantiate(bulletPrefab, pos, rot);
            Bullet b = clone.GetComponent<Bullet>();
            // Play sound

            // Play MuzzleFlash
            Instantiate(muzzleFlash, pos, rot);
            // Reduce current ammo by 1
            ammo --;
            // return the new bullet 
            return b;
        }
    }
}
