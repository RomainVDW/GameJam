using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private InputActionAsset _action;
    [SerializeField] private GameObject _panelPause;
    [SerializeField] private GameObject _panelGameOver;
    private InputAction _pause;
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
        _action.FindActionMap("Gameplay").Disable();
    }
    void Lose()
    {
        Time.timeScale = 0;
        _panelGameOver.SetActive(true);
    }
}
