using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPointList;
    [SerializeField] private float _enemySpawnDelay = 0.5f;
    [SerializeField] private List<GameObject> _enemyPrefab;
    [SerializeField] private float _randomSpawnPositionRadius = 3;
    [SerializeField] private float _maxEnemies = 3;
    [SerializeField] private int _minEnemies = 0;
    private static EnemySpawnManager _instance;
    [SerializeField] private int _difficulty = 1;
    private int _aliveEnemiesCount = 0;
    private int _killedEnemiesCount = 0;

    public static EnemySpawnManager s_instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<EnemySpawnManager>();
            return _instance;
        }
    }

    IEnumerator SpawnEnemies()
    {
        _maxEnemies += _difficulty;
        while (_aliveEnemiesCount < _maxEnemies)
        {
            Transform spawnPoint = _spawnPointList[Random.Range(0, _spawnPointList.Count)];
            Vector2 randomSpawnPosition = Random.insideUnitCircle * _randomSpawnPositionRadius;
            Vector3 spawnPosition = new(spawnPoint.position.x + randomSpawnPosition.x, spawnPoint.position.y, spawnPoint.position.z + randomSpawnPosition.y);
            Instantiate(_enemyPrefab[Random.Range(0, _spawnPointList.Count)], spawnPosition, spawnPoint.rotation);
            _aliveEnemiesCount++;
            yield return new WaitForSeconds(_enemySpawnDelay);
        }
    }

    public void RemoveSpawnPoint(Transform spawnPoint)
    {
        _spawnPointList.Remove(spawnPoint);
    }
    public void AddSpawnPoint(Transform spawnPoint)
    {
        _spawnPointList.Add(spawnPoint);
    }
    public void DecreaseAliveEnemiesCount()
    {
        _aliveEnemiesCount--;
        _killedEnemiesCount++;
        if (_aliveEnemiesCount <= _minEnemies)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
}
