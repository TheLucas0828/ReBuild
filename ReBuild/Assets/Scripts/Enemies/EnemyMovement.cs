using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject tower;
    public float speed = 1f;
    public bool touchingTower = false;
    public float stopRadius = 0f;
    void Update()
    {
        touchingTower = false;
        touchPlayer();
        float step = speed * Time.deltaTime;
        if(!touchingTower){
            this.gameObject.GetComponent<MeleeAttack>().canAttack = false;
            this.transform.position = Vector2.MoveTowards(this.transform.position, tower.transform.position, step);
        }
        if (touchingTower)
        {
            this.gameObject.GetComponent<MeleeAttack>().canAttack = true;
        }
    }

    void touchPlayer()
    {
        Collider2D[] towersHit = Physics2D.OverlapCircleAll(transform.position, stopRadius);
        foreach (Collider2D towers in towersHit)
        {
            //Debug.Log(towers.)
            if (towers.gameObject.name == "Tower")
            {
                touchingTower = true;
            }
        }
    }
}
