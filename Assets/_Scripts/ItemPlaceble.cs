using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ItemPlaceble : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private ARRaycastManager _raycastManager;

    private List<ARRaycastHit> _raycastHits = new();
    private GameObject _spawnedObject;

    private void OnValidate()
    {
        _raycastManager ??= GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        Vector2 touchPos = default;

        if (!TryGetTouchPos(ref touchPos)) return;

        if (!_raycastManager.Raycast(touchPos, _raycastHits, TrackableType.PlaneWithinPolygon)) return;

        var hitPose = _raycastHits[0].pose;
        if (!_spawnedObject)
            _spawnedObject = Instantiate(_prefab, hitPose.position, hitPose.rotation);

        _spawnedObject.transform.position = hitPose.position;
    }

    private bool TryGetTouchPos(ref Vector2 pos)
    {
        if (Input.touchCount > 0)
            pos = Input.GetTouch(0).position;
        
        return pos != default;
    }
}
