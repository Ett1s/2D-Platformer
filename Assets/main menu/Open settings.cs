using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opensettings : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject Setting;

    public void on_click()
    {
        Setting.SetActive(true);
        GameIsPaused = true;
    }
    public void Resume()
    {
        Setting.SetActive(false);
        GameIsPaused = false;
    }
}
