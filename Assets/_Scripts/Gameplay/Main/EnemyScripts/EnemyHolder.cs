using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using PaleLuna.DataHolder;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour, IStartable, IService
{
    [SerializeField]
    private Enemy _enemyPrefab;

    [SerializeField]
    private int _maxEnemiesOnLevel = 10;

    [SerializeField, Tooltip("The interval between spawns in seconds")]
    private int _intervalBetweenSpawns = 5;

    private Queue<Enemy> _enemiesToRespawn;

    private PathPoint _startPoint;

    private Coroutine _respawnEnemies;

    private bool _isStart = false;
    public bool IsStarted => _isStart;

    public void OnStart()
    {
        if (_isStart || !enabled) return;

        ServiceLocator sceneLocator = ServiceManager.Instance.SceneLocator;

        sceneLocator.Registarion(this);

        _startPoint = sceneLocator.Get<PathHolder>().GetPath(0).GetFirst();

        _enemiesToRespawn = new(_maxEnemiesOnLevel);

        StartCoroutine(SpawnEnemies());

        GameEvents.enemyDeathEvent.AddListener(AddEnemyToQueue);
        GameEvents.enemyFinishReachedEvent.AddListener(AddEnemyToQueue);

        _isStart = true;
    }

    private void AddEnemyToQueue(Enemy enemy)
    {
        _enemiesToRespawn.Enqueue(enemy);

        if (_respawnEnemies != null) return;

        _respawnEnemies = StartCoroutine(RespawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < _maxEnemiesOnLevel; i++)
        {
            Enemy enemy = Instantiate(_enemyPrefab, _startPoint.transform.position, Quaternion.identity, transform);
            enemy.gameObject.name = $"enemy {i}";
            enemy.Respawn(_startPoint);

            yield return new WaitForSeconds(_intervalBetweenSpawns);
        }
    }

    private IEnumerator RespawnEnemies()
    {
        while(_enemiesToRespawn.Count > 0)
        {
            yield return new WaitForSeconds(_intervalBetweenSpawns);

            Enemy enemy = _enemiesToRespawn.Dequeue();
            enemy.Respawn(_startPoint);
        }

        _respawnEnemies = null;
    }
}
