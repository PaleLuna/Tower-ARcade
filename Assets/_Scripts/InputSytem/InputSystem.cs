using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private IInput _inputHandler = new MobileInput();

    private void Update()
    {
        CheckTap();
        CheckDoubleTap();
    }
    private void CheckTap()
    {
        if (_inputHandler.TryTap(out Vector2 pos))
            InputEvents.tapEvent.Invoke(pos);
    }

    private void CheckDoubleTap()
    {
        if (_inputHandler.TryDoubleTap(out Vector2 pos))
            InputEvents.doubleTapEvent.Invoke(pos);
    }
}
