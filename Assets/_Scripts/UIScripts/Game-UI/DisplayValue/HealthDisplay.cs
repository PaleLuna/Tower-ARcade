
using Services;

public class HealthDisplay : ValueDisplay
{
    private void OnEnable() {
        GameEvents.baseHealthChangedEvent.AddListener(OnValueUpdate);
        CheckValue();
    }
    private void OnDisable()
    {
        GameEvents.baseHealthChangedEvent.RemoveListener(OnValueUpdate);
    }

    protected override void CheckValue()
    {
        OnValueUpdate(ServiceManager.Instance.SceneLocator.Get<ValueCounterHolder>().Get<HealthCounter>().currentValue);
    }

}
