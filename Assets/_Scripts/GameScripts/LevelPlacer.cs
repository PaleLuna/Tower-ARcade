using PaleLuna.Architecture.GameComponent;
using Services;
using UnityEditor;
using UnityEngine;

public class LevelPlacer : MonoBehaviour, IStartable
{
    

    [SerializeField]
    private GameObject _levelPrefab;
    private GameObject _levelObj;

    private CameraScript _camera;
    
    private bool _isStart = false;

    public bool IsStarted => _isStart;

    public void OnStart()
    {
        if(_isStart) return;

        _camera = ServiceManager.Instance.SceneLocator.Get<CameraScript>();

        _isStart = true;
    }
    
    public void PlaceLevel(Vector3 point)
    {
        if (_levelObj == null)
            CreateLevel(point);

        _levelObj.transform.position = point;
        RotateToCam();

    }
    private void CreateLevel(Vector3 spawnPoint)
    {
        _levelObj = Instantiate(_levelPrefab);
        _levelPrefab = null;

        GameEvents.levelPlaceFirstly.Invoke();
    }

    private void RotateToCam()
    {
        Vector3 direction = (_camera.transform.position - _levelObj.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction*-1);

        _levelObj.transform.rotation = Quaternion.AngleAxis(rotation.eulerAngles.y, Vector3.up);
    }

    [ContextMenu("Test")]
    private void Test(){
        GameEvents.levelPlaceFirstly.Invoke();
    }
}
