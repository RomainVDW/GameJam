using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] List<Transform> _spawnPointList;
    [SerializeField] float _enemySpawnDelay = 0.5f;
    [SerializeField] List<GameObject> _enemyPrefab;
    private float _maxEnemies;

    IEnumerator SpawnEnemies()
    {
        while (_maxEnemies > 0)
        {
            Transform spawnPoint = _spawnPointList[Random.Range(0, _spawnPointList.Count)];
            Instantiate(_enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            _maxEnemies--;
            yield return new WaitForSeconds(_enemySpawnDelay);
        }
        yield return new WaitForSeconds(_enemySpawnDelay);
    }
}
