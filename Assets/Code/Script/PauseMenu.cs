using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panelPause;
    [SerializeField] private InputActionAsset _action;
    public void Resume()
    {
        Time.timeScale = 1.0f;
        _panelPause.SetActive(false);
        _action.FindActionMap("Gameplay").Enable();
    }
    public void Retry() 
    {
        Time.timeScale = 1.0f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
