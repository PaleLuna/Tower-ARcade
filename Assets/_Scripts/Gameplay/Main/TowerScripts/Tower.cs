using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerConf _towerConf;
    [SerializeField] private CombatZone _combatZone;

    public TowerConf towerConf => _towerConf;
    public CombatZone combatZone => _combatZone;

    virtual public void Fire(Transform enemyTransform) //In the future it will be used for shooting
    {
        Debug.Log($"Fire on {enemyTransform.gameObject.name}");
    } 
}
