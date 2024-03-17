using UnityEngine.Events;

public static class GameEvents
{
    public static readonly UnityEvent<Enemy> enemyDeathEvent = new();

    public static readonly UnityEvent levelConfirmEvent = new();
    public static readonly UnityEvent<Enemy> enemyFinishReachedEvent = new();

    public static readonly UnityEvent gameDefeatEvent = new();
    public static readonly UnityEvent gameRestart = new();
}
