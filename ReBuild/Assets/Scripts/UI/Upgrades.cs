using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public float money;
    public GameObject tower;
    public bool firstUpgrade = false;
    public int topPath;
    public int bottomPath;

    // Update is called once per frame
    void Update()
    {
        money = tower.GetComponent<PlayerStats>().money;
        if (firstUpgrade)
        {
            tower.GetComponent<PlayerStats>().powerLevel = topPath + bottomPath + 2;
        } else
        {
            tower.GetComponent<PlayerStats>().powerLevel = 1;
        }
    }
}
