using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image _laserStateImage;
    [SerializeField] private Sprite _laserStateDamaging;
    [SerializeField] private Sprite _laserStateHealing;
    public event Action _gameOver;
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
    public float LaserPhaseTimer
    {
        get { return _laserPhaseTimer; }
    }
    [SerializeField] private float _laserDamagingPhaseTimerMax = 30;
    [SerializeField] private float _laserHealingPhaseTimerMax = 10;
    public bool BouclierIsActivated { get; set; }

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
       // Time.timeScale = 0f;
    }

    private void Update()
    {
        UpdateLaserState();
        Debug.Log(s_laserState);
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
                    _laserStateImage.sprite = _laserStateHealing;
                    _laserPhaseTimer = _laserHealingPhaseTimerMax;
                    break;
                case ELaserState.Healing:
                    s_laserState = ELaserState.Damaging;
                    _laserStateImage.sprite = _laserStateDamaging;
                    _laserPhaseTimer = _laserDamagingPhaseTimerMax;
                    break;
            }
        }
    }

    public void GameOver()
    {
        _gameOver.Invoke();
       // Time.timeScale = 0;
    }
}
