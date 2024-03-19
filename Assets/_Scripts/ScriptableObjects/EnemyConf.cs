using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConf", menuName = "Configs/Enemy")]
public class EnemyConf : ScriptableObject
{
    [Header("Mobility")]
    [SerializeField]
    private float _speed;

    [Header("Health")]
    [SerializeField, Min(1)]
    private int _health = 1;

    [Space]
    [SerializeField, Min(1)]
    private int _penaltyForPassing = 1;

    public float speed => _speed;

    public int health
    {
        get => _health;
        set
        {
            _health = Mathf.Max(0, value);
        }
    }
    public int penaltyForPassing => _penaltyForPassing;

    public void Copy(EnemyConf other)
    {
        _speed = other._speed;
        _health = other._health;
        _penaltyForPassing = other._penaltyForPassing;
    }
}
