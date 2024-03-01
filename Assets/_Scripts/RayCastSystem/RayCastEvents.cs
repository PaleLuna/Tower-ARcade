using UnityEngine;
using UnityEngine.Events;

public class RayCastEvents
{
    private readonly UnityEvent<GameObject> _detectGameObjectEvent = new();
    private readonly UnityEvent _detectUIEvent = new();
    private readonly UnityEvent<Vector3> _detectPlaneEvent = new();
    private readonly UnityEvent _detectNothing = new();

    #region [ Subscribe ]
    public void SubscribeGODetect(UnityAction<GameObject> action) =>
        _detectGameObjectEvent.AddListener(action);
    public void SubscribeUIDetect(UnityAction action) =>
        _detectUIEvent.AddListener(action);
    public void SubscribePlaneDetect(UnityAction<Vector3> action) =>
        _detectPlaneEvent.AddListener(action);
    public void SubscribeNothingDetect(UnityAction action) =>
        _detectNothing.RemoveListener(action);
    #endregion

    #region [ Unsubscribe ]
    public void UnsubscribeGODetect(UnityAction<GameObject> action) =>
        _detectGameObjectEvent.RemoveListener(action);
    public void UnsubscribeUIDetect(UnityAction action) =>
        _detectUIEvent.RemoveListener(action);
    public void UnsubscribePlaneDetect(UnityAction<Vector3> action) =>
        _detectPlaneEvent.RemoveListener(action);
    public void UnsubscribeNothingDetect(UnityAction action) =>
            _detectNothing.RemoveListener(action);
    #endregion

    #region [ Invoke ]
    public void OnGODetect(GameObject gObj) =>
        _detectGameObjectEvent.Invoke(gObj);
    public void OnUIDetect() =>
        _detectUIEvent.Invoke();
    public void OnPlaneDetect(Vector3 point) =>
        _detectPlaneEvent.Invoke(point);
    public void OnNothingDetect() =>
        _detectNothing.Invoke();
    #endregion
}
