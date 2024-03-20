using PaleLuna.Architecture.GameComponent;
using UnityEngine;
using UnityEngine.UI;


public class UIObjectControll : MonoBehaviour, IStartable
{
    [Header("Buttons")]
    [SerializeField]
    private Button _confirmBtn;

    [SerializeField]
    private Button _pauseBtn;

    [Header("Panels")]
    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private GameObject _gameDefeatPanel;

    private bool _isStart = false;

    public bool IsStarted => _isStart;

    public void OnStart()
    {
        if(_isStart) return;

        SubscribeOnEvents();

        _confirmBtn.interactable = false;
        _isStart = true;
    }

    private void SubscribeOnEvents()
    {
        GameEvents.gameRestart.AddListener(OnGameRestart);
        GameEvents.levelConfirmEvent.AddListener(OnLevelConfirm);
        GameEvents.levelPlaceFirstly.AddListener(OnLevelPlaceFirstly);
        GameEvents.gameDefeatEvent.AddListener(OnGameDefeat);

        GameEvents.gameSetPauseEvent.AddListener(OnGameSetPause);
        GameEvents.gameSetResumeEvent.AddListener(OnGameSetResume);
    }

    private void OnGameRestart(){
        _confirmBtn.gameObject.SetActive(true);

        _gameDefeatPanel.SetActive(false);
        OnGameSetResume();
        _pauseBtn.gameObject.SetActive(false);
    }
    private void OnLevelConfirm(){
        _confirmBtn.gameObject.SetActive(false);
        _pauseBtn.gameObject.SetActive(true);
    }
    private void OnLevelPlaceFirstly(){
        _confirmBtn.interactable = true;
        OnGameRestart();
    }

    private void OnGameSetPause()
    {
        _pauseBtn.gameObject.SetActive(false);
        _pausePanel.SetActive(true);
    }
    private void OnGameSetResume()
    {
        _pausePanel.SetActive(false);
        _pauseBtn.gameObject.SetActive(true);
    }

    private void OnGameDefeat()
    {
        _gameDefeatPanel.SetActive(true);
        _pauseBtn.gameObject.SetActive(false);
    }
}
