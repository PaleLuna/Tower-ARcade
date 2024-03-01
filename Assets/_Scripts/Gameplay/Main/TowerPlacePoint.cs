using UnityEngine;

public class TowerPlacePoint : MonoBehaviour
{
    private bool _isFree = true;

    public bool isFree => _isFree;

    public void TakeThisPlace()
    {
        _isFree = false;
    }
}
