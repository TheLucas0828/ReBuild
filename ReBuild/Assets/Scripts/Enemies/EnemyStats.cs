using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float timeAlive = 0f;
    public float health;
    public float damage;
    public EnemyMovement movement;
    public float cost;

    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (movement.touchingTower)
        {
            AttackTower(movement.tower);
        }
        if(health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void AttackTower(GameObject tower)
    {

    }
}
