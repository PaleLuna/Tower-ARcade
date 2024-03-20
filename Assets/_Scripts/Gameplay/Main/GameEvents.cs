using UnityEngine.Events;

public static class GameEvents
{

    #region [ Global game events ]
    
    public static readonly UnityEvent gameDefeatEvent = new();
    public static readonly UnityEvent gameRestart = new();

    public static readonly UnityEvent gameSetPauseEvent = new();
    public static readonly UnityEvent gameSetResumeEvent = new();

    #endregion

    #region [ Global level events ]
    public static readonly UnityEvent levelPlaceFirstly = new();
    public static readonly UnityEvent levelConfirmEvent = new();
    #endregion

    #region [ Global enemy events ]

    public static readonly UnityEvent<Enemy> enemyDeathEvent = new();
    public static readonly UnityEvent<Enemy> enemyFinishReachedEvent = new();

    #endregion

    #region [ Global counters events ]
        public static readonly UnityEvent<int> walletBalanceChangedEvent = new();
        public static readonly UnityEvent<int> baseHealthChangedEvent = new();
    #endregion
}
