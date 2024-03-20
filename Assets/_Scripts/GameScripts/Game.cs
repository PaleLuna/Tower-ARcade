using System;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class Game : MonoBehaviour, IService, IStartable
{
    [SerializeField]
    private LevelConfig _levelConf;

    private ValueCounterHolder _counters;

    private bool _isStart = false;

    public bool IsStarted => _isStart;

    public LevelConfig levelConfig => _levelConf;


    public void OnStart()
    {
        if(_isStart) return;

        _counters = ServiceManager.Instance.SceneLocator.Get<ValueCounterHolder>();

        ServiceManager.Instance.SceneLocator.Registarion(this);

        GameEvents.gameDefeatEvent.AddListener(OnGameDefeat);
        GameEvents.gameRestart.AddListener(OnGameRestart);
        GameEvents.enemyDeathEvent.AddListener(OnEnemyDie);

        OnGameRestart();

        _isStart = true;
    }

    private void ResetHealth()
    {
        _counters.Get<HealthCounter>().SetCurrentValue(levelConfig.maxBaseHealthPoint);
    }

    private void ResetWallet()
    {
        _counters.Get<Wallet>().SetCurrentValue(levelConfig.startBalance);
    }

    private void OnGameDefeat()
    {
        //TODO
    }
    private void OnGameRestart()
    {
        ResetWallet();
        ResetHealth();
    }

    private void OnEnemyDie(Enemy enemy){
        _counters.Get<Wallet>().Add(enemy.enemyConf.awardForKill);
    }

}
