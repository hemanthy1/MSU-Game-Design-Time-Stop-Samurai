using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 originalVelocity;

    private Rigidbody rb;

    TimeStop timeStopScript ;

    private bool stopped;


    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopped)
            originalVelocity=rb.velocity;
        
        timeStopScript = FindObjectOfType<TimeStop>();
        stopped=timeStopScript.timeStopped;
        if (stopped)
        {
            Debug.Log("stopped");
            
            rb.velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("not stopped");
            rb.velocity = originalVelocity;
        }
    }
}
