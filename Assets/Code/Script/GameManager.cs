using System.Collections;
using System.Collections.Generic;
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
    
    public static GameManager s_Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<GameManager>();
            return _instance;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
