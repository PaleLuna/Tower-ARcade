using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Rigidbody))]
public class Enemy : MonoBehaviour, IDamagable
{
    [Header("Config")]
    [SerializeField]
    private EnemyConf _baseEnemyConf;
    private EnemyConf _currentEnemyConf;

    [Header("Components")]
    [SerializeField] Rigidbody _rigidbody;

    [SerializeField]
    private bool _canDie = true;

    public readonly UnityEvent<PathPoint> _pathPointReachedEvent = new();

    private EnemyStateHolder _stateHolder;

    public Rigidbody rb => _rigidbody;

    public EnemyConf enemyConf => _currentEnemyConf;

    #region [ MonoBehaviour methods ]
    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        if(!_stateHolder.currentState.GetType().Equals(typeof(EnemyStateDeath)))
            _stateHolder.ChangeState<EnemyStateDeath>();
    }

    private void Awake()
    {
        _stateHolder = new(this);

        _currentEnemyConf = ScriptableObject.CreateInstance<EnemyConf>();
        _currentEnemyConf.Copy(_baseEnemyConf);
    }
    #endregion

    public void SetDamage(float damage)
    {
        _currentEnemyConf.health -= (int)damage;

        if (_currentEnemyConf.health == 0 && _canDie)
            _stateHolder.ChangeState<EnemyStateDeath>();
    }

    public void Respawn(PathPoint startPoint)
    {
        transform.position = startPoint.transform.position;
        _stateHolder.ChangeState<EnemyStateWalk>();
        PathPointReached(startPoint);
        gameObject.SetActive(true);
    }

    public void PathPointReached(PathPoint point) => _pathPointReachedEvent.Invoke(point);
    public void FinishPointReached() => _stateHolder.ChangeState<EnemyStateDeath>();
}
