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
        Time.timeScale = 0f;
        _panel.SetActive(true);
        _action.FindActionMap("Gameplay").Disable();
    }

    private void OnEnable()
    {
        _pause.performed += OnPause;
    }

    private void OnDisable()
    {
        _pause.performed -= OnPause;
    }
}
