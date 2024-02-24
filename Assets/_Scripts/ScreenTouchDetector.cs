using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ScreenTouchDetector : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager _raycastManager;

    [SerializeField]
    private LayerMask _layerMask;

    private List<ARRaycastHit> _raycastHits = new();

    private void OnValidate()
    {
        _raycastManager ??= GetComponent<ARRaycastManager>();
    }

    public bool IsScreenTouch() =>
        Input.touchCount > 0;

    public bool TryGetPlaneTouch(out Vector3 touchPos)
    {
        touchPos = default;

        if (Input.touchCount == 0 || !TryGetPlane(Input.GetTouch(0).position))
            return false;

        touchPos = _raycastHits[0].pose.position;
        return true;
    }

    public bool TryGetPlane(Vector2 startRay) =>
        _raycastManager.Raycast(startRay, _raycastHits, TrackableType.Planes);
    public bool TryGetPlane(Vector2 startRay, out Pose pose)
    {
        pose = default;

        bool isHit = _raycastManager.Raycast(startRay, _raycastHits, TrackableType.Planes);

        if(isHit) pose = _raycastHits[0].pose;

        return isHit;
    }

    public T TryGetComponentFromGO<T>()
        where T : Component
    {
        T component = default;

        if (Input.touchCount == 0)
            return component;

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        if (!Physics.Raycast(ray, out RaycastHit hit))
            return component;

        component = hit.collider.GetComponent<T>();

        return component;
    }
}
