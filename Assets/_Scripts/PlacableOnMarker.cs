using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacableOnMarker : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField] 
    private GameObject _markerPrefab;

    [SerializeField]
    private ARRaycastManager _raycastManager;

    private List<ARRaycastHit> _raycastHits = new();

    private GameObject _spawnedObject = null;
    private GameObject _marker;

    private Vector2 _rayStart;

    private void OnValidate()
    {
        _raycastManager ??= GetComponent<ARRaycastManager>();
    }

    private void Start()
    {
        _rayStart = new Vector2(Screen.width / 2, Screen.height / 2);

        _marker = Instantiate(_markerPrefab);
        _marker.SetActive(false);
    }

    private void Update()
    {
        if (!_raycastManager.Raycast(_rayStart, _raycastHits, TrackableType.Planes))
        {
            _marker.SetActive(false);
            return;
        }
        var hitPose = _raycastHits[0].pose;

        _marker.SetActive(true);
        _marker.transform.position = hitPose.position;

        if (Input.touchCount == 0) return;

        if (!_spawnedObject)
            _spawnedObject = Instantiate(_prefab, hitPose.position, hitPose.rotation);

        _spawnedObject.transform.position = hitPose.position;
    }
}
