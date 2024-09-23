using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private InputActionAsset _action;
    [SerializeField] private GameObject _panelPause;
    [SerializeField] private GameObject _panelGameOver;
    private InputAction _pause;
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _selectedResume;
    [SerializeField] private GameObject _selectedRetry;
    private void Awake()
    {
        _pause = _action.FindAction("PauseGame");    
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        Pausing();
    }

    private void OnEnable()
    {
        //_eventSystem.firstSelectedGameObject(_selected);
        _pause.performed += OnPause;
        GameManager.s_Instance._gameOver += Lose;
    }

    private void OnDisable()
    {
        _pause.performed -= OnPause;
        GameManager.s_Instance._gameOver -= Lose;
    }

    void Pausing()
    {
        Time.timeScale = 0f;
        _panelPause.SetActive(true);
        _eventSystem.SetSelectedGameObject(_selectedResume);
        _action.FindActionMap("Gameplay").Disable();
    }

    void Lose()
    {
        Time.timeScale = 0f;
        _panelGameOver.SetActive(true);
        _eventSystem.SetSelectedGameObject(_selectedRetry);
        _action.FindActionMap("Gameplay").Disable();
    }
}
