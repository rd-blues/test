using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject MainMenuWindow;
    public GameObject ManualWindow;
    public GameObject LoadWindow;

    public void btnStart()
    {
        // Open Load Here
        MainMenuWindow.SetActive(false);
        LoadWindow.SetActive(true);
        this.GetComponent<LevelLoader>().LoadLevel(1);
    }

    public void btnManual()
    {
        MainMenuWindow.SetActive(false);
        ManualWindow.SetActive(true);
    }

    public void btnExit()
    {
        Application.Quit();
    }
}

