using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject tower;
    public float secondsPerSpawn;
    public GameObject[] enemies;
    int power = 1;
    public float credits = 0f;
    public float creditsGiven;

    private void Start()
    {
        StartCoroutine(GiveCredits());
    }

    private void Update()
    {
        float x = 5f - (0.1f * (power - 1f));
        if(x >= 0.125f) 
        { 
            secondsPerSpawn = x;
        } else
        {
            secondsPerSpawn = 0.125f;
        }

        float creditsToGive = 5f * (Mathf.Pow(1.26f, power - 1f));
        creditsGiven = creditsToGive * 5f;

        if(credits > 0f)
        {
            enemy = enemies[Random.Range(0, enemies.Length - 1)];
            if(enemy.GetComponent<EnemyStats>().cost <= credits)
            {
                credits -= enemy.GetComponent<EnemyStats>().cost;
                SpawnEnemy(enemy);
            }
        }
    }

    void AddEnemy(GameObject newEnemy)
    {
        enemies[enemies.Length] = newEnemy;
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
        float randX = Random.Range(0f, 100f);
        float randY = Random.Range(0f, 100f);

        string randVar = "";

        float x = 0f;
        float y = 0f;

        if(Random.Range(0f, 100f) >= 50f)
        {
            x = Random.Range(0f, 5f);
            randVar = "x";
        } else
        {
            y = Random.Range(0f, 5f);
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
        newEnemy.GetComponent<EnemyMovement>().tower = tower;
        newEnemy.name = newEnemy.name.Replace("(Clone)", "");
    }
}
