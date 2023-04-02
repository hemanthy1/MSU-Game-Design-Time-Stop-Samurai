using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //input fields
    private PlayerActions playerControls;
    private InputAction move;
    private InputAction run;
    private InputAction dash;

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
    [SerializeField]
    private float dashForce = 15f;
    [SerializeField]
    private float dashDuration = .25f;
    [SerializeField]
    private float dashCooldown = 1f;
    private Vector3 forceDirection = Vector3.zero;
    private bool canHit = false;
    private bool canDash = false;

    private Animator animator;
    private bool isRunning = false;
    private bool isDashing = false;
    //[SerializeField]
    //private KeyCode runKey = KeyCode.Space;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private Light playerLight;

    /*[SerializeField]
    private Canvas gameCanvas;

    private GameObject gameOverUI;*/

    //[SerializeField]
    //private TextMeshProUGUI healthTxt;

    private void Awake()
    {
        animator = GameObject.Find("Rigged MC").GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        cd = this.GetComponent<Collider>();
        playerControls = new PlayerActions();
        playerLight.enabled = false;
        canHit = true;
        canDash = true;
        //gameOverUI = gameCanvas.GetComponentInChildren<>
        //Debug.Log(gameOverUI);
    }

    private void Update()
    {

        if (dash.ReadValue<float>() != 0 && canDash)
        {
            isDashing = true;
            Dash();
        }
        if (run.ReadValue<float>() != 0)
        {
            Debug.Log("Moving");

            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (health <= 0)
        {
            OnDisable();
            //gameOverUI.SetActive(true);
        }
        //Debug.Log(health);
        //healthTxt.text = "Health: " + health;

        /*if (isDashing)
        {
            cd.enabled = false;
        }
        else
        {
            cd.enabled = true;
        }*/
    }

    private void FixedUpdate()
    {
        //Debug.Log("isDashing: "+isDashing+"\nisRunning: "+isRunning);
        if (isDashing)
        {
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * dashForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * dashForce;

            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;

            Vector3 horVel = rb.velocity;
            horVel.y = 0;
            if (horVel.sqrMagnitude > dashForce * dashForce)
                rb.velocity = horVel.normalized * dashForce;
        }
        else if (isRunning)
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
            Debug.Log("forceDircecion" + forceDirection);
            if(forceDirection==Vector3.zero)
                animator.SetBool("IsMoving", false);
            else
                animator.SetBool("IsMoving", true);
            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;

            Vector3 horVel = rb.velocity;
            horVel.y = 0;
            if (horVel.sqrMagnitude > maxSpeed * maxSpeed)
                rb.velocity = horVel.normalized * maxSpeed;

            Debug.Log("Moving");
        }


        LookAt();
    }

    private void Dash()
    {
        canDash = false;

        StartCoroutine(ResetDash());

        StartCoroutine(ResetCooldown());
    }

    IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
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
        run = playerControls.PlayerControls.Run;
        dash = playerControls.PlayerControls.Dash;
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
            canHit = false;
            playerLight.enabled = true;
            StartCoroutine(ResetLight());
            FindObjectOfType<AudioManager>().PlaySound("PlayerHit");
        }
    }

    IEnumerator ResetLight()
    {
        yield return new WaitForSeconds(.5f);
        canHit = true;
        playerLight.enabled = false;
    }
}
