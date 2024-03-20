using Services;

public class BalanceDisplay :ValueDisplay
{
    private void OnEnable() 
    {
        GameEvents.walletBalanceChangedEvent.AddListener(OnValueUpdate);
        CheckValue();
    }
    private void OnDisable()
    {
        GameEvents.walletBalanceChangedEvent.RemoveListener(OnValueUpdate);
    }

    protected override void CheckValue() =>
        OnValueUpdate(ServiceManager.Instance.SceneLocator.Get<ValueCounterHolder>().Get<Wallet>().currentValue);

}
