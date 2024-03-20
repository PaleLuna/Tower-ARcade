using PaleLuna.Architecture.GameComponent;
using Services;
using UnityEngine;

public class LevelPlacer : MonoBehaviour, IStartable
{
    private GameObject _levelObj;

    private CameraScript _camera;
    
    private bool _isStart = false;
    public bool IsStarted => _isStart;

    public void OnStart()
    {
        if(_isStart) return;

        _camera = ServiceManager.Instance.SceneLocator.Get<CameraScript>();

        CreateLevel();

        _isStart = true;
    }
    public void PlaceLevel(Vector3 point)
    {
        if (!_levelObj.activeSelf) EnableLevel();
            

        _levelObj.transform.position = point;
        RotateToCam();

    }
    private void CreateLevel()
    {
        _levelObj = Instantiate(ServiceManager.Instance.SceneLocator.Get<Game>().levelConfig.levelPrefab.gameObject);
        _levelObj.SetActive(false);
    }
    private void EnableLevel()
    {
        _levelObj.SetActive(true);
        GameEvents.levelPlaceFirstly.Invoke();
    }

    private void RotateToCam()
    {
        Vector3 direction = (_camera.transform.position - _levelObj.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction*-1);

        _levelObj.transform.rotation = Quaternion.AngleAxis(rotation.eulerAngles.y, Vector3.up);
    }

    [ContextMenu("Test")]
    private void Test()
    {
        PlaceLevel(Vector3.zero);
    }
}
