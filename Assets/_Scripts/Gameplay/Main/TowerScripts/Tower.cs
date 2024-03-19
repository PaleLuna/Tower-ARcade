using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private TowerConf _towerConf;

    [SerializeField]
    private CombatZone _combatZone;

    [SerializeField]
    private AmmunitionHolder _ammunitionHolder;

    [Header("Objects")]
    [SerializeField]
    private Transform _towerHead;

    [SerializeField]
    private Transform _muzzel;

    [Header("Properties")]
    private TowerStateHolder _stateHolder;
    private TowerWeapon _towerWeapon;

    public TowerConf towerConf => _towerConf;
    public CombatZone combatZone => _combatZone;
    public TowerStateHolder stateHolder => _stateHolder;
    public Transform towerHead => _towerHead;

    private void Awake()
    {
        _stateHolder = new(this);
        _towerWeapon = new CanonWeapon(_ammunitionHolder, _towerConf);

        _combatZone.UpdateConf(_towerConf);
    }

    public virtual void Fire(Enemy enemy)
    {
        _towerWeapon.Fire(enemy, _muzzel.position);
    }

    public virtual void RotateHead(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        _towerHead.rotation = Quaternion.AngleAxis(rotation.eulerAngles.y, Vector3.up);
    }

    private void OnDestroy() 
    {
        _stateHolder.Clear();
    }
}
