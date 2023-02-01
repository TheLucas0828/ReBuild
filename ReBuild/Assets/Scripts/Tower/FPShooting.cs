using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPShooting : MonoBehaviour
{
    public Vector2 direction;
    public GameObject enemyToHit;
    public float firstID;
    public float speed;
    float timeAlive;
    public float lifeSpan;
    public CircleCollider2D collider;
    public float radius;
    public LayerMask enemyMask;
    public float damage;
    public bool bounce = false;
    public EnemySpawner spawner;
    public TowerAI tower;

    private void Start()
    {
        firstID = enemyToHit.GetComponent<EnemyStats>().ID;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enemyToHit.GetComponent<EnemyStats>().ID);
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
            if (enemy.gameObject.GetComponent<EnemyStats>().ID == enemyToHit.GetComponent<EnemyStats>().ID)
            {
                enemy.gameObject.GetComponent<EnemyStats>().health -= damage;
                Collider2D[] newEnemies = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 2f, enemyMask);
                if (bounce && newEnemies != null && newEnemies[0].gameObject.GetComponent<EnemyStats>().ID != firstID)
                {
                    
                    bounce = false;
                    if(newEnemies != null)
                    {
                        enemyToHit = newEnemies[0].gameObject;
                    }
                    foreach (Collider2D enemies in newEnemies)
                    {
                        if(enemies.gameObject.GetComponent<EnemyStats>().ID != firstID && Vector3.Distance(enemies.transform.position, this.gameObject.transform.position) <
                            Vector3.Distance(enemyToHit.transform.position, this.gameObject.transform.position) && enemyToHit.GetComponent<EnemyStats>().tempHealth <= damage)
                        {
                            enemyToHit = enemies.gameObject;
                        }
                    }
                    tower.ShootEnemy(enemyToHit, this.transform);
                    bounce = false;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
