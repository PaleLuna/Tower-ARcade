using JetBrains.Annotations;
using UnityEngine;

public class TowerPlacePoint : MonoBehaviour, IInteractable
{
    [SerializeField] Tower _towerPrefab;
    [SerializeField] Vector3 _towerPos;

    private Tower _tower = null;

    public bool isFree => _tower == null;

    private void Start(){
        GameEvents.gameRestart.AddListener(Clear);
    }

    public void Interact()
    {
        TakeThisPlace(); 
    }

    public void Clear()
    {
        if(isFree) return;
        Destroy(_tower.gameObject);
    }

    private void TakeThisPlace()
    {
        if(isFree){
            _tower = Instantiate(_towerPrefab, transform.TransformPoint(_towerPos), Quaternion.identity);
            _tower.transform.parent = transform;
        }
    }
}
