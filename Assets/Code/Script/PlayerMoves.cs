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
    [SerializeField] private float _rotationSmoothing = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.s_Instance.Player = transform;
        _action.FindActionMap("Gameplay").Enable();
        _move = _action.FindAction("Move");
        _rotate = _action.FindAction("Rotate");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 moveInput = _move.ReadValue<Vector2>();
        Vector3 playerDirection = new Vector3(moveInput.x, 0, moveInput.y) * _playerSpeed;
        _playerCtrlr.SimpleMove(playerDirection);
        Vector2 rotateInput = _rotate.ReadValue<Vector2>();
        Vector3 playerRotation = new (rotateInput.x,0, rotateInput.y);
        transform.forward = Vector3.Slerp(transform.forward, playerRotation, _rotationSmoothing);
    }
}
