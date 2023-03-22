using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    //input fields
    private PlayerActions playerControls;
    private InputAction move;

    //Variables
    public int health = 3;

    //movement fields
    private Rigidbody rb;
    private Collider cd;
    [SerializeField]
    private float moveForce = 1f;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float runSpeed = 5f;
    [SerializeField]
    private float maxRun = 10f;
    private Vector3 forceDirection = Vector3.zero;

    private bool isRunning = false;
    [SerializeField]
    private KeyCode runKey = KeyCode.Space;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private Light playerLight;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        cd = this.GetComponent<Collider>();
        playerControls = new PlayerActions();
        playerLight.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(runKey))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * runSpeed;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * runSpeed;

            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;

            Vector3 horVel = rb.velocity;
            horVel.y = 0;
            if (horVel.sqrMagnitude > maxRun * maxRun)
                rb.velocity = horVel.normalized * maxRun;
        }
        else
        {
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * moveForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * moveForce;

            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;

            Vector3 horVel = rb.velocity;
            horVel.y = 0;
            if (horVel.sqrMagnitude > maxSpeed * maxSpeed)
                rb.velocity = horVel.normalized * maxSpeed;
        }

        LookAt();
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if(move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void OnEnable()
    {
        move = playerControls.PlayerControls.Move;
        playerControls.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.PlayerControls.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the sphere collides with a gameobject of player tag
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 1;
            playerLight.enabled = true;
            StartCoroutine(ResetLight());
        }
    }

    IEnumerator ResetLight()
    {
        yield return new WaitForSeconds(.3f);
        playerLight.enabled = false;
    }
}
