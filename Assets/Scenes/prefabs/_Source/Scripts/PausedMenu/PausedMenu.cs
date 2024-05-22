using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VLRTK.Notification;

public class PausedMenu : MonoBehaviour
{
    public GameObject MainMenuWindow;
    public GameObject DeveloperWindow;
    public GameObject ManualWindow;

    bool isOpenManual = false;
    bool isOpenMenu = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(SettingsKey.KeyMenu))
        {
            Menu();
        }

        if (Input.GetKeyDown(SettingsKey.KeyManual))
        {
            Manual();
        }

        if (Input.GetKeyDown(SettingsKey.KeyExit))
        {
            Exit();
        }
    }

    public void btnContinue()
    {
        Menu();
    }

    public void btnMainMenu()
    {
        MessageBox mbox = new MessageBox("Вы уверены ?", "Вы уверены, что хотите выйти? \n результат не сохранится", Notification.Type.Question);
        mbox.AddButton(new ButtonMessageBox("Нет", null, ButtonMessageBox.Type.Border));
        mbox.AddButton(new ButtonMessageBox("Да", LoadMainMenu, ButtonMessageBox.Type.Fill));
        mbox.Show();

        void LoadMainMenu() { SceneManager.LoadScene(0); }
    }
    // private void Start()
    // {
    //     PauseContorl.Resume();
    // }
    public void btnManual()
    {
        Manual();
    }

    public void btnExit()
    {
        Exit();
    }

    void Menu()
    {
        if (!isOpenMenu)
        {
            isOpenMenu = true;

            // Open
            PauseContorl.Pause();

            MainMenuWindow.SetActive(true);
            ManualWindow.SetActive(false);
            DeveloperWindow.SetActive(false);
        }
        else
        {
            isOpenMenu = false;

            // Close
            PauseContorl.Resume();

            MainMenuWindow.SetActive(false);
            ManualWindow.SetActive(false);
            DeveloperWindow.SetActive(false);
        }
    }

    void Manual()
    {
        if (!isOpenManual)
        {
            isOpenManual = true;

            // Open
            PauseContorl.Pause();

            ManualWindow.SetActive(true);
            DeveloperWindow.SetActive(false);
        }
        else
        {
            isOpenManual = false;

            // Close
            PauseContorl.Resume();

            ManualWindow.SetActive(false);
            DeveloperWindow.SetActive(false);
        }
    }

    void Exit()
    {
        PauseContorl.Pause();

        MessageBox mbox = new MessageBox("Вы уверены ?", "Вы уверены, что хотите выйти? \n результат не сохранится", Notification.Type.Question);
        mbox.AddButton(new ButtonMessageBox("Нет", null, ButtonMessageBox.Type.Border));
        mbox.AddButton(new ButtonMessageBox("Да", close, ButtonMessageBox.Type.Fill));
        mbox.Show();

        void close() { Application.Quit(); }
    }
}
