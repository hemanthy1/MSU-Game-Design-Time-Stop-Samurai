

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{

   public KeyCode timeStopKey;
    public float slowdownTime ;
    private Rigidbody rb;

    TimeStop timeStopScript ;

    private bool stopped;

     // The initial speed of the object
    private Vector3 initialSpeed;

    // The lower speed to slow down to
    private Vector3 lowerSpeed=Vector3.zero;

    private Vector3 currentVelocity;

    private bool first=false;



    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        initialSpeed=rb.velocity;
        currentVelocity=initialSpeed;
        
        

    }

    // A method to start the coroutine with a given slowdown time
    public void StartSlowDown(float slowdownTime)
    {
        StartCoroutine(SlowDownCoroutine(slowdownTime));
    }


    // A coroutine that slows down and speeds up the object smoothly
    private IEnumerator SlowDownCoroutine(float slowdownTime)
    {
        // Calculate the half time
        float halfTime = slowdownTime / 2f;

        // Store the current time
        float currentTime = 0f;
        // Loop until half time is reached
        while (currentTime < halfTime)
        {
            Debug.Log("slowing down");
            // Increment the current time by delta time
            currentTime += Time.deltaTime;

            // Calculate a smooth factor between 0 and 1 using SmoothStep
            float smoothFactor = Mathf.SmoothStep(0f, 1f, currentTime/halfTime);

            // Interpolate between the initial speed and the lower speed using smooth factor
            Vector3 currentSpeed = Vector3.Lerp(initialSpeed, lowerSpeed, smoothFactor);

            // Set the velocity of the object using current speed


            rb.velocity = currentSpeed;

            // Yield until next frame 
            yield return null;

            
        }

        currentTime=0;

        // Loop until half time is reached again
        while (currentTime <halfTime)
        {
            // Increment the current time by delta time
            currentTime += Time.deltaTime;

            // Calculate a smooth factor between 0 and 1 using SmoothStep (in reverse order)
            float smoothFactor = Mathf.SmoothStep(0f, 1f, currentTime/halfTime);

            // Interpolate between the lower speed and the initial speed using smooth factor 
            Vector3 currentSpeed = Vector3.Lerp(lowerSpeed, initialSpeed, smoothFactor);

            // Set the velocity of the object using current speed 
            rb.velocity = currentSpeed;

            // Yield until next frame 
            yield return null;

            
        }
        
        
       }

    // Update is called once per frame
    void Update()
    {
        if(initialSpeed==Vector3.zero)
            initialSpeed=rb.velocity;
        

            

        timeStopScript = FindObjectOfType<TimeStop>();
        stopped=timeStopScript.timeStopped;
        if (Input.GetKeyDown(timeStopKey))
        {

           StartCoroutine(SlowDownCoroutine(slowdownTime));
        }
       
    }

    
}
