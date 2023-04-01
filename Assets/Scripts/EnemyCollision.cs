using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public StaminaController script;

    void OnCollisionEnter(Collision collision)
    {

        // If the sphere collides with a gameobject of player tag
        if (collision.gameObject.CompareTag("Player"))
        {
            script.staminaMeter.value -=10;
            // Destroy the sphere
            Destroy(gameObject);
        }
    

    }
}
