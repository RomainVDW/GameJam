using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public float _health;
    private Transform _player;
    private Rigidbody _rb;
    private Vector3 _moveDirection;
    public float _speed = 2f;
    void Start()
    {
        _player = GameManager.s_Instance.Player;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = _player.transform.position - transform.position;
        _moveDirection = direction;
        _rb.velocity = new Vector3(_moveDirection.x, transform.position.y, _moveDirection.z) * _speed;
    }
}
