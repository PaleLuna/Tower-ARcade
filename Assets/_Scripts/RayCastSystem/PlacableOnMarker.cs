using UnityEngine;
using UnityEngine.XR.ARSubsystems;

public class PlacableOnMarker : MonoBehaviour
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

    private void Start()
    {
        _rayStart = new Vector2(Screen.width / 2, Screen.height / 2);

        _marker = Instantiate(_markerPrefab);
        _marker.SetActive(false);
    }

    private void Update()
    {
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
