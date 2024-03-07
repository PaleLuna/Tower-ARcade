using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerConf _towerConf;

    public TowerConf towerConf => _towerConf;
}
