using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public bool canAttack = false;
    public GameObject tower;
    public float damage;
    public float attackSpeed;
    public int cooldown = 0;

    // Update is called once per frame
    void Update()
    {
        if(cooldown == 0 && canAttack)
        {
            cooldown = 1;
            StartCoroutine(Cooldown());
        }

        if (canAttack && cooldown == 2)
        {
            cooldown = 0;
            tower.GetComponent<PlayerStats>().health -= damage;
        }
    }

    IEnumerator Cooldown()
    {
        for(int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(attackSpeed);
        }
        cooldown = 2;
    }
}
