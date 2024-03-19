using UnityEngine;

public class UIElementsMethods : MonoBehaviour
{
    public void ConfirmLevelPlace() => 
        GameEvents.levelConfirmEvent.Invoke();

    public void RestartLevel() =>
        GameEvents.gameRestart.Invoke();
}
