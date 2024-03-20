using Services;
using UnityEngine;

public class TowerPlacePoint : MonoBehaviour, IInteractable
{
    [SerializeField] Tower _towerPrefab;
    [SerializeField] Vector3 _towerPos;

    private Tower _tower = null;

    private Wallet _wallet;

    public bool isFree => _tower == null;

    private void Start(){
        GameEvents.gameRestart.AddListener(Clear);

        _wallet = ServiceManager.Instance.SceneLocator.Get<Wallet>();
    }

    public void Interact()
    {
        TakeThisPlace(); 
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        if(isFree) return;
        Destroy(_tower.gameObject);
        _tower = null;
    }

    [ContextMenu("Create")]
    private void TakeThisPlace()
    {
        if(isFree && CheckBalance(_towerPrefab)){
            _tower = Instantiate(_towerPrefab, transform.TransformPoint(_towerPos), Quaternion.identity);
            _tower.transform.parent = transform;
        }
    }

    private bool CheckBalance(Tower tower)
    {
        print(_wallet);
        bool flag = _wallet.TryTakeFromWallet(tower.towerConf.cost);
        return flag;
    }
}
