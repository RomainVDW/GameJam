using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyParent : MonoBehaviour, IHealth
{
    private Transform _player;
    NavMeshAgent _agent;
    private enum EState
    {
        Chasing,
        Attacking,
        Dead
    }
    private EState _state;
    private Vector3 _moveDirection;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _maxHealth;
    private float _health;
    [SerializeField] private float _fireRate;
    private float _fireTimer = 0;

    public virtual void Start()
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
    }

    void Chasing()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            StateChange(EState.Attacking);
        }
    }
    void Attacking()
    {
        if (_agent.remainingDistance >= _agent.stoppingDistance)
        {
            StateChange(EState.Chasing);
        }
    }
    void Dead()
    {

    }
    private void StateChange(EState newState)
    {
        StateExit();
        _state = newState;
        StateEnter();
    }

    private void StateEnter()
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
    private void StateUpdate()
    {
        switch (_state)
        {
            case EState.Chasing:
                Chasing();
                break;
            case EState.Attacking:
                _fireTimer += Time.deltaTime;
                if (_fireTimer >= 1/_fireRate)
                {
                    _fireTimer = 0;
                    Fire();
                }
                Attacking();
                break;
            case EState.Dead:
                Dead();
                break;
        }
    }

    protected virtual void Fire()
    {
        
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

    public void TakeDamage(float damage)
    {
        if (_health - damage < _maxHealth)
        {
            _health = 0;
            OnDeath();
        }
        else
        {
            _health -= damage;
        }
    }

    public void Heal(float heal)
    {
        if (_health + heal > _maxHealth)
        {
            _health = _maxHealth;
        }
        else
        {
            _health += heal;
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
