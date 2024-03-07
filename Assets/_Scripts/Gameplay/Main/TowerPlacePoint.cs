using UnityEngine;

public class TowerPlacePoint : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _towerPrefab;

    private Transform _tower = null;

    public bool isFree => _tower != null;

    public void Interact()
    {
        TakeThisPlace(); 
    }

    private void TakeThisPlace()
    {
        if(!isFree)
            _tower = Instantiate(_towerPrefab, transform.position, Quaternion.identity);
    }
}
