using PaleLuna.Architecture.GameComponent;
using UnityEngine;

public class InputHandler : MonoBehaviour, IStartable
{
    [SerializeField]
    private RayScanner _screenTouchDetector;

    [SerializeField]
    private LevelPlacer _levelPlacer;

    private bool _isStart = false;
    public bool IsStarted => _isStart;

    public void OnStart()
    {
        if(_isStart) return;

        OnLevelRestart();

        GameEvents.gameRestart.AddListener(OnLevelRestart);
        GameEvents.gameDefeatEvent.AddListener(OnGameDefeat);
        GameEvents.levelConfirmEvent.AddListener(OnLevelPlaceConfirm);

        _isStart = true;
    }


    #region [ Event reactions ]
    private void OnTapReaction(GameObject gObj)
    {
        IInteractable interactable = gObj.GetComponent<IInteractable>();
        interactable?.Interact();
    }
    private void OnLevelPlaceConfirm(){
        _screenTouchDetector.UnsubscribeOnPlaneDetect(_levelPlacer.PlaceLevel);
    }

    private void OnLevelRestart(){
        _screenTouchDetector.SubscribeOnPlaneDetect(_levelPlacer.PlaceLevel);

        _screenTouchDetector.SubscribeOnGODetect(OnTapReaction);
    }
    private void OnGameDefeat(){
        _screenTouchDetector.UnsubscribeOnGODetect(OnTapReaction);
    }
    #endregion
}
