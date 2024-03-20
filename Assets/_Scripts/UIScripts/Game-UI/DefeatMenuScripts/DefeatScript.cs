using PaleLuna.Architecture.GameComponent;
using Services;
using TMPro;
using UnityEngine;

public class DefeatScript : MonoBehaviour, IStartable
{
    [Header("TMP Components")]
    [SerializeField]
    private TextMeshProUGUI _enemyKillScoreText;
    [SerializeField]
    private TextMeshProUGUI _scoreText;


    private EnemyKillCounter _enemyKillCounter;
    private ScoreCounter _scoreCounter;

    private bool _isStart;
    public bool IsStarted => _isStart;


    public void OnStart()
    {
        if(_isStart) return;

        _enemyKillCounter = ServiceManager.Instance.SceneLocator.Get<ValueCounterHolder>().Get<EnemyKillCounter>();
        _scoreCounter = ServiceManager.Instance.SceneLocator.Get<ValueCounterHolder>().Get<ScoreCounter>();

        _isStart = true;
    }

    private void OnEnable() {
        UpdateScoreTexts();
    }


    private void UpdateScoreTexts()
    {
        _enemyKillScoreText.text = _enemyKillCounter.currentValue.ToString();
        _scoreText.text = _scoreCounter.currentValue.ToString();
    }

    

}
