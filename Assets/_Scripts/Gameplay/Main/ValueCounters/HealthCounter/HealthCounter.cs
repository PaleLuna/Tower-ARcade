
public class HealthCounter : ValueCounter
{
    protected override void InvokeEventOnChange()
    {
        GameEvents.baseHealthChangedEvent.Invoke(m_currentValue);
    }

}
