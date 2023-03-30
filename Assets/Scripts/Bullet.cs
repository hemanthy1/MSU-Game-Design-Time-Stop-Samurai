

using System.Collections;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public KeyCode slowDownKey; //the key to slow down the bullet
    public KeyCode speedUpKey; //the key to speed up the bullet
    public float slowDownTime; //the time it takes to slow down or speed up
    private Rigidbody rb;

    private bool stopped=false; //the flag to check if the bullet is stopped

    private Vector3 initialSpeed; //the initial speed of the bullet

    private Vector3 lowerSpeed; //the lower speed of the bullet

    private Vector3 currentVelocity; //the current velocity of the bullet

    private bool first = false;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialSpeed = rb.velocity;
        currentVelocity = initialSpeed;
        lowerSpeed = initialSpeed / 10;
    }

    public void StartSlowDown(float slowDownTime)
    {
        StartCoroutine(SlowDownCoroutine(slowDownTime));
    }

    public void StartSpeedUp(float slowDownTime)
    {
        StartCoroutine(SpeedUpCoroutine(slowDownTime));
    }

    private IEnumerator SlowDownCoroutine(float slowDownTime)
    {
        float halfTime = slowDownTime / 2f;

        float currentTime = 0f;
        while (currentTime < halfTime)
        {
            currentTime += Time.deltaTime;

            float smoothFactor = Mathf.SmoothStep(0.25f, 1f, currentTime / halfTime);

            Vector3 currentSpeed = Vector3.Lerp(initialSpeed, lowerSpeed, smoothFactor);

            rb.velocity = currentSpeed;

            yield return null;
        }

        //set the flag to true when the bullet is slowed down
        stopped = true;
    }

    private IEnumerator SpeedUpCoroutine(float slowDownTime)
    {
        float halfTime = slowDownTime / 2f;

        float currentTime = 0f;
        while (currentTime < halfTime)
        {
            currentTime += Time.deltaTime;

            float smoothFactor = Mathf.SmoothStep(0.4f, 1f, currentTime / halfTime);

            Vector3 currentSpeed = Vector3.Lerp(lowerSpeed, initialSpeed, smoothFactor);

            rb.velocity = currentSpeed;

            yield return null;
        }

        //set the flag to false when the bullet is sped up
        stopped = false;
    }

    void Update()
    {
        if (initialSpeed == Vector3.zero)
        {
            initialSpeed = rb.velocity;
            lowerSpeed = initialSpeed / 10;
        }

        //if the slow down key is pressed and the bullet is not already slowed down, start the slow down coroutine
        if (Input.GetKeyDown(slowDownKey) && !stopped)
        {

            StartCoroutine(SlowDownCoroutine(slowDownTime));
        }

        //if the speed up key is pressed and the bullet is already slowed down, start the speed up coroutine
        if (Input.GetKeyDown(speedUpKey) && stopped)
        {

            StartCoroutine(SpeedUpCoroutine(slowDownTime));
        }
    }
}
