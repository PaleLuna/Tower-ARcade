using UnityEngine;

public interface IInput
{
    public bool TryTap(out Vector2 tapPosition);
    public bool TryDoubleTap(out Vector2 lastTapPosition);
}
