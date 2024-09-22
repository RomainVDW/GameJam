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
    private float _heightSpawnOffset = 1;
    private bool _spawning = false;

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
        if (_spawning) yield break;
        _spawning = true;
        _maxEnemies += _difficulty;
        while (_aliveEnemiesCount < _maxEnemies)
        {
            Transform spawnPoint = _spawnPointList[Random.Range(0, _spawnPointList.Count - 1)];
            Vector2 randomSpawnPosition = Random.insideUnitCircle * spawnPoint.GetComponent<SpawnPoint>()._spawnPointRadius;
            Vector3 spawnPosition = new(spawnPoint.position.x + randomSpawnPosition.x, spawnPoint.position.y + _heightSpawnOffset, spawnPoint.position.z + randomSpawnPosition.y);
            Instantiate(_enemyPrefab[Random.Range(0, _enemyPrefab.Count - 1)], spawnPosition, spawnPoint.rotation);
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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform spawnPoint in _spawnPointList)
        {
            Gizmos.DrawWireSphere(spawnPoint.position, spawnPoint.GetComponent<SpawnPoint>()._spawnPointRadius);
        }
    }
}
