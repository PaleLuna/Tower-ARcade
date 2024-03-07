using PaleLuna.Architecture.GameComponent;
using UnityEngine;

public class InputHandler : MonoBehaviour, IStartable
{
    [SerializeField]
    private GameObject _levelPrefab;
    [SerializeField]
    private RayScanner _screenTouchDetector;


    private GameObject _levelObj;

    private bool _isStart = false;
    public bool IsStarted => _isStart;

    public void OnStart()
    {
        _screenTouchDetector.SubscribeOnPlaneDetect(PlaceLevelIfNot);
        _screenTouchDetector.SubscribeOnGODetect(OnTapReaction);
    }

    private void PlaceLevelIfNot(Vector3 point)
    {
        print("Tap");
        if (_levelObj != null) return;

        _levelObj = Instantiate(_levelPrefab);
        _levelPrefab = null;

        _levelObj.transform.position = point;
    }

    private void OnTapReaction(GameObject gObj)
    {
        print("ObjTap");
        if (gObj.GetComponent<TowerPlacePoint>())
            print("TowerPosition");
    }
}
