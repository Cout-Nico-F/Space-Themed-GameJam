using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemiesConfiguration enemiesConfiguration;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private EnemyId defaultEnemy;
    [SerializeField] private float timeToSpawn;

    private EnemyFactory _enemyFactory;
    private float _timeUntilSpawn;

    private void Awake()
    {
        var instance = Instantiate(enemiesConfiguration);
        _enemyFactory = new EnemyFactory(instance);
        _timeUntilSpawn = timeToSpawn;
    }

    private void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;
        if (_timeUntilSpawn <= 0)
        {
            var enemy = _enemyFactory.Create(defaultEnemy.Value, enemySpawnPoint.position, Quaternion.identity);
            enemy.Init();
            _timeUntilSpawn = timeToSpawn;
        }
    }
}
