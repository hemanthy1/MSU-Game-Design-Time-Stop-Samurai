using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StaminaController : MonoBehaviour
{
    public Slider staminaMeter;
    public bool timestopped = false;
    private InputAction timestop;
    public TimeStop script;
    public GameObject timePostProcess;

    public GameObject postProcess;

    private void Start()
    {
// playerStamina = GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>();
        //staminaMeter = GetComponent<Slider>();
        staminaMeter.maxValue = 100;
        staminaMeter.minValue = 0;
        staminaMeter.value = staminaMeter.minValue;
    }
    public void Update()
    {
        if (timestopped == true)
        {
            staminaMeter.value += Time.deltaTime*10;
            
        }
        if (script.timeStopped == true)
            timestopped = true;
        else
            timestopped = false;

        if (staminaMeter.value >= staminaMeter.maxValue)
        {
            //timestop. = 0;
            timestopped = false;
            script.timeStopped = false;
            timePostProcess.SetActive(false);
            postProcess.SetActive(true);
        }
            

        if (Input.GetKeyDown("v"))
        {
            staminaMeter.value -= 10;
            if (staminaMeter.value < 0)
                staminaMeter.value = 0;
        }
    }
    public void SetStamina(int st)
    {
        staminaMeter.value = st;
    }
}