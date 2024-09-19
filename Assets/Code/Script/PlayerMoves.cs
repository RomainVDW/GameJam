using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoves : MonoBehaviour
{
    [SerializeField] private CharacterController _playerCtrlr;
    private InputActionAsset _action;
    private InputAction _move;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.s_Instance.Player = transform;
        _move = _action.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _playerCtrlr.SimpleMove(_move.ReadValue<Vector2>());
    }
}
