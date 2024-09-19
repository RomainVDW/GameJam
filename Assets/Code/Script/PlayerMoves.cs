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

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.s_Instance.Player = transform;
        _move = _action.FindAction("Move");
        //_action.FindActionMap("Gameplay").enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 inputValue = _move.ReadValue<Vector2>();
        Vector3 playerDirection = new Vector3(inputValue.x, 0, inputValue.y) * _playerSpeed;
        _playerCtrlr.SimpleMove(playerDirection);
        print(playerDirection);
    }
}
