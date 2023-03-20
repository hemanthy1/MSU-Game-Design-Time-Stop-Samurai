using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public GameObject Sword;
    public bool CanAttack = true;
    public float CoolDown = 1.0f;
    public bool isAttacking = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        isAttacking = true;
        CanAttack = false;
        StartCoroutine(ResetCooldown());

    }

    IEnumerator ResetCooldown()
    {
        StartCoroutine(ResetAttack());
        yield return new WaitForSeconds(CoolDown);
        CanAttack = true;
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
}
