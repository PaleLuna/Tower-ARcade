using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TowerConf _towerConf;
    [SerializeField] private CombatZone _combatZone;

    [Header("Objects")]
    [SerializeField] private Transform _towerHead;
    [SerializeField] private Transform _Muzzel;

    [Header("Bullet prefab")]
    private Transform _bulletPrefab;

    private TowerStateHolder _stateHolder;

    public TowerConf towerConf => _towerConf;
    public CombatZone combatZone => _combatZone;
    public TowerStateHolder stateHolder => _stateHolder;
    public Transform towerHead => _towerHead;

    private void Start()
    {
        _stateHolder = new(this);

        _combatZone.Init(_towerConf);
    }

    virtual public void Fire(Transform enemyTransform) //In the future it will be used for shooting
    {
        
    }
    virtual public void RotateHead(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        _towerHead.rotation = Quaternion.AngleAxis(rotation.eulerAngles.y, Vector3.up);
    }
}
