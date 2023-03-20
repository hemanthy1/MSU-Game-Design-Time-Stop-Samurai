using System;

public class StaminaController : MonoBehaviour
{
    public Slider staminaMeter;
    public Stamina playerStamina;

    private void Start()
    {
        playerStamina = GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>();
        staminaMeter = GetComponent<Slider>();
        staminaMeter.maxValue = playerStamina.maxStamina;
        staminaMeter.value = playerStamina.maxStamina;
    }

    public void SetStamina(int st)
    {
        staminaMeter.value = st;
    }
}
