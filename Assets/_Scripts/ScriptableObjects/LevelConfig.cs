using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConf", menuName = "Configs/Level")]
public class LevelConfig : ScriptableObject
{
    [Header("Level Prefab")]
    [SerializeField]
    private Base _levelPrefab;

    [Header("Level start characteristics")]
    [SerializeField]
    LevelStartCharacteristics _levelStartChar;

    #region Properties
        public Base levelPrefab => _levelPrefab;

        #region [ Level start properties  ]
            public int maxBaseHealthPoint => _levelStartChar.maxBaseHealthPoint;
            public int startBalance => _levelStartChar.startBalance;
        #endregion
    #endregion

    public void Copy(LevelConfig other){
        _levelStartChar.Copy(other._levelStartChar);
    }
}

[Serializable]
public struct LevelStartCharacteristics 
{
    [Header("Base properties"), Min(1)]
    public int maxBaseHealthPoint;
    [Header("Wallet properties")]
    public int startBalance;

    public void Copy(LevelStartCharacteristics other)
    {
        maxBaseHealthPoint = other.maxBaseHealthPoint;
        startBalance = other.startBalance;
    }

}
