using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeStop : MonoBehaviour
{

    public KeyCode timeStopKey;
    private PlayerActions playControls;
    private InputAction timestop;

    public GameObject timePostProcess;

    public GameObject postProcess;

    public bool timeStopped=false;

    private bool current = false;









    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(timeStopKey))
        {
            

            timeStopped = !timeStopped;

            if (timeStopped)
            {
                timePostProcess.SetActive(true);
                postProcess.SetActive(false);
            }
            else
            {
                timePostProcess.SetActive(false);
                postProcess.SetActive(true);
            }
            current = true;
        }
        else
        {
            current = false;
        }
            
    
    }

    private void OnEnable()
    {
        timestop = playControls.PlayerControls.TimeStop;
        playControls.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        playControls.PlayerControls.Disable();
    }
}
