using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    // The sphere prefab to instantiate
    public GameObject bulletPrefab;

    // The speed of the sphere
    public float bulletSpeed = 10f;

    // The time interval between each shot
    public float shootInterval = 1f;

    // The timer for shooting
    private float shootTimer = 0f;

    public GameObject aimTarget;

    void Update()
    {
        // Update the timer
        shootTimer += Time.deltaTime;

        // If the timer reaches the interval, shoot a sphere
        if (shootTimer >= shootInterval)
        {
            // Reset the timer
            shootTimer = 0f;

            // Instantiate a sphere at the position and rotation of this gameobject
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            // If the player exists, calculate the direction to it from this gameobject
            if (aimTarget != null)
            {
                Vector3 direction = (aimTarget.transform.position - transform.position).normalized;
                
                // Add a force to the sphere in that direction with the speed factor
                bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
            }
            Destroy(bullet, 6f);
        }
    }
}