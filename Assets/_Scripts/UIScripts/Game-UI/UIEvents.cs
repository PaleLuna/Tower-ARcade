using UnityEngine.Events;

public static class UIEvents
{
    private static UnityEvent _objectSelect = new();
    private static UnityEvent _objectDeselect = new();

    private static UnityEvent _objectDelete = new();

    private static UnityEvent<int> _objectChange = new();
    private static UnityEvent<int> _scaleSliderChange = new();

    public static UnityEvent objectSelectEvent => _objectSelect;
    public static UnityEvent objectDeselectEvent => _objectDeselect;

    public static UnityEvent objectDeleteEvent => _objectDelete;

    public static UnityEvent<int> objectChangeEvent => _objectChange;
    public static UnityEvent<int> scaleSliderChangeEvent => _scaleSliderChange;

}
