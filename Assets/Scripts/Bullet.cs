

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{

   public KeyCode timeStopKey;
    public float slowdownTime ;
    private Rigidbody rb;

    

    private bool stopped;

    private Vector3 initialSpeed;

    private Vector3 lowerSpeed;

    private Vector3 currentVelocity;

    private bool first=false;



    void Start()
    {
        rb=GetComponent<Rigidbody>();
        initialSpeed=rb.velocity;
        currentVelocity=initialSpeed;
        
        

    }

    public void StartSlowDown(float slowdownTime)
    {
        StartCoroutine(SlowDownCoroutine(slowdownTime));
    }


    private IEnumerator SlowDownCoroutine(float slowdownTime)
    {
        float halfTime = slowdownTime / 2f;

        float currentTime = 0f;
        while (currentTime < halfTime)
        {
            currentTime += Time.deltaTime;

            float smoothFactor = Mathf.SmoothStep(0.25f, 1f, currentTime/halfTime);

            Vector3 currentSpeed = Vector3.Lerp(initialSpeed, lowerSpeed, smoothFactor);



            rb.velocity = currentSpeed;


            yield return null;

            
        }

        currentTime=0;

        while (currentTime <halfTime)
        {
            currentTime += Time.deltaTime;

            float smoothFactor = Mathf.SmoothStep(0.4f, 1f, currentTime/halfTime);

            Vector3 currentSpeed = Vector3.Lerp(lowerSpeed, initialSpeed, smoothFactor);

            rb.velocity = currentSpeed;

            yield return null;

            
        }
        
        
       }

    void Update()
    {
        if(initialSpeed==Vector3.zero)
        {
            initialSpeed=rb.velocity;
            lowerSpeed=initialSpeed/10;
        }
        
        if (Input.GetKeyDown(timeStopKey))
        {
            Debug.Log("Time Stop");

           StartCoroutine(SlowDownCoroutine(slowdownTime));
        }
       
    }

    
}
