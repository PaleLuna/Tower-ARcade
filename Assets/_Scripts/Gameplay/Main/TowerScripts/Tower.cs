using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TowerConf _towerConf;
    [SerializeField] private CombatZone _combatZone;
    [SerializeField] private AmmunitionHolder _ammunitionHolder;

    [Header("Objects")]
    [SerializeField] private Transform _towerHead;
    [SerializeField] private Transform _muzzel;

    [Header("Properties")]

    private TowerStateHolder _stateHolder;

    private bool _timeToShoot = true;

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
        if (!_ammunitionHolder || !_timeToShoot) return;

        Shell shell = _ammunitionHolder.GetShell();

        Vector3 direction = enemyTransform.position - transform.TransformDirection(_muzzel.transform.position);
        shell.transform.position = _muzzel.position;

        shell.ThrowThis(direction, towerConf.initialSpeed, _towerConf.damageByHit);

        StartCoroutine(CoolDown());
    }
    virtual public void RotateHead(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        _towerHead.rotation = Quaternion.AngleAxis(rotation.eulerAngles.y, Vector3.up);
    }

    private IEnumerator CoolDown()
    {
        _timeToShoot = false;

        yield return new WaitForSeconds(60F / _towerConf.shotsPerMinute);

        _timeToShoot = true;
    }
}
