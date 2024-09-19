using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyParent : MonoBehaviour
{
    public float _health;
    private Transform _player;

    NavMeshAgent _agent;

    private enum EState
    {
        Chasing,
        Attacking,
        Dead
    }
    private EState _state;

    private Rigidbody _rb;
    private Vector3 _moveDirection;
    public float _speed = 2f;
    void Start()
    {
        _player = GameManager.s_Instance.Player;
        _agent = GetComponent<NavMeshAgent>();
        _state = EState.Chasing;
    }

    void Update()
    {
        _agent.SetDestination(_player.transform.position);
        Debug.Log(_state);
        StateUpdate();
        if (_agent.isStopped)
        {
            StateChange(EState.Attacking);
        }
    }

    void Chasing()
    {

    }
    void Attacking()
    {

    }
    void Dead()
    {

    }
    void StateChange(EState newState)
        {
            StateExit();
            _state = newState;
            StateEnter();
        }

    void StateEnter()
    {
        switch (_state)
        {
            case EState.Chasing:
                break;
            case EState.Attacking:
                break;
            case EState.Dead:
                break;
        }
    }
    void StateUpdate()
    {
        switch (_state)
        {
            case EState.Chasing:
                break;
            case EState.Attacking:
                break;
            case EState.Dead:
                break;
        }
    }
    void StateExit()
    {
        switch (_state)
        {
            case EState.Chasing:
                break;
            case EState.Attacking:
                break;
            case EState.Dead:
                break;
        }
    }
}
