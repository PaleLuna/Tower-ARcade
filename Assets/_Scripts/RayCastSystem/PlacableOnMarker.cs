using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using Services;
using UnityEngine;

public class PlacableOnMarker : MonoBehaviour, IStartable, IUpdatable
{
    #region [ Properties ]

    #region [ Serializable ]
    [Header("GameObjects")]
    [SerializeField]
    private GameObject _markerPrefab;

    [Header("Components")]
    [SerializeField]
    private Transform _camTransform;

    [SerializeField]
    private RayScanner _touchDetector;
    #endregion

    #region [ Flags ]
    private bool _isStart = false;
    public bool IsStarted => _isStart;
    #endregion

    #region [ Other ]
    private Vector2 _rayStart;
    private Pose _currentPoseHit;

    private GameObject _marker;
    #endregion

    #endregion

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
        if (_touchDetector.TryGetPlane(_rayStart, out _currentPoseHit))
            ReplaceMarker();
        else
            _marker.SetActive(false);
    }

    public bool TryPlaceObjectOnMarker(Transform objTransform)
    {
        print($"Marker: {_marker.activeSelf}");
        if (!_marker.activeSelf)
            return false;

        objTransform.position = _marker.transform.position;
        RotateToCamera(objTransform);

        return true;
    }

    #region [ Private Methods ]

    private void RotateToCamera(Transform objTransform)
    {
        Vector3 direction = _camTransform.position - objTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        objTransform.transform.rotation = Quaternion.AngleAxis(rotation.eulerAngles.y, Vector3.up);
    }

    private void ReplaceMarker()
    {
        _marker.SetActive(true);
        _marker.transform.position = _currentPoseHit.position;
    }

    #endregion
}
