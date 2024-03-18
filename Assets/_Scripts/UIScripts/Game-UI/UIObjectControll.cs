using PaleLuna.Architecture.GameComponent;
using UnityEngine;
using UnityEngine.UI;


public class UIObjectControll : MonoBehaviour, IStartable
{
    [SerializeField]
    private Button _confirmBtn;
    [SerializeField]
    private Button _restartBtn;

    private bool _isStart = false;

    public bool IsStarted => _isStart;

    public void OnStart()
    {
        if(_isStart) return;

        GameEvents.gameRestart.AddListener(OnGameRestart);
        GameEvents.levelConfirmEvent.AddListener(OnLevelConfirm);
        GameEvents.levelPlaceFirstly.AddListener(OnLevelPlaceFirstly);

        _confirmBtn.interactable = false;
        _isStart = true;
    }

    private void OnGameRestart(){
        _confirmBtn.gameObject.SetActive(true);
        _restartBtn.gameObject.SetActive(false);
    }
    private void OnLevelConfirm(){
        _confirmBtn.gameObject.SetActive(false);
        _restartBtn.gameObject.SetActive(true);
    }
    private void OnLevelPlaceFirstly(){
        _confirmBtn.interactable = true;
        OnGameRestart();
    }
}
