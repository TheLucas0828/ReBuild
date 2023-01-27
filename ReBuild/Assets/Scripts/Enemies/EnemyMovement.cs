using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject tower;
    float speed = 1f;
    public CircleCollider2D radius;
    public bool touchingTower = false;
    void Update()
    {
        touchingTower = false;
        touchPlayer();
        float step = speed * Time.deltaTime;
        if(!touchingTower){
            this.transform.position = Vector2.MoveTowards(this.transform.position, tower.transform.position, step);
        }
    }

    void touchPlayer()
    {
        Collider2D[] towersHit = Physics2D.OverlapCircleAll(transform.position, .4f);
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
