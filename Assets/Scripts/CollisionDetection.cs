using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public SwordScript ss;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet" && ss.isAttacking)
        {
            Debug.Log(other.name);
            Destroy(other.gameObject);
        }
    }
}
