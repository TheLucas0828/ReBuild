using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject tower;
    public float secondsPerSpawn;
    public List<GameObject> enemies;
    public int power;
    public float credits = 0f;
    public float creditsGiven;
    bool cooldown = true;
    public GameObject[] enemiesForList;

    private void Start()
    {
        StartCoroutine(GiveCredits());
    }

    private void Update()
    {
        power = tower.GetComponent<PlayerStats>().powerLevel;
        float x = 5f - (0.2f * (power - 1f));
        if(x >= 0.125f) 
        { 
            secondsPerSpawn = x;
        } else
        {
            secondsPerSpawn = 0.125f;
        }

        float creditsToGive = 5f * (Mathf.Pow(1.8f, power - 1f));
        creditsGiven = creditsToGive;

        if(credits > 0f)
        {
            enemy = enemies[Random.Range(0, enemies.Count)];
            if(enemy.GetComponent<EnemyStats>().cost <= credits && cooldown)
            {
                cooldown = false;
                credits -= enemy.GetComponent<EnemyStats>().cost;
                SpawnEnemy(enemy);
                StartCoroutine(SpawnCooldown());
            }
        }
    }

    public GameObject GetNewEnemy(int level)
    {
        switch (level)
        {
            case 0:
                GameObject[] zeroLevelList = { enemiesForList[0],
                                                enemiesForList[0],
                                                enemiesForList[0],
                                                enemiesForList[0],
                                                enemiesForList[0],
                                                enemiesForList[1],
                                                enemiesForList[2] };
                return zeroLevelList[Random.Range(0, zeroLevelList.Length)];
                break;
            case 1:
                GameObject[] firstLevelList = { enemiesForList[0],
                                                    enemiesForList[1],
                                                    enemiesForList[2] };
                return firstLevelList[Random.Range(0, firstLevelList.Length)];
                break;
            case 2:
                GameObject[] secondLevelList = { enemiesForList[1],
                                                    enemiesForList[2] };
                return secondLevelList[Random.Range(0, secondLevelList.Length)];
                break;
            case 3:
                GameObject[] thirdLevelList = { enemiesForList[1],
                                                    enemiesForList[2] };
                return thirdLevelList[Random.Range(0, thirdLevelList.Length)];
                break;
        }
        return null;
    }

    public void AddEnemy(GameObject newEnemy)
    {
        enemies.Add(newEnemy);
    }

    IEnumerator SpawnCooldown()
    {
        for(int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(0.075f);
        }
        cooldown = true;
    }

    IEnumerator GiveCredits()
    {
        for(int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(secondsPerSpawn);
        }
        credits += creditsGiven;
        StartCoroutine(GiveCredits());
    }

    void SpawnEnemy(GameObject enemy)
    {
        int randID = Random.Range(0, 10000);

        float randX = Random.Range(0f, 100f);
        float randY = Random.Range(0f, 100f);

        string randVar = "";

        float x = 0f;
        float y = 0f;

        if(Random.Range(0f, 100f) >= 50f)
        {
            x = Random.Range(0.1f, 5f);
            randVar = "x";
        } else
        {
            y = Random.Range(0.1f, 5f);
            randVar = "y";
        }

        if(randVar == "x" && x < 3.65f)
        {
            y = ((randY / 100f) * 1.35f) + 3.65f;
        }
        if(randVar == "y" && y < 3.65f)
        {
            x = ((randX / 100f) * 1.35f) + 3.65f;
        }

        if (Random.Range(0, 100) >= 50f)
        {
            x *= -1f;
        }
        if(Random.Range(0, 100) >= 50f)
        {
            y *= -1f;
        }

        GameObject newEnemy = Instantiate(enemy, new Vector2(x, y), Quaternion.identity, this.transform);
        newEnemy.GetComponent<EnemyStats>().ID = randID;
        newEnemy.GetComponent<EnemyMovement>().tower = tower;
        newEnemy.GetComponent<MeleeAttack>().tower = tower;
        newEnemy.name = newEnemy.name.Replace("(Clone)", "");
    }
}
