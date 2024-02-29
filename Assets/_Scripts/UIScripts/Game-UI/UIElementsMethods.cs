using UnityEngine;

public class UIElementsMethods : MonoBehaviour
{
    public void OnObjectChange(int value) => 
        UIEvents.objectChangeEvent.Invoke(value);
    public void OnScaleChange(int value) => 
        UIEvents.scaleSliderChangeEvent.Invoke(value);

    public void OnDeleteObject() =>
        UIEvents.objectDeleteEvent.Invoke();
}
