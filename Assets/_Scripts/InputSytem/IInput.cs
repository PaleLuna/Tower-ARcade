using UnityEngine;

public interface IInput
{
    public bool TryTap(Vector2 tapPosition);
    public bool TryDoubleTap(Vector2 firstTapPosition);
}
