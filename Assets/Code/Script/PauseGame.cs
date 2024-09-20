using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private InputActionAsset _action;
    [SerializeField] private GameObject _panel;
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
        GameManager.s_Instance._gameOver += Pausing;
    }

    private void OnDisable()
    {
        _pause.performed -= OnPause;
    }

    void Pausing()
    {
        Time.timeScale = 0f;
        _panel.SetActive(true);
        _action.FindActionMap("Gameplay").Disable();
    }
}
