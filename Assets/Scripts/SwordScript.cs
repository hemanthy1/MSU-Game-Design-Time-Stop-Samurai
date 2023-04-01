using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordScript : MonoBehaviour
{
    private PlayerActions playerCon;
    private InputAction attack;

    public GameObject Sword;
    public bool CanAttack = true;
    public float CoolDown = 1.0f;
    public bool isAttacking = false;

    [SerializeField]
    private Light swordLight;

    private void Awake()
    {
        playerCon = new PlayerActions();
        swordLight.enabled = false;
    }

    private void Update()
    {
        if (attack.ReadValue<float>() != 0)
        {
            if (CanAttack)
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        Debug.Log("Attack");
        isAttacking = true;
        CanAttack = false;
        swordLight.enabled = true;
        FindObjectOfType<AudioManager>().PlaySound("SwordSwing");
        StartCoroutine(ResetCooldown());

    }

    IEnumerator ResetCooldown()
    {
        Debug.Log("ResetCooldown");
        StartCoroutine(ResetAttack());
        yield return new WaitForSeconds(CoolDown);

        CanAttack = true;
    }

    IEnumerator ResetAttack()
    {
        Debug.Log("ResetAttack");
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
        swordLight.enabled = false;
    }

    private void OnEnable()
    {
        attack = playerCon.PlayerControls.Attack;
        playerCon.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        playerCon.PlayerControls.Disable();
    }
}
