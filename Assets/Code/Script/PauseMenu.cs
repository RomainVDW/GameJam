using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private InputActionAsset _action;

    public void Resume()
    {
        Time.timeScale = 1.0f;
        _panel.SetActive(false);
        _action.FindActionMap("Gameplay").Enable();
    }
    public void Retry() 
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("BaseScene");
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
