using PaleLuna.Architecture.GameComponent;
using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class Game : MonoBehaviour, IService, IStartable
{
    [SerializeField]
    private LevelConfig _levelConf;

    private bool _isStart = false;

    public bool IsStarted => _isStart;

    public LevelConfig levelConfig => _levelConf;


    public void OnStart()
    {
        if(_isStart) return;

        ServiceManager.Instance.SceneLocator.Registarion(this);

        GameEvents.gameDefeatEvent.AddListener(OnGameDefeat);

        _levelConf.levelPrefab.SetMaxHP(_levelConf.maxBaseHealthPoint);

        _isStart = true;
    }


    private void OnGameDefeat(){
        GameEvents.gameRestart.Invoke();
    }

}
