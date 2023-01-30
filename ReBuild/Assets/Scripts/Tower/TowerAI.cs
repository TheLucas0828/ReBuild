using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    public float attackRadius;
    public float attackSpeed;
    public float health;
    public string targeting = "First";
    bool canShoot = true;
    public EnemySpawner spawner;
    public GameObject bullet;
    public LayerMask enemyMask;

    // Update is called once per frame
    void Update()
    {
        if (canShoot && spawner.gameObject.transform.childCount > 0 && EnemyInRange())
        {
            GameObject returnEnemy = null;
            List<GameObject> enemies = new List<GameObject>();
            for (int i = 0; i < spawner.gameObject.transform.childCount; i++)
            {
                if (InRange(spawner.gameObject.transform.GetChild(i).gameObject)) ;
                {
                    enemies.Add(spawner.gameObject.transform.GetChild(i).gameObject);
                }
            }
            foreach(GameObject enemy in enemies)
            {
                if (Vector3.Distance(enemy.transform.position, this.transform.position) <= attackRadius)
                {
                    returnEnemy = enemy;
                    break;
                }
            }
            switch (targeting)
            {
                case "First":
                    foreach (GameObject enemy in enemies)
                    {
                        if (returnEnemy != null && enemy.GetComponent<EnemyStats>().timeAlive > returnEnemy.GetComponent<EnemyStats>().timeAlive && InRange(enemy))
                        {
                            returnEnemy = enemy;
                        }
                    }
                    break;
                case "Last":
                    foreach (GameObject enemy in enemies)
                    {
                        if (returnEnemy != null && enemy.GetComponent<EnemyStats>().timeAlive < returnEnemy.GetComponent<EnemyStats>().timeAlive && InRange(enemy))
                        {
                            returnEnemy = enemy;
                        }
                    }
                    break;
                case "Strongest":
                    foreach (GameObject enemy in enemies)
                    {
                        if (returnEnemy != null && enemy.GetComponent<EnemyStats>().health > returnEnemy.GetComponent<EnemyStats>().health && InRange(enemy))
                        {
                            returnEnemy = enemy;
                        }
                    }
                    foreach (GameObject enemy in enemies)
                    {
                        if (returnEnemy != null && enemy.GetComponent<EnemyStats>().health == returnEnemy.GetComponent<EnemyStats>().health &&
                            enemy.GetComponent<EnemyStats>().timeAlive > returnEnemy.GetComponent<EnemyStats>().timeAlive && InRange(enemy))
                        {
                            returnEnemy = enemy;
                        }
                    }
                    break;
                case "Closest":
                    foreach (GameObject enemy in enemies)
                    {
                        if (returnEnemy != null && Vector3.Distance(enemy.transform.position, this.gameObject.transform.position) <
                            Vector3.Distance(returnEnemy.transform.position, this.gameObject.transform.position) && InRange(enemy))
                        {
                            returnEnemy = enemy;
                        }
                    }
                    foreach (GameObject enemy in enemies)
                    {
                        if (returnEnemy != null && Vector3.Distance(enemy.transform.position, this.gameObject.transform.position) ==
                            Vector3.Distance(returnEnemy.transform.position, this.gameObject.transform.position) &&
                            enemy.GetComponent<EnemyStats>().timeAlive > returnEnemy.GetComponent<EnemyStats>().timeAlive && InRange(enemy))
                        {
                            returnEnemy = enemy;
                        }
                    }
                    break;
            }
            if (returnEnemy != null)
            {
                ShootEnemy(returnEnemy, bullet);
                canShoot = false;
                StartCoroutine(Cooldown());
            }
        }
    }

    void ShootEnemy(GameObject enemy, GameObject projectile)
    {
        GameObject pewpew = Instantiate(projectile, this.transform.position, Quaternion.identity, this.transform);
        pewpew.GetComponent<FPShooting>().direction = enemy.transform.position - this.transform.position;
    }

    IEnumerator Cooldown()
    {
        for (int i = 0; i < 1f; i++)
        {
            yield return new WaitForSeconds(attackSpeed);
        }
        canShoot = true;
    }

    public bool EnemyInRange()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(this.gameObject.transform.position, attackRadius, enemyMask);
        foreach (Collider2D enemies in enemiesHit)
        {
            return true;
        }
        return false;
    }

    bool InRange(GameObject enemy)
    {
        if(Vector3.Distance(enemy.transform.position, this.transform.position) <= attackRadius)
        {
            return true;
        }
        return false;
    }
}
