using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // If the sphere collides with a gameobject of player tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destroy the sphere
            Destroy(gameObject);
        }
        // if(collision.gameObject.CompareTag("Enemy"))
        // {
        //     Destroy(gameObject);
        // }
       else if(collision.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
        
    }
}