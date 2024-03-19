using UnityEngine.Events;

public static class UIEvents
{
    public static readonly UnityEvent<TowerPlacePoint> towerPlaceSelect = new();
    public static readonly UnityEvent<TowerPlacePoint> towerPlaceDeselect = new();
}
