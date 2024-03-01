using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class RayScanner : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCam;

    [SerializeField]
    private ARRaycastManager _raycastManager;

    [SerializeField]
    private LayerMask _layerMask;

    private List<ARRaycastHit> _raycastHits = new();

    private RayCastEvents _rayCastEvents = new();

    private void OnValidate()
    {
        _raycastManager ??= GetComponent<ARRaycastManager>();
    }

    private void Start()
    {
        InputEvents.tapEvent.AddListener(RayCastOnTouch);
    }

    #region [ Events public methods ]
    public void SubscribeOnGODetect(UnityAction<GameObject> action) =>
        _rayCastEvents.SubscribeGODetect(action);
    public void SubscribeOnPlaneDetect(UnityAction<Vector3> action) =>
        _rayCastEvents.SubscribePlaneDetect(action);
    public void SubscribeOnUIDetect(UnityAction action) =>
        _rayCastEvents.SubscribeUIDetect(action);
    public void SubscribeOnNothingDetect(UnityAction action) =>
        _rayCastEvents.SubscribeNothingDetect(action);

    public void UnsubscribeOnGODetect(UnityAction<GameObject> action) =>
        _rayCastEvents.UnsubscribeGODetect(action);
    public void UnsubscribeOnPlaneDetect(UnityAction<Vector3> action) =>
        _rayCastEvents.UnsubscribePlaneDetect(action);
    public void UnsubscribeOnUIDetect(UnityAction action) =>
        _rayCastEvents.UnsubscribeUIDetect(action);
    public void UnsubscribeOnNothingDetect(UnityAction action) =>
        _rayCastEvents.UnsubscribeNothingDetect(action);
    #endregion

    private void RayCastOnTouch(Vector2 startPos)
    {
        print("Tap!");
        if (IsUI(startPos)) _rayCastEvents.OnUIDetect();
        else if (IsGameObject(startPos, out GameObject gObj)) _rayCastEvents.OnGODetect(gObj);
        else if (IsPlane(startPos, out Vector3 point)) _rayCastEvents.OnPlaneDetect(point);
        else _rayCastEvents.OnNothingDetect();
    }

    private bool IsPlane(Vector2 startPos, out Vector3 point)
    {
        bool flag = TryGetPlaneTouch(startPos, out point);

        print($"is Plane: {flag}");

        return flag;
    }

    private bool IsGameObject(Vector2 startPos, out GameObject gObj)
    {
        gObj = null;

        Ray ray = _mainCam.ScreenPointToRay(startPos);
        bool isGObj = Physics.Raycast(ray, out RaycastHit hit);

        if (isGObj)
            gObj = hit.collider.gameObject;
        bool flag = isGObj && !gObj.GetComponent<CanvasRenderer>() && !gObj.GetComponent<ARPlane>();

        print($"is GO: {flag}; {gObj}");
        return flag;
    }

    private bool IsUI(Vector2 startPos)
    {
        bool flag = IsGameObject(startPos, out GameObject gObj) && gObj.GetComponent<CanvasRenderer>() ;

        print($"is UI: {flag}");
        return flag;
    }

    public bool TryGetPlaneTouch(Vector2 touchPos, out Vector3 point)
    {
        point = default;
        if (!TryGetPlane(touchPos))
            return false;

        point = _raycastHits[0].pose.position;
        return true;
    }

    public bool TryGetPlane(Vector2 startRay) =>
        _raycastManager.Raycast(startRay, _raycastHits, TrackableType.Planes);

    public bool TryGetPlane(Vector2 startRay, out Pose pos)
    {
        pos = default;
        bool isPlane = _raycastManager.Raycast(startRay, _raycastHits, TrackableType.Planes);
        if (isPlane)
            pos = _raycastHits[0].pose;
        return isPlane;
    }
}
