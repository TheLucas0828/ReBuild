using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPShooting : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    float timeAlive;
    public float lifeSpan;
    public CircleCollider2D collider;
    public float radius;
    public LayerMask enemyMask;
    public float damage;

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        this.transform.position = Vector2.MoveTowards(this.transform.position, direction, speed * Time.deltaTime);
        CheckCollision();
        if(timeAlive >= lifeSpan)
        {
            Destroy(this.gameObject);
        }
    }
    void CheckCollision()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, radius, enemyMask);
        foreach(Collider2D enemy in enemiesHit)
        {
            enemy.gameObject.GetComponent<EnemyStats>().health -= damage;
            Destroy(this.gameObject);
        }
    }
}
