using PaleLuna.DataHolder;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class CombatZone : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SphereCollider _sphereCollider;

    private TowerConf _combatConf;

    private readonly DataHolder<Enemy> _enemies = new();

    #region [ Events ]
    private readonly UnityEvent _firstEnemyEnter = new();
    private readonly UnityEvent _lastEnemyExit = new();

    #region [ Events Subscribe methods ]
    public void SubscribeOnFirstEnter(UnityAction action) => _firstEnemyEnter.AddListener(action);
    public void SubscribeOnLastExit(UnityAction action) => _lastEnemyExit.AddListener(action);

    public void UnsubscribeOnFirstEnter(UnityAction action) => _firstEnemyEnter.RemoveListener(action);
    public void UnsubscribeOnLastExit(UnityAction action) => _lastEnemyExit.RemoveListener(action);
    #endregion
    #endregion

    private void OnEnable()
    {
        GameEvents.enemyDeathEvent.AddListener(OnEnemyDead);
    }
    private void OnDisable()
    {
        GameEvents.enemyDeathEvent.RemoveListener(OnEnemyDead);
    }

    private void OnValidate()
    {
        _sphereCollider ??= GetComponent<SphereCollider>();

        _sphereCollider.isTrigger = true;
    }

    public Enemy GetEnemy() => _enemies.At(0);

    public void UpdateConf(TowerConf towerConf)
    {
        _combatConf = towerConf;
        _sphereCollider.radius = _combatConf.combatRadius;
    }

    private void OnEnemyDead(Enemy enemy)
    {
        RemoveEnemy(enemy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Enemy enemy)) return;

        _enemies.Registration(enemy);

        if (_enemies.Count == 1) _firstEnemyEnter.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Enemy enemy)) return;

        RemoveEnemy(enemy);
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _enemies.Unregistration(enemy);

        if (_enemies.Count == 0) _lastEnemyExit.Invoke();
    }
}
