using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.InputSystem;

public class StaminaController : MonoBehaviour
{
    public Slider staminaMeter;
    public bool timestop = false;
// public Stamina playerStamina;

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
        if (timestop == true)
        {
            staminaMeter.value += Time.deltaTime*10;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("space key was pressed");
            //print("space key was pressed");
            //print(staminaMeter.value);
            //print(timestop);
            if (timestop == false)
                timestop = true;
            else
                timestop = false;
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