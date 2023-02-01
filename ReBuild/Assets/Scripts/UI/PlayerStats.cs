using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public float attackSpeed;
    public int powerLevel;
    public float level;
    public TextMeshProUGUI levelText;
    public float money;
    public TextMeshProUGUI moneyText;
    public float health;
    public float maxHealth;
    public TextMeshProUGUI healthText;
    public string towerClass;
    public GameObject gunnerBullet;
    public GameObject heavyBullet;
    public Upgrades upgrader;

    // Update is called once per frame
    void Update()
    {
        levelText.text = level.ToString();
        moneyText.text = money.ToString();
        healthText.text = health.ToString() + "/" + maxHealth.ToString();

        if(towerClass == "Gunner")
        {
            if (upgrader.bottomPath < 1)
            {
                this.gameObject.GetComponent<TowerAI>().bullet = gunnerBullet;
            }
            if (upgrader.firstUpgrade)
            {
                attackSpeed = .60f;
            }
            switch (upgrader.topPath)
            {
                case 1:
                    attackSpeed = .4f;
                    break;
                case 2:
                    attackSpeed = .2f;
                    break;
                case 3:
                    attackSpeed = .05f;
                    break;
            }
            switch (upgrader.bottomPath)
            {
                case 1:
                    this.gameObject.GetComponent<TowerAI>().bullet = heavyBullet;
                    break;
                case 2:
                    this.gameObject.GetComponent<TowerAI>().bouncing = true;
                    break;
                case 3:
                    break;
            }




        }
    }
}
