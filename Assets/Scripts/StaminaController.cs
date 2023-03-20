using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    public Slider staminaMeter;
    public Stamina playerStamina;

    private void Start()
    {
        playerStamina = GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>();
        staminaMeter = GetComponent<Slider>();
        staminaMeter.maxValue = playerStamina.maxStamina;
        staminaMeter.minValue = playerStamina.minStamina;
    }

    public void SetStamina(int st)
    {
        staminaMeter.value = st;
    }
}
public class Stamina : MonoBehaviour
{
    public int maxStamina = 100;
    public int minStamina = 0;
}
