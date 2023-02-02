using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPShooting : MonoBehaviour
{
    public Vector2 direction;
    public GameObject enemyToHit;
    public float firstID;
    public float speed;
    public float timeAlive;
    public float lifeSpan;
    public CircleCollider2D collider;
    public float radius;
    public LayerMask enemyMask;
    public float damage;
    public int bounce = 0;
    public EnemySpawner spawner;
    public TowerAI tower;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enemyToHit.GetComponent<EnemyStats>().ID);
        timeAlive += Time.deltaTime;
        if (enemyToHit != null)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, enemyToHit.transform.position, speed * Time.deltaTime);
            CheckCollision();
        }
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
            if (enemy.gameObject.GetComponent<EnemyStats>().ID == enemyToHit.GetComponent<EnemyStats>().ID)
            {
                enemy.gameObject.GetComponent<EnemyStats>().health -= damage;
                Collider2D[] newEnemies = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 2f, enemyMask);
                if (bounce > 0 && newEnemies != null && newEnemies[0].gameObject.GetComponent<EnemyStats>().ID != firstID)
                {
                    bounce--;
                    if(newEnemies != null)
                    {
                        enemyToHit = newEnemies[0].gameObject;
                    }
                    foreach (Collider2D enemies in newEnemies)
                    {
                        if(enemies.gameObject.GetComponent<EnemyStats>().ID != firstID && Vector2.Distance(enemies.transform.position, this.gameObject.transform.position) <
                            Vector2.Distance(enemyToHit.transform.position, this.gameObject.transform.position) && enemyToHit.GetComponent<EnemyStats>().tempHealth < 0)
                        {
                            enemyToHit = enemies.gameObject;
                        }
                    }
                    tower.ShootEnemy(enemyToHit, this.transform.position, bounce);
                    Destroy(this.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
