using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class Game : MonoBehaviour, IService, IStartable
{
    [SerializeField]
    private LevelConfig _levelConf;

    private ServiceLocator _sceneLocator;

    private bool _isStart = false;

    public bool IsStarted => _isStart;

    public LevelConfig levelConfig => _levelConf;


    public void OnStart()
    {
        if(_isStart) return;

        _sceneLocator = ServiceManager.Instance.SceneLocator;

        _sceneLocator.Registarion(this);

        GameEvents.gameDefeatEvent.AddListener(OnGameDefeat);
        GameEvents.gameRestart.AddListener(OnGameRestart);
        GameEvents.enemyDeathEvent.AddListener(OnEnemyDie);

        _levelConf.levelPrefab.SetMaxHP(_levelConf.maxBaseHealthPoint);

        ResetWallet();

        _isStart = true;
    }

    private void ResetWallet()
    {
        _sceneLocator.Get<Wallet>().SetCurrentBalance(levelConfig.startBalance);
    }

    private void OnGameDefeat()
    {
        
    }
    private void OnGameRestart()
    {
        ResetWallet();
    }

    private void OnEnemyDie(Enemy enemy){
        _sceneLocator.Get<Wallet>().AddToWallet(enemy.enemyConf.awardForKill);
    }

}
