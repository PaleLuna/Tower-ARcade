using System;
using Services;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private int _maxHealthPoints;
    [SerializeField]
    private int _currentHealthPoints;

    private void Start(){
        
        _currentHealthPoints = _maxHealthPoints;

        GameEvents.enemyFinishReachedEvent.AddListener(OnEnemyFinishReached);
        GameEvents.gameRestart.AddListener(OnGameRestart);
    }

    public void SetMaxHP(int value){
        _maxHealthPoints = value;
    }
    public void ResetCurrentHP()
    {
        _currentHealthPoints = _maxHealthPoints;
    }

    private void OnEnemyFinishReached(Enemy enemy){
        _currentHealthPoints = Math.Max(_currentHealthPoints - enemy.enemyConf.penaltyForPassing, 0);

        if(_currentHealthPoints == 0)
            GameEvents.gameDefeatEvent.Invoke();
    }
    private void OnGameRestart()
    {
        ResetCurrentHP();
    }
}
