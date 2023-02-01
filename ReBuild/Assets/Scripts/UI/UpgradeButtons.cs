using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtons : MonoBehaviour
{
    public GameObject tower;
    public int upgradeNumber;
    public string path;
    public float price;

    private void Update()
    {
        if (tower.GetComponent<PlayerStats>().money < price)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else if (path == "Top" && upgradeNumber <= tower.GetComponent<Upgrades>().topPath)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else if (path == "Bottom" && upgradeNumber <= tower.GetComponent<Upgrades>().bottomPath)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
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
        if(tower.GetComponent<PlayerStats>().money >= price && ((tower.GetComponent<Upgrades>().topPath < upgradeNumber && path == "Top" && tower.GetComponent<Upgrades>().firstUpgrade) || (tower.GetComponent<Upgrades>().bottomPath < upgradeNumber && path == "Bottom" && tower.GetComponent<Upgrades>().firstUpgrade) || (path == "First")))
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
            tower.GetComponent<PlayerStats>().money -= price;
        }
    }
}
