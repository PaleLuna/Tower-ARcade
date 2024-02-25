using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : IInput
{
    private const float SECONDS_TO_DOUBLE_TAP = 0.2F;

    private float _lastTapTime = 0;

    public bool TryDoubleTap(out Vector2 lastTapPosition)
    {
        if (!TryTap(out lastTapPosition)) return false;

        if (Time.time - _lastTapTime < SECONDS_TO_DOUBLE_TAP)
            _lastTapTime = 0;
        _lastTapTime = Time.time;

        return lastTapPosition != default;
    }

    public bool TryTap(out Vector2 tapPosition)
    {
        tapPosition = default;
        if (Input.touchCount == 0) return false;

        tapPosition = Input.GetTouch(0).position;
        return true;
    }
}
