using UnityEngine.Events;

public static class GameEvents
{
    public static readonly UnityEvent<Enemy> enemyDeathEvent = new();

    public static readonly UnityEvent _levelConfirmEvent = new();

}
