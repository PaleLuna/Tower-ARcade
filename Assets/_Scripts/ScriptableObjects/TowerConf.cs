using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerConf", menuName = "Configs/Tower")]
public class TowerConf : ScriptableObject
{
    [Header("Main characteristics")]
    [SerializeField]
    private TowerMainCharacteristics _mainCharacteristics;

    [Header("Combat characteristics")]
    [SerializeField]
    private TowerCombatCharacteristics _combatCharacteristics; 

    #region [ Properties ]
        #region [ Main properties ] 
            public int cost => _mainCharacteristics.towerCost;
        #endregion
        #region [ Combat properties ]
            public float combatRadius => _combatCharacteristics.combatRadius;
            public int damageByHit => _combatCharacteristics.damageByHit;
        
            public float shotsPerMinute => _combatCharacteristics.shotsPerMinute;
            public float initialSpeed => _combatCharacteristics.shellInitialSpeed;
        #endregion
    #endregion

    public void Copy(TowerConf other)
    {
        _mainCharacteristics.Copy(other._mainCharacteristics);
        _combatCharacteristics.Copy(other._combatCharacteristics);
    }
}

#region [ Structs ]
    [Serializable]
    public struct TowerCombatCharacteristics
    {
        [Range(0, 10)]
        public float combatRadius;
    
        [Min(0)]
        public int damageByHit;
        [Min(0.01F), Tooltip("amount of shots fired per minute")]
        public int shotsPerMinute;
        public float shellInitialSpeed;
    
        public void Copy(TowerCombatCharacteristics other)
        {
            combatRadius = other.combatRadius;
            damageByHit = other.damageByHit;
            shotsPerMinute = other.shotsPerMinute;
            shellInitialSpeed = other.shellInitialSpeed;
        }
    }

    [Serializable]
    public struct TowerMainCharacteristics
    {
        public int towerCost;

        public void Copy(TowerMainCharacteristics other)
        {
            towerCost = other.towerCost;
        }
    }
#endregion
