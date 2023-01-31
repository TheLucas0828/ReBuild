using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonScripts : MonoBehaviour
{
    public UIScreenManager manager;

    public void OpenUpgradeMenu()
    {
        manager.inMenu = true;
        manager.upgradeMenu.SetActive(true);
        manager.UI.SetActive(false);
    }
}
