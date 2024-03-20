using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using PaleLuna.Patterns.State.Game;
using Services;
using UnityEngine;

public class Game : MonoBehaviour, IService, IStartable
{
    [SerializeField]
    private LevelConfig _levelConf;

    private ValueCounterHolder _counters;
    private GameController _gameController;

    private bool _isStart = false;

    public bool IsStarted => _isStart;

    public LevelConfig levelConfig => _levelConf;


    public void OnStart()
    {
        if(_isStart) return;

        ServiceManager.Instance.SceneLocator.Registarion(this);

        GetServices();
        SubscribeOnEvents();
        OnGameRestart();

        _isStart = true;
    }

    private void SubscribeOnEvents()
    {
        GameEvents.gameDefeatEvent.AddListener(OnGameDefeat);
        GameEvents.gameRestart.AddListener(OnGameRestart);
        GameEvents.enemyDeathEvent.AddListener(OnEnemyDie);

        GameEvents.gameSetPauseEvent.AddListener(OnPauseGame);
        GameEvents.gameSetResumeEvent.AddListener(OnResumeGame);
    }
    private void GetServices()
    {
        _counters = ServiceManager.Instance.SceneLocator.Get<ValueCounterHolder>();
        _gameController = ServiceManager.Instance.GlobalServices.Get<GameController>();
    }

    public void OnPauseGame() =>
        _gameController.stateHolder.ChangeState<PauseState>();
    public void OnResumeGame() =>
        _gameController.stateHolder.ChangeState<PlayState>();

    private void ResetHealth()
    {
        _counters.Get<HealthCounter>().SetCurrentValue(levelConfig.maxBaseHealthPoint);
    }
    private void ResetScore()
    {
        _counters.Get<EnemyKillCounter>().SetCurrentValue(0);
        _counters.Get<ScoreCounter>().SetCurrentValue(0);
    }
    private void ResetWallet()
    {
        _counters.Get<Wallet>().SetCurrentValue(levelConfig.startBalance);
    }

    private void OnGameDefeat()
    {
        OnPauseGame();
    }
    private void OnGameRestart()
    {
        ResetWallet();
        ResetHealth();
        ResetScore();

        OnResumeGame();
    }
    private void OnEnemyDie(Enemy enemy){
        _counters.Get<Wallet>().Add(enemy.enemyConf.awardForKill);

        _counters.Get<EnemyKillCounter>().Add(1);
        _counters.Get<ScoreCounter>().Add(enemy.enemyConf.awardScore);
    }
}
