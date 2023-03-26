using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashScript : MonoBehaviour
{
    private PlayerActions playControls;
    private InputAction dash;

    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private CharacterController cc;

    private bool canDash = false;

    public float dashForce;
    public float dashDuration;
    public float dashCooldown;
    public float dashTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        playControls = new PlayerActions();
        OnEnable();
        canDash = true;
    }

    private void Update()
    {
        if (dash.ReadValue<float>() != 0 && canDash)
            Dash();
    }

    private void Dash()
    {
        Debug.Log("DASH");
        Vector3 forceDash = orientation.forward * dashForce;

        rb.AddForce(forceDash, ForceMode.Impulse);

        canDash = false;

        Invoke(nameof(ResetDash), dashDuration);

        StartCoroutine(ResetCooldown());
    }

    private void ResetDash()
    {

    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    private void OnEnable()
    {
        dash = playControls.PlayerControls.Dash;
        playControls.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        playControls.PlayerControls.Disable();
    }
}
