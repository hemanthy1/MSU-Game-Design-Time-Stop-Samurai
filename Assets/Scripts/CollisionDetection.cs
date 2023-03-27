using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public SwordScript ss;


    private void Update()
    {
        GameObject[] bullets=GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject bullet in bullets)
        {
            float distance = Vector3.Distance(bullet.transform.position, transform.position);
            if (distance < 1.8f && ss.isAttacking)
            {
                Debug.Log("PERFECT TIME");
                Destroy(bullet);
            }
            else if (distance < 2.2f && ss.isAttacking)
            {
                Debug.Log("hit but not perfect");
                Destroy(bullet);
            }
        }
      
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.tag == "Bullet" && ss.isAttacking)
    //     {
    //         Debug.Log(other.name);
    //         Destroy(other.gameObject);
    //     }
    // }
}
