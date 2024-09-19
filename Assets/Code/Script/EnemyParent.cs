using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public float _health;
    public Transform _player;
    void Awake()
    {
        _player = GameManager.s_Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
