using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletShooter : MonoBehaviour
{
    
    public KeyCode key;
    public float slowdownTime;

    // The sphere prefab to instantiate
    public GameObject bulletPrefab;

    // The speed of the sphere
    public float bulletSpeed = 10f;

    // The time interval between each shot
    public float shootInterval = 1f;

    // The timer for shooting
    private float shootTimer = 0f;

    public GameObject aimTarget;

    public bool stopped;
    Bullet bulletScript;




    

    public void DelayTime(float slowdownTime)
    {
        StartCoroutine(DelayCoroutine(slowdownTime));
    }
    private IEnumerator DelayCoroutine(float slowdownTime)
    {     
        yield return new WaitForSeconds(slowdownTime);
    }


    void Update()
    {
       
       
        if (Input.GetKeyDown(key))
        {
           stopped=!stopped;
           if(stopped)
           shootInterval*=3;
           else
              shootInterval=shootInterval/3;

        }

        if(!stopped)
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
                Destroy(bullet, 20f);
            }
        }
        else
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
                    bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed/5f, ForceMode.Impulse);

                    bullet.GetComponent<Bullet>().stopped=true;
                }
                Destroy(bullet, 20f);
            }
        }
       
        
        
        
    }
}