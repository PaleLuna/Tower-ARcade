using PaleLuna.Architecture.GameComponent;
using UnityEngine;

public class LevelPlacer : MonoBehaviour, IStartable
{
    public bool IsStarted => throw new System.NotImplementedException();

    private GameObject _levelPrefab;
    private GameObject _levelObj;
    
    private bool _isStart = false;
    public void OnStart()
    {
        
    }
    
    public void PlaceLevelIfNot(Vector3 point)
    {
        print("Tap");
        if (_levelObj != null)
            return;

        _levelObj = Instantiate(_levelPrefab);
        _levelPrefab = null;

        _levelObj.transform.position = point;
    }
}
