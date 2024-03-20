using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour, IStartable, IService, IPausable
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

        ServiceManager.Instance.SceneLocator.Registarion(this);

        _enemiesToRespawn = new(_maxEnemiesOnLevel);

        GameEvents.enemyDeathEvent.AddListener(OnEnemyDeactivate);

        GameEvents.levelPlaceFirstly.AddListener(OnLevelPlaceFirstly);
        GameEvents.gameRestart.AddListener(DeactivateAllEnemies);
        GameEvents.levelConfirmEvent.AddListener(OnLevelRestart);

        ServiceManager.Instance.GlobalServices.Get<GameController>().pausablesHolder.Registration(this);   

        _isStart = true;
    }

    private void OnLevelPlaceFirstly(){
        _startPoint = ServiceManager.Instance.SceneLocator.Get<PathHolder>().GetPath(0).GetFirst();
        StartCoroutine(SpawnEnemies());
    }

    private void OnLevelRestart(){

        print("Restarting");

        StopRespawn();

        _respawnEnemies = null;

        StartRespawn();
    }

    private void OnEnemyDeactivate(Enemy enemy){
        AddEnemyToQueue(enemy);

        StartRespawn();
    }

    private void DeactivateAllEnemies()
    {
        StopRespawn();

        Enemy[] enemies = GetComponentsInChildren<Enemy>(includeInactive:true);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].Deactivate();
            AddEnemyToQueue(enemies[i]);
        }
    }

    private void AddEnemyToQueue(Enemy enemy) =>
        _enemiesToRespawn.Enqueue(enemy);



    public void OnPause()
    {
        StopRespawn();
    }

    public void OnResume()
    {
        StartRespawn();
    }

    private void StartRespawn()
    {
        if (_respawnEnemies == null)
            _respawnEnemies = StartCoroutine(RespawnEnemies());
    }
    private void StopRespawn()
    {
        if(_respawnEnemies == null) return;

        StopCoroutine(_respawnEnemies);
        _respawnEnemies = null;
    }

    

    #region [ Coroutines ]
    private IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < _maxEnemiesOnLevel; i++)
        {
            Enemy enemy = Instantiate(_enemyPrefab, _startPoint.transform.position, Quaternion.identity, transform);
            enemy.gameObject.name = $"enemy {i}";

            enemy.Deactivate();
            AddEnemyToQueue(enemy);

            yield return new WaitForEndOfFrame();
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

    
    #endregion
}
