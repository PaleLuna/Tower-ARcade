using PaleLuna.Architecture.GameComponent;
using UnityEditor;
using UnityEngine;

public class LevelPlacer : MonoBehaviour, IStartable
{
    

    [SerializeField]
    private GameObject _levelPrefab;
    private GameObject _levelObj;
    
    private bool _isStart = false;

    public bool IsStarted => _isStart;

    public void OnStart()
    {
        if(_isStart) return;
        _isStart = true;
    }
    
    public void PlaceLevel(Vector3 point)
    {
        if (_levelObj == null)
            CreateLevel(point);

        _levelObj.transform.position = point;
    }

    private void CreateLevel(Vector3 spawnPoint)
    {
        _levelObj = Instantiate(_levelPrefab);
        _levelPrefab = null;

        GameEvents.levelPlaceFirstly.Invoke();
    }

    [ContextMenu("Test")]
    private void Test(){
        GameEvents.levelPlaceFirstly.Invoke();
    }
}
