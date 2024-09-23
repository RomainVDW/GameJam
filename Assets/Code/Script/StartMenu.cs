using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartPlay()
    {
        SceneManager.LoadScene("Level1");
    }
    public void StartEndless()
    {
        SceneManager.LoadScene("Level_Endless");
    }
    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
