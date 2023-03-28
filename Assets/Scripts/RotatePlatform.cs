using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatePlatform : MonoBehaviour
{
  public float maxSpeed = 20f; //the maximum speed of rotation
    public float smoothTime = 3f; //the time it takes to reach max speed or stop
    public KeyCode key = KeyCode.Space; //the key to toggle rotation
    private bool rotate = true; //the flag to control rotation
    private float targetSpeed = 0f; //the target speed of rotation
    private float currentSpeed = 0f; //the current speed of rotation
    private float velocity = 0f; //the velocity for smooth damping

    private void Start()
    {
        //set the target speed to the maximum speed
        velocity = maxSpeed;
        currentSpeed = maxSpeed;
        targetSpeed = maxSpeed;
    }
    void Update()
    {
        //if the key is pressed, toggle the rotation flag and the target speed
        if (Input.GetKeyDown(key))
        {
            rotate = !rotate;
            targetSpeed = rotate ? maxSpeed : 0f;
        }

        //smoothly change the current speed towards the target speed
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref velocity, smoothTime);

        //rotate the platform around its own axis with the current speed
        transform.Rotate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}