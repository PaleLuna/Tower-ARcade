using UnityEngine;

[CreateAssetMenu(fileName = "LevelConf", menuName = "Configs/Level")]
public class LevelConfig : ScriptableObject
{
    [SerializeField]
    private int _maxBaseHealthPoint;

    public int maxBaseHealthPoint => _maxBaseHealthPoint;

    public void Copy(LevelConfig other){
        _maxBaseHealthPoint = other._maxBaseHealthPoint;
    }
}
