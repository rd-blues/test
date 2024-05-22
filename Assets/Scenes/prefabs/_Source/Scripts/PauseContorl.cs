using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseContorl : MonoBehaviour
{
    public static bool isPaused = false;

    public static void Pause()
    {
        PlayerController.instance.state = PlayerController.State.Stop;

        isPaused = true;

        // Global settings
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public static void Resume()
    {
        PlayerController.instance.state = PlayerController.State.Move;

        isPaused = false;

        // Global Settings
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}
