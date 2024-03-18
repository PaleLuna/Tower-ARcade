using UnityEngine;

public class TowerPlacePoint : MonoBehaviour, IInteractable
{
    [SerializeField] Tower _towerPrefab;
    [SerializeField] Vector3 _towerPos;

    private Tower _tower = null;

    public bool isFree => _tower != null;

    public void Interact()
    {
        TakeThisPlace(); 
    }

    private void TakeThisPlace()
    {
        if(!isFree){
            _tower = Instantiate(_towerPrefab, transform.TransformPoint(_towerPos), Quaternion.identity);
            _tower.transform.parent = transform;
        }
    }
}
