using PaleLuna.Architecture.GameComponent;
using UnityEngine;

public class InputHandler : MonoBehaviour, IStartable
{
    [SerializeField]
    private GameObject _levelPrefab;

    [SerializeField]
    private RayScanner _screenTouchDetector;

    [SerializeField]
    private LevelPlacer _levelPlacer;

    private GameObject _levelObj;

    private bool _isStart = false;
    public bool IsStarted => _isStart;

    public void OnStart()
    {
        _screenTouchDetector.SubscribeOnPlaneDetect(_levelPlacer.PlaceLevelIfNot);
        _screenTouchDetector.SubscribeOnGODetect(OnTapReaction);
    }


    private void OnTapReaction(GameObject gObj)
    {
        IInteractable interactable = gObj.GetComponent<IInteractable>();
        print($"ObjTap {interactable}");
        interactable?.Interact();
    }
}
