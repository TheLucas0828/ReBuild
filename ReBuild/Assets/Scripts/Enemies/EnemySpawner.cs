using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject tower;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for(int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        SpawnEnemy(enemy);
        StartCoroutine(Spawn());
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

        GameObject newEnemy = Instantiate(enemy, new Vector2(x, y), Quaternion.identity);
        newEnemy.GetComponent<EnemyMovement>().tower = tower;
    }
}
