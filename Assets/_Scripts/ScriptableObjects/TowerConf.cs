using UnityEngine;

[CreateAssetMenu(fileName = "TowerConf", menuName = "Configs/Tower")]
public class TowerConf : ScriptableObject
{
    [Header("Combat zone radius")]
    [SerializeField, Range(0, 10)]
    private float _combatRadius;

    [Header("Combat characteristics")]
    [SerializeField, Min(0)]
    private float _damageByHit;
    [SerializeField, Min(0.01F), Tooltip("amount of shots fired per minute")]
    private int _shotsPerMinute;


    public float combatRadius => _combatRadius;
    public float damageByHit => _damageByHit;

    public float shotsPerMinute => _shotsPerMinute;
}
