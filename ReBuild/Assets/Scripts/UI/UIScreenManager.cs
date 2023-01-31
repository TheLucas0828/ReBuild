using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : MonoBehaviour
{
    public GameObject UI;
    public GameObject upgradeMenu;
    public GameObject rebuildMenu;
    public bool inMenu = false;

    private void Update()
    {
        if (inMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            inMenu = false;
            upgradeMenu.SetActive(false);
            rebuildMenu.SetActive(false);
        }
    }
}
