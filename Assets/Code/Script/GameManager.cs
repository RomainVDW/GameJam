using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform _player;
    private static GameManager _instance;
    public Transform Player
    {
        get { return _player; }
        set { _player = value; }
    }
    public enum ELaserState
    {
        Damaging,
        Healing
    }
    public static ELaserState s_laserState;
    private float _laserPhaseTimer = 0;
    [SerializeField] private float _laserDamagingPhaseTimerMax = 30;
    [SerializeField] private float _laserHealingPhaseTimerMax = 10;


    public static GameManager s_Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<GameManager>();
            return _instance;
        }
    }
    private void Start()
    {
        s_laserState = ELaserState.Damaging;
        _laserPhaseTimer = _laserDamagingPhaseTimerMax;
    }

    private void Update()
    {
        UpdateLaserState();
    }

    private void UpdateLaserState()
    {
        _laserPhaseTimer -= Time.deltaTime;
        if (_laserPhaseTimer <= 0)
        {
            switch (s_laserState)
            {
                case ELaserState.Damaging:
                    s_laserState = ELaserState.Healing;
                    _laserPhaseTimer = _laserHealingPhaseTimerMax;
                    break;
                case ELaserState.Healing:
                    s_laserState = ELaserState.Damaging;
                    _laserPhaseTimer = _laserDamagingPhaseTimerMax;
                    break;
            }
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
