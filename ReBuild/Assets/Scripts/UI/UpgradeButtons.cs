using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtons : MonoBehaviour
{
    public GameObject tower;
    public GameObject spawner;
    public int upgradeNumber;
    public string path;
    public float price;

    private void Update()
    {
        if (tower.GetComponent<PlayerStats>().money < price)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else if (path == "Top")
        {
            if (upgradeNumber != tower.GetComponent<Upgrades>().topPath + 1 || !tower.GetComponent<Upgrades>().firstUpgrade)
            {
                this.gameObject.GetComponent<Button>().interactable = false;
            } else
            {
                this.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        else if (path == "Bottom")
        {
            if (upgradeNumber != tower.GetComponent<Upgrades>().bottomPath + 1 || !tower.GetComponent<Upgrades>().firstUpgrade)
            {
                this.gameObject.GetComponent<Button>().interactable = false;
            } else
            {
                this.gameObject.GetComponent<Button>().interactable = true;
            }
        }
        else if (path == "First" && tower.GetComponent<Upgrades>().firstUpgrade)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        } else
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void BuyUpgrade()
    {
        if(tower.GetComponent<PlayerStats>().money >= price && ((tower.GetComponent<Upgrades>().topPath == upgradeNumber - 1 && path == "Top" && tower.GetComponent<Upgrades>().firstUpgrade) || (tower.GetComponent<Upgrades>().bottomPath == upgradeNumber - 1 && path == "Bottom" && tower.GetComponent<Upgrades>().firstUpgrade) || (path == "First")))
        {
            if(path == "Top")
            {
                tower.GetComponent<Upgrades>().topPath = upgradeNumber;
            } else if(path == "Bottom")
            {
                tower.GetComponent<Upgrades>().bottomPath = upgradeNumber;
            } else if(path == "First")
            {
                tower.GetComponent<Upgrades>().firstUpgrade = true;
            }
            spawner.GetComponent<EnemySpawner>().AddEnemy(spawner.GetComponent<EnemySpawner>().GetNewEnemy(upgradeNumber));
            tower.GetComponent<PlayerStats>().money -= price;
        }
    }
}
