using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Enemy : MonoBehaviour, IDamagable
{
    [Header("Config")]
    [SerializeField]
    private EnemyConf _baseEnemyConf;
    private EnemyConf _currentEnemyConf;

    [Header("Components")]
    [SerializeField] Rigidbody _rigidbody;

    private EnemyStateHolder _stateHolder;

    #region [ MonoBehaviour methods ]
    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        _currentEnemyConf = ScriptableObject.CreateInstance<EnemyConf>();
        _currentEnemyConf.Copy(_baseEnemyConf);
    }
    #endregion

    public void SetDamage(float damage)
    {
        _currentEnemyConf.health -= (int)damage;

        if (_currentEnemyConf.health == 0)
            _stateHolder.ChangeState<EnemyStateDeath>();
    }
}
