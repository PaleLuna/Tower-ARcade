using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private LevelConfig _levelConfig;

    private int _currentHealthPoints;
    private void Start(){
        _currentHealthPoints = _levelConfig.maxBaseHealthPoint;

        GameEvents.enemyFinishReachedEvent.AddListener(OnEnemyFinishReached);
    }

    private void OnEnemyFinishReached(Enemy enemy){
        _currentHealthPoints = Math.Max(_currentHealthPoints - enemy.enemyConf.penaltyForPassing, 0);

        if(_currentHealthPoints == 0)
            GameEvents.gameDefeatEvent.Invoke();
    }
}
