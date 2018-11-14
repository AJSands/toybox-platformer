using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public Transform firePoint; // The origin point for this weapon's projectiles (i.e. the gun nozzle).
    public GameObject bulletPrefab; // The type of projectile launched from this weapon.

    public bool auto = true; // True: Weapon shoots as long as trigger is held. False: Weapon shoots once per trigger pull.
    public float fireRate = 0.075f; // Delay between shots, in seconds.
    //public int magazineCapacity = 50;
    //public float reloadTime = 2f;
    //public int ammo = 500;
    public int pelletCount = 1; // Number of projectiles fired at once.
    public float sprayRange = 4f; // Size of arc bullets may randomly fire in, in degrees.

    private float lastShotTime; // Time last shot was fired, for calculating delay before next shot.

    void Update()
    {
        if (auto)
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        //TODO: Charge beam? Might need to go in its own class.
    }

    void Shoot()
    {
        if (Time.time > lastShotTime + fireRate)
        {
            for (int i = 0; i < pelletCount; i++)
            {
                float spread = Random.Range(-sprayRange / 2, sprayRange / 2);
                Quaternion bulletRotation = firePoint.rotation * Quaternion.Euler(0, 0, spread);

                //TODO: Object pooling.
                Instantiate(bulletPrefab, firePoint.position, bulletRotation);
                lastShotTime = Time.time;
            }
        }
    }
}
