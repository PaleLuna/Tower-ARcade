using System;
using Services;
using UnityEngine;

public class Base : MonoBehaviour
{  
    HealthCounter _healthCounter;

    private void Start(){
        
        _healthCounter = ServiceManager.Instance.SceneLocator.Get<ValueCounterHolder>().Get<HealthCounter>();

        GameEvents.enemyFinishReachedEvent.AddListener(OnEnemyFinishReached);
    }

    private void OnEnemyFinishReached(Enemy enemy){

        if(!_healthCounter.TryTake(enemy.enemyConf.penaltyForPassing))
            GameEvents.gameDefeatEvent.Invoke();
    }
}
