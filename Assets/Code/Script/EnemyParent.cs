using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyParent : MonoBehaviour, IHealth
{
    private Transform _player;
    protected NavMeshAgent _agent;
    protected enum EState
    {
        Chasing,
        Attacking,
        Dead
    }
    private EState _state;
    private Vector3 _moveDirection;
    [SerializeField] private float _maxHealth;
    private float _health;
    [SerializeField] protected float _fireRate;
    protected float _fireTimer = 0;
    private float _rotationSpeed = 0.9f;
    protected bool _isFiring = false;
    private bool _canTakeDamage = true;

    public virtual void Start()
    {
        _player = GameManager.s_Instance.Player;
        _agent = GetComponent<NavMeshAgent>();
        _state = EState.Chasing;
    }

    void Update()
    {
        _agent.SetDestination(_player.transform.position);
        StateUpdate();
        
    }

    void Chasing()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            StateChange(EState.Attacking);
        }
    }
    protected virtual void Attacking()
    {
        if (!_isFiring)
        {
            Quaternion rotation = Quaternion.LookRotation(_player.transform.position - transform.position);
            Vector3 rotationVect = rotation.eulerAngles.y * Vector3.up;
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, rotationVect, _rotationSpeed);
        }
        if (_agent.remainingDistance >= _agent.stoppingDistance)
        {
            StateChange(EState.Chasing);
        }
        _fireTimer += Time.deltaTime;
        if (_fireTimer >= 1 / _fireRate)
        {
            _fireTimer = 0;
            Fire();
        }
    }
    void Dead()
    {
        
    }
    protected void StateChange(EState newState)
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
        if (!_canTakeDamage) return;
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
        if (!_canTakeDamage) return;
        _canTakeDamage = false;
        EnemySpawnManager.s_instance.DecreaseAliveEnemiesCount();
        Destroy(GetComponent<Laser>());
        Destroy(GetComponent<RangedEnemy>());
        Debug.Log("I Was Here");
        GetComponent<Animator>().SetTrigger("Dead");
    }
}
