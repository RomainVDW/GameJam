using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoves : MonoBehaviour
{
    [SerializeField] private CharacterController _playerCtrlr;
    [SerializeField] private InputActionAsset _action;
    [Header("Parameters")]
    [SerializeField] private float _playerSpeed = 10;
    private InputAction _move;
    private InputAction _rotate;
    private InputAction _rotateMouse;
    [SerializeField] private float _rotationSmoothing = 0.5f;
    [SerializeField] private Camera mainCamera;
    private Vector2 _mousePosition;
    private Vector2 _joystickPosition;

    private enum Device
    {
        controller,
        mouseAndKeyboard
    }

    private Device _currentDevice;

    private Animator _animator;
    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
        GameManager.s_Instance.Player = transform;
        _action.FindActionMap("Gameplay").Enable();
        _move = _action.FindAction("Move");
        _rotate = _action.FindAction("Rotate");
        _rotateMouse = _action.FindAction("MouseRotate");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Debug.Log(_currentDevice);
    }

    private void MovePlayer()
    {
        Vector2 moveInput = _move.ReadValue<Vector2>();
        if (moveInput.normalized.magnitude > 0.5f)
        {
            _animator.SetBool("IsRun", true);
        }
        else
        {
            _animator.SetBool("IsRun", false);
        }
        Vector3 playerDirection = new Vector3(moveInput.x, 0, moveInput.y) * _playerSpeed;
        _playerCtrlr.SimpleMove(playerDirection);
        UpdateDevice();
        if (_currentDevice == Device.controller)
        {
            Vector2 rotateInput = _rotate.ReadValue<Vector2>();
            Vector3 playerRotation = new (rotateInput.x,0, rotateInput.y);
            transform.forward = Vector3.Slerp(transform.forward, playerRotation, _rotationSmoothing);
        }
        else
        {
            Aim();
        }
    }

    private void Aim()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        direction.y = 0;
        transform.forward = direction;
    }

    private void UpdateDevice()
    {
        if(Mouse.current.position.ReadValue() != _mousePosition)
        {
            _currentDevice = Device.mouseAndKeyboard;
            _mousePosition = Mouse.current.position.ReadValue();
        }
        else if (_move.ReadValue<Vector2>() != _joystickPosition)
        {
            _currentDevice = Device.controller;
            _joystickPosition = _move.ReadValue<Vector2>();
        }
    }
}
