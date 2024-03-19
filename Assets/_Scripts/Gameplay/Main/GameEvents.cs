using UnityEngine.Events;

public static class GameEvents
{

    #region [ Global game events ]
    
    public static readonly UnityEvent gameDefeatEvent = new();
    public static readonly UnityEvent gameRestart = new();

    #endregion

    #region [ Global level events ]
    public static readonly UnityEvent levelPlaceFirstly = new();
    public static readonly UnityEvent levelConfirmEvent = new();
    #endregion

    #region [ Global enemy events ]

    public static readonly UnityEvent<Enemy> enemyDeathEvent = new();
    public static readonly UnityEvent<Enemy> enemyFinishReachedEvent = new();

    #endregion

    #region [ Global wallet events ]
        public static readonly UnityEvent<int> walletBalanceChangedEvent = new();
    #endregion
}
