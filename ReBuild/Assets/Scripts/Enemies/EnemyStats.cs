using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float timeAlive = 0f;
    public float health;
    public float deathMoney;
    public int ID;
    public EnemyMovement movement;
    public float cost;
    public float tempHealth;

    private void Start()
    {
        tempHealth = health;
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (movement.touchingTower)
        {
            AttackTower(movement.tower);
        }
        if(health <= 0f)
        {
            this.gameObject.GetComponent<EnemyMovement>().tower.GetComponent<PlayerStats>().money += deathMoney;
            Destroy(this.gameObject);
        }
    }

    public void AttackTower(GameObject tower)
    {

    }
}
