public class Wallet : ValueCounter
{
    protected override void InvokeEventOnChange()
    {
        GameEvents.walletBalanceChangedEvent.Invoke(m_currentValue);
    }
}
