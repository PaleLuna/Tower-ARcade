using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using Services;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

public class PlacableOnMarker : MonoBehaviour, IStartable, IUpdatable
{
    [SerializeField] 
    private GameObject _markerPrefab;

    [SerializeField]
    private Transform _camTransform;

    [SerializeField]
    private RayScanner _touchDetector;

    private GameObject _marker;


    private Vector2 _rayStart;
    private Pose _currentPoseHit;

    private bool _isStart = false;
    public bool IsStarted => _isStart;

    public void OnStart()
    {
        _rayStart = new Vector2(Screen.width / 2, Screen.height / 2);

        _marker = Instantiate(_markerPrefab);
        _marker.SetActive(false);

        _isStart = true;

        ServiceManager.Instance.GlobalServices.Get<GameController>().Registatrion(this);
    }

    public void EveryFrameRun()
    {
        print("Update");

        if (!_touchDetector.TryGetPlane(_rayStart, out _currentPoseHit))
        {
            _marker.SetActive(false);
            return;
        }

        _marker.SetActive(true);
        _marker.transform.position = _currentPoseHit.position;

        if (Input.touchCount == 0) return;
    }

    public bool TryPlaceObjectOnMarker(Transform objTransform)
    {
        print($"Marker: {_marker.activeSelf}");
        if (!_marker.activeSelf) return false;

        objTransform.position = _marker.transform.position;
        RotateToCamera(objTransform);

        return true;
    }


    private void RotateToCamera(Transform objTransform)
    {
        Vector3 direction = _camTransform.position - objTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        objTransform.transform.rotation = Quaternion.AngleAxis(rotation.eulerAngles.y, Vector3.up);
    }
}
