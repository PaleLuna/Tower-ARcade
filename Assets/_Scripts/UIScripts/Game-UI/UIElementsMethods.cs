using UnityEngine;

public class UIElementsMethods : MonoBehaviour
{
    public void ConfirmLevelPlace() => 
        GameEvents.levelConfirmEvent.Invoke();
}
