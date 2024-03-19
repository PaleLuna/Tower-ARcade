using UnityEngine;
using UnityEngine.Events;

public static class InputEvents
{
    public static readonly UnityEvent<Vector2> tapEvent = new();
    public static readonly UnityEvent<Vector2> doubleTapEvent = new();
}
