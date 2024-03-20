using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConf", menuName = "Configs/Enemy")]
public class EnemyConf : ScriptableObject
{
    [Header("Main characteristics")]
    [SerializeField]
    private EnemyMainCharacterisctis _mainCharacteristics;

    [Header("Penalties and awards")]
    [SerializeField]
    private EnemyPenaltiesAndAwards _penaltiesAndAwards;
    
    


    #region [ Properties ]
        #region [ Main characteristics properties ]
            public float speed => _mainCharacteristics.speed;
            public int health
            {
                get => _mainCharacteristics.health;
                set
                {
                    _mainCharacteristics.health = Mathf.Max(0, value);
                }
            }
        #endregion

        public int penaltyForPassing => _penaltiesAndAwards.penaltyForPassing;
        public int awardForKill => _penaltiesAndAwards.awardForKill;
        public int awardScore => _penaltiesAndAwards.awardScore;
    #endregion

    public void Copy(EnemyConf other)
    {
        _mainCharacteristics.Copy(other._mainCharacteristics);
        _penaltiesAndAwards.Copy(other._penaltiesAndAwards);
    }
}

[Serializable]
public struct EnemyMainCharacterisctis
{
    [Header("Mobility")]
    public float speed;

    [Header("Health")]
    [Min(1)]
    public int health;

    public void Copy(EnemyMainCharacterisctis other)
    {
        speed = other.speed;
        health = other.health;
    }
}
[Serializable]
public struct EnemyPenaltiesAndAwards
{
    [Min(1)]
    public int penaltyForPassing;
    [Min(1)]
    public int awardForKill;
    [Min(1)]
    public int awardScore;

    public void Copy(EnemyPenaltiesAndAwards other)
    {
        penaltyForPassing = other.penaltyForPassing;
        awardForKill = other.awardForKill;
        awardScore = other.awardScore;
    }
}
