using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConf", menuName = "Configs/Enemy")]
public class EnemyConf : ScriptableObject
{
    [Header("Main characteristics")]
    [SerializeField]
    private EnemyMainCharacterisctis _mainCharacteristics;

    [Header("Penalties and awards")]
    [SerializeField, Min(1)]
    private int _penaltyForPassing = 1;
    [SerializeField, Min(1)]
    private int _awardForKill;


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

        public int penaltyForPassing => _penaltyForPassing;
        public int awardForKill => _awardForKill;
    #endregion

    public void Copy(EnemyConf other)
    {
        _mainCharacteristics.Copy(other._mainCharacteristics);
        _penaltyForPassing = other._penaltyForPassing;
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
